using UnityEngine;
using Pathfinding;

public class BulletShooter : MonoBehaviour
{
    public GameObject SpriteMoving;
    public GameObject SpriteSticked;
    public bool SetChildToCollision = false;
    public bool IsSticky = true;
    private bool _isMoving;
    public Vector2 direction;
    public float bulletSpeed;
    private Vector2 defaultBulletDirection;
    public int BulletDamage = 50;
    public float nextWaypointDistance;
    public bool pool;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    public Transform target;

    void Awake()
    {
        _isMoving = true;
        seeker = GetComponent<Seeker>();
    }

    void FixedUpdate()
    {
        if(!pool)
            Move();
    }
    private void Update()
    {
        if(pool)
        {
            UpdatePath();
            if (path == null)
                return;
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint + 1] - (Vector2)transform.position).normalized;
            Vector2 force = direction * bulletSpeed * Time.deltaTime;
            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            transform.position += new Vector3(force.x, force.y, 0);
            if (Vector2.Distance(transform.position, target.position) <= 0.6f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsSticky)
        {
            _isMoving = false;
            SpriteMoving.SetActive(false);
            SpriteSticked.SetActive(true);
            var normal = collision.contacts[0].normal;
            transform.right = -new Vector3(normal.x, normal.y, 0);
            if (SetChildToCollision)
            {
                transform.parent = collision.transform;
            }
        }
        else
        {
            Destroy(gameObject);
        }

        if(!pool)
        {
            var characterHealth = collision.gameObject.GetComponent<CharacterHealth>();
            characterHealth?.TakeDamage(BulletDamage);
        }
    }

    private void Move()
    {
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
    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(transform.position, target.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
