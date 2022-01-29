using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Defender : MonoBehaviour
{
    [SerializeField] private Vector2 input;
    [SerializeField] private Vector2 lastInput;
    public KeyCode absorbKey;
    public Transform playerGraphics;
    public float absorbRadius;
    public float absorbAngle;
    public LayerMask absorbLayer;
    public float movementSpeed;
    public GameObject absorbIndicator;
    public KeyCode attrackKey;
    public bool ableToMove;
    public bool absorbing;
    public float timeAbsorbing;
    private void Update()
    {
        GetMovementInput();
        Movement();
        GetAbsorbInput();
        if (absorbing)
        {
            Absorb();
        }
        if (GetAttrackInput())
        {
            Attrack();
        }
    }
    void GetMovementInput()
    {
        //Test
        if (!absorbing)
        {
            Vector2 newInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            input = newInput;
            if (newInput != Vector2.zero)
                lastInput = newInput;
            input = input.normalized;
            lastInput = lastInput.normalized;
        }
        //New Input
    }
    void GetAbsorbInput()
    {
        //Test
        if (Input.GetKeyDown(absorbKey) && !absorbing)
        {
            absorbing = true;
            ableToMove = false;
            Invoke(nameof(IT), timeAbsorbing);
        }
        //New Input
    }
    bool GetAttrackInput()
    {
        return Input.GetKeyDown(attrackKey);
    }
    void Absorb()
    {
        Collider2D[] bullets = Physics2D.OverlapCircleAll(transform.position, absorbRadius, absorbLayer);
        if (bullets.Length > 0)
        {
            foreach (Collider2D collider in bullets)
            {
                if (Vector2.Angle((transform.position + new Vector3(lastInput.x, lastInput.y, 0)) - transform.position, (collider.transform.position - transform.position).normalized) <= absorbAngle)
                {
                    Debug.Log(collider.name);
                    //Absorber
                    collider.GetComponent<EnemyBullet>().target = transform;
                    collider.GetComponent<EnemyBullet>().pool = true;
                }
            }
        }
        absorbIndicator.SetActive(true);
        absorbIndicator.transform.right = new Vector3(lastInput.x, lastInput.y).normalized;
    }
    void Movement()
    {
        if (ableToMove)
        {
            Flip();
            transform.Translate(input * Time.deltaTime * movementSpeed, Space.World);
        }
    }
    void Flip()
    {
        if (input.x >= 0.01f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (input.x <= -0.01f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void SwapCharacterBehaviour()
    {
        //Activar el componente atacante
    }
    void Attrack()
    {

    }
    void IT()
    {
        absorbIndicator.SetActive(false);
        absorbing = false;
        ableToMove = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(lastInput.x, lastInput.y, 0) * absorbRadius);
    }
}