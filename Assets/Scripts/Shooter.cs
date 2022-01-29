using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public LightBulletBehaviour bulletPrefab;
    public Transform target;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LightBulletBehaviour bullet = Instantiate(bulletPrefab, this.transform);
            bullet.bulletDirection = Vector2.up;
            bullet.target = target;
        }
    }
}
