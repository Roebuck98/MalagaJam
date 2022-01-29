using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BasicEnemyAI : MonoBehaviour
{
    public enum AIState
    {
        Following, Thinking, Shooting
    }
    public AIState currentState;

    public float movementSpeed;

    public Transform target;
    public float nextWaypointDistance;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    public float timer;
    public float waitTimeUntilNextShot;
    public LayerMask obstacleLayerMask;
    private void Start()
    {
        seeker = GetComponent<Seeker>();
    }
    private void Update()
    {
        UpdatePath();
        switch (currentState)
        {
            case AIState.Following:
                Follow();
                break;
            case AIState.Thinking:
                Think();
                break;
            case AIState.Shooting:
                Shoot();
                break;
            default:
                break;
        }
    }

    public void Follow()
    {
        if (!Physics2D.Raycast(transform.position, target.position, obstacleLayerMask))
        {
            Debug.Log("Linea de vision");
            currentState = AIState.Shooting;
            return;
        }
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
        Vector2 force = direction * movementSpeed * Time.deltaTime;
        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        transform.position += new Vector3(force.x, force.y, 0);
    }
    public void Shoot()
    { 
        currentState = AIState.Thinking;
        Debug.Log("Disparar");
    }
    void ResetShot()
    {
        Debug.Log("Reset");
        timer = 0;
        currentState = AIState.Following;
    }
    void Think()
    {
        timer += Time.deltaTime;
        if(timer >= waitTimeUntilNextShot)
        {
            if (!Physics2D.Raycast(transform.position, target.position,obstacleLayerMask))
            {
                Debug.Log("Linea de vision");
                currentState = AIState.Shooting;
                timer = 0;
                return;
            }
            else
            {
                ResetShot();
            }        
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
