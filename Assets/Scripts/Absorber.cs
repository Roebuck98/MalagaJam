using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorber : MonoBehaviour
{
    public float absorbRadius;
    public float absorbAngle;
    public LayerMask absorbLayer;
    private void Update()
    {
        Absorb();
    }
    public void Absorb()
    {
        Collider2D[] bullets = Physics2D.OverlapCircleAll(transform.position, absorbRadius,absorbLayer);
        if(bullets.Length > 0)
        Debug.Log(bullets.Length);
        foreach (Collider2D collider in bullets)
        {
            Debug.Log(Vector3.Angle(transform.right, (collider.transform.position - transform.position).normalized));
            if (Vector3.Angle(transform.right,(collider.transform.position - transform.position).normalized) <= absorbAngle)
            {
                
                collider.GetComponent<LightBulletBehaviour>().pool = true;
                collider.GetComponent<LightBulletBehaviour>().target = transform;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision.GetComponent<LightBulletBehaviour>().pool = true;
        //collision.GetComponent<LightBulletBehaviour>().target = transform;
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, absorbRadius);
    }
}
