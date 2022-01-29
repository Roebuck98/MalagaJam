using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    private bool _isMoving;
    public Vector2 direction;
    public float bulletSpeed;
    private Vector2 defaultBulletDirection;
    void Awake()
    {
        _isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        _isMoving = false;
    }

    private void Move()
    {
        Debug.Log(direction);
        if (_isMoving && direction != Vector2.zero) 
        {
            defaultBulletDirection = new Vector2(direction.x, direction.y);
            transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.World);
        } else if(_isMoving && direction == Vector2.zero)
        {
            transform.Translate(defaultBulletDirection * bulletSpeed * Time.deltaTime, Space.Self);
        }
            
    }
}
