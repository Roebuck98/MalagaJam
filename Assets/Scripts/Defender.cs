using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
public class Defender : MonoBehaviour
{
    [SerializeField] private Vector2 input;
    [SerializeField] private Vector2 lastInput;
    private Vector2 rotation;
    private Vector2 lastRotation;
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
    [Range(0f, 1f)]
    public float InputMovementThreshold = 0.1f;

    private Attacker attacker;
    public GameObject otherPlayer;
    public float energyBar;

    public float absorbCooldown;
    private float lastTimeAbsorb;

    public float absorbMovementSpeed;

    private void Awake()
    {
        attacker = GetComponent<Attacker>();
        lastTimeAbsorb = -absorbCooldown;
    }
    private void Update()
    {
        //GetMovementInput();
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

    private void FixedUpdate()
    {
        Movement();
    }
    public void OnMovement(InputAction.CallbackContext value)
    {
        var inputMovement = value.ReadValue<Vector2>();
        var rawInputMovement = new Vector2(inputMovement.x, inputMovement.y);
        rawInputMovement = FilterInput(rawInputMovement, InputMovementThreshold);
        input = rawInputMovement;
        if (rawInputMovement != Vector2.zero)
            lastInput = rawInputMovement;
        input = input.normalized;
        lastInput = lastInput.normalized;
    }
    public void OnAbsorb(InputAction.CallbackContext value)
    {
        if(!absorbing && lastTimeAbsorb + absorbCooldown <= Time.time)
        {
            absorbing = true;
            Invoke(nameof(RestartAbsorb), timeAbsorbing);
        }
    }
    void OnRotation(InputAction.CallbackContext value)
    {
        var inputMovement = value.ReadValue<Vector2>();
        var rawInputMovement = new Vector2(inputMovement.x, inputMovement.y);
        rawInputMovement = FilterInput(rawInputMovement, InputMovementThreshold);
        rotation = rawInputMovement;
        if (rawInputMovement != Vector2.zero)
            lastRotation = rawInputMovement;
        rotation = rotation.normalized;
        lastRotation = lastRotation.normalized;
    }

    public void ChangeRole()
    {
        if(energyBar >= 1) 
        {
            energyBar = 0;
            attacker.enabled = true;
            Debug.Log("Cambiando rol");
            otherPlayer.GetComponent<Attacker>().ChangeRole();
            //attacker.ChangeRole();
            this.enabled = false;
        }
            
    }

    public void onSwap(InputAction.CallbackContext value)
    {
        ChangeRole();
    }
    void GetAbsorbInput()
    {
        //Test
        if (Input.GetKeyDown(absorbKey) && !absorbing)
        {
            absorbing = true;
            Invoke(nameof(RestartAbsorb), timeAbsorbing);
        }
        //New Input
    }
    bool GetAttrackInput()
    {
        return Input.GetKeyDown(attrackKey);
    }
    void Absorb()
    {
        if(absorbing)
        {
            Collider2D[] bullets = Physics2D.OverlapCircleAll(transform.position, absorbRadius, absorbLayer);
            if (bullets.Length > 0)
            {
                foreach (Collider2D collider in bullets)
                {
                    if (Vector2.Angle((transform.position + new Vector3(lastRotation.x, lastRotation.y, 0)) - transform.position, (collider.transform.position - transform.position).normalized) <= absorbAngle)
                    {
                        Debug.Log(collider.name);
                        //Absorber
                        if(!collider.GetComponent<BulletShooter>().pool)
                        {
                            collider.GetComponent<BulletShooter>().target = transform;
                            collider.GetComponent<BulletShooter>().pool = true;
                        }
                    }
                }
            }
            absorbIndicator.SetActive(true);
            absorbIndicator.transform.right = new Vector3(lastRotation.x, lastRotation.y).normalized;
        }
    }
    void Movement()
    {
        if (ableToMove)
        {
            Flip();
            if(absorbing)
            {
                transform.Translate(input * Time.fixedDeltaTime * absorbMovementSpeed, Space.World);
            }
            else
            {
                transform.Translate(input * Time.fixedDeltaTime * movementSpeed, Space.World);
            }
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
    void RestartAbsorb()
    {
        absorbIndicator.SetActive(false);
        absorbing = false;
        lastTimeAbsorb = Time.time;
    }
    private Vector3 FilterInput(Vector3 rawInput, float threshold)
    {
        return new Vector3(
            HighPassFilter(rawInput.x, threshold),
            HighPassFilter(rawInput.y, threshold),
            0);
    }

    private float HighPassFilter(float value, float threshold)
    {
        return Mathf.Abs(value) > threshold ? value : 0;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(lastInput.x, lastInput.y, 0) * absorbRadius);
    }
}