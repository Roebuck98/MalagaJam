using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attacker : MonoBehaviour
{

    private PlayerInput playerInput;
    public CharacterController CharacterController;
    
    public float MovementSped = 10f;
    [Range(0f, 1f)]
    public float InputMovementThreshold = 0.1f;

    [SerializeField] private Vector3 _InputMovement;

    public GameObject bulletPrefab;
    public Transform bulletStart;
    public float FireRate = 3;

    private bool _shooting = false;
    private float _lastShotTime = 0;
    private Defender def;

    public float RotationSpeed = 50f;
    [Range(0f, 1f)]
    public float InputRotationThreshold = 0.6f;
    private Vector3 _InputRotation;
    private Vector3 _lastInputRotation;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        def = GetComponent<Defender>();
        _lastInputRotation = Vector3.right;

        
    }
    private void Update()
    {
        Move();
        Rotate();
        Shoot();
    }
    public void Move()
    {
        if (_InputMovement != Vector3.zero)
        {
            if(_InputRotation.x >= 0.01f)
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            else if(_InputRotation.x <= - 0.01f)
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            CharacterController.Move(_InputMovement.normalized * MovementSped * Time.deltaTime);
        }
    }

    public void Shoot()
    {
        if (_shooting && _lastShotTime + 1 / FireRate < Time.time)
        {
            _lastShotTime = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, bulletStart.position,Quaternion.identity);
            bullet.GetComponent<BulletShooter>().direction = _lastInputRotation.normalized;
            bullet.transform.right = _lastInputRotation;
        }
    }

    private void Rotate()
    {
        if (_InputRotation.x >= 0.01f)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        else if (_InputRotation.x <= -0.01f)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    public void ChangeRole()
    {
        def.enabled = true;
        this.enabled = false;
    }


    public void OnMovement(InputAction.CallbackContext value)
    {
        var inputMovement = value.ReadValue<Vector2>();
        var rawInputMovement = new Vector2(inputMovement.x, inputMovement.y);
        _InputMovement = FilterInput(rawInputMovement, InputMovementThreshold);
        _InputMovement = _InputMovement.normalized;
    }

    public void OnRotation(InputAction.CallbackContext value)
    {
        var inputRotation = value.ReadValue<Vector2>();
        var rawInputRotation = new Vector2(inputRotation.x, inputRotation.y);
        _InputRotation = FilterInput(rawInputRotation, InputRotationThreshold);
        _lastInputRotation = _InputRotation;
        if (rawInputRotation == Vector2.zero)
            _lastInputRotation = transform.right;
    }

    public void OnShoot(InputAction.CallbackContext value)
    {
        if (value.ReadValue<float>() > 0)
        {
            _shooting = true;
        }
        else
        {
            _shooting = false;
        }
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
}
