using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemAgent : MonoBehaviour
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


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        
    }
    private void Update()
    {
        Move();
        Shoot();
    }
    public void Move()
    {
        if (_InputMovement != Vector3.zero)
        {
            CharacterController.Move(_InputMovement.normalized * MovementSped * Time.deltaTime);
        }
    }

    public void Shoot()
    {
        if (_shooting && _lastShotTime + 1 / FireRate < Time.time)
        {
            _lastShotTime = Time.time;
            Instantiate(bulletPrefab, bulletStart);
        }
    }

    /*private void Move()
    {
        if (_InputMovement != Vector3.zero)
        {
            CharacterController.Move(_InputMovement.normalized * MovementSped * Time.deltaTime);
        }
    }*/

    public void OnMovement(InputAction.CallbackContext value)
    {
        var inputMovement = value.ReadValue<Vector2>();
        var rawInputMovement = new Vector2(inputMovement.x, inputMovement.y);
        _InputMovement = FilterInput(rawInputMovement, InputMovementThreshold);
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
