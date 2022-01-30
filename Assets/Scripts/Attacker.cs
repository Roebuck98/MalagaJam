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
    public Vector3 _InputRotation;
    public Vector3 _lastInputRotation;

    public static float bulletsToShoot;

    public Animator anim;
    public SoundManager sound;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        def = GetComponent<Defender>();
        _lastInputRotation = Vector3.right;
        bulletsToShoot = 10;
        MainSceneUiManager.instance.UpdateBulletBar();
        sound = GetComponent<SoundManager>();
    }
    private void Start()
    {
        anim.SetBool("Attacking", true);
    }
    private void Update()
    {
        Rotate();
        Shoot();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            BasicEnemyAI[] enemies = GameObject.FindObjectsOfType<BasicEnemyAI>();
            foreach (var enemy in enemies)
            {
                Destroy(enemy.gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        if (_InputMovement != Vector3.zero)
        {
            anim.SetBool("Walking", true);
            if(_InputRotation.x >= 0.01f)
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            else if(_InputRotation.x <= - 0.01f)
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            transform.Translate(_InputMovement.normalized * MovementSped * Time.fixedDeltaTime, Space.World);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    public void Shoot()
    {
        if (_shooting && _lastShotTime + 1 / FireRate < Time.time && bulletsToShoot > 0)
        {
            anim.SetTrigger("Attacked");
            _lastShotTime = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, bulletStart.position, bulletStart.rotation);
            bullet.GetComponent<BulletShooter>().direction = _lastInputRotation.normalized;
            bullet.transform.right = _lastInputRotation;
            bulletsToShoot--;
            MainSceneUiManager.instance.UpdateBulletBar();
            sound.PlayShot();
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
        enabled = true;
        anim.SetBool("Attacking", false);
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
