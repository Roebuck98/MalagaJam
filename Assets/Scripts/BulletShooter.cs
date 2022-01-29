using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    private bool _isMoving;
    public Vector2 direction;
    public float bulletSpeed;
    void Start()
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
        if (_isMoving) 
        {
            transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.Self);
        }
            
    }
}
