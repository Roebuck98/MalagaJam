using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public GameObject SpriteMoving;
    public GameObject SpriteSticked;
    public bool SetChildToCollision = false;
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
        SpriteMoving.SetActive(false);
        SpriteSticked.SetActive(true);
        var normal = collision.contacts[0].normal;
        transform.right = - new Vector3(normal.x, normal.y, 0);
        if (SetChildToCollision)
        {
            transform.parent = collision.transform;
        }
    }

    private void Move()
    {
        Debug.Log(direction);
        if (_isMoving && direction != Vector2.zero)
        {
            defaultBulletDirection = new Vector2(direction.x, direction.y);
            transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.World);
        }
        else if (_isMoving && direction == Vector2.zero)
        {
            transform.Translate(defaultBulletDirection * bulletSpeed * Time.deltaTime, Space.World);
        }

    }
}
