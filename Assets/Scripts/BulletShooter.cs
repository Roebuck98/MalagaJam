using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public GameObject SpriteMoving;
    public GameObject SpriteSticked;
    private bool _isMoving;
    public Vector2 direction;
    public float bulletSpeed;
    private Vector2 defaultBulletDirection;
    void Awake()
    {
        _isMoving = true;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isMoving = false;
    }

    private void Move()
    {
        transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.Self);
            
    }
}
