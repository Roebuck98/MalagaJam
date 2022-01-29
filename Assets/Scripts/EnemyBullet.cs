using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyBullet : MonoBehaviour
{
    public Vector2 bulletDirection;
    public bool pool;
    public float movementSpeed;
    public float poolMovementSpeed;

    public Transform target;

    public float nextWaypointDistance;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;
    public float arriveDistance;
    public Vector2 movementSpeedMaxMin;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        movementSpeed = Random.Range(movementSpeedMaxMin.x, movementSpeedMaxMin.y);
    }
    private void Update()
    {
        if (pool)
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
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint + 1] - rb.position).normalized;
            Vector2 force = direction * poolMovementSpeed * Time.deltaTime;
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            transform.position += new Vector3(force.x, force.y, 0);
            if (Vector2.Distance(transform.position, target.position) <= arriveDistance)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Vector3 direction = new Vector3(bulletDirection.x, bulletDirection.y, 0) * Time.deltaTime * movementSpeed;
            transform.position += direction;
        }
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
            Destroy(this.gameObject);
    }
}
