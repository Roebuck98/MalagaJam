using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float ExplosionMaxSize = 5f;
    public float ExplosionTime = 4f;
    public float DamageRadius = 2.7f;
    public LayerMask EnemyLayer;
    public int ExplosionDamage = 100;
    public AudioSource ExplosionSound;

    private float _time;
    private float _growRate;
    private float _initialSize;

    private void Start()
    {
        _time = 0;
        _initialSize = transform.localScale.x;
    }

    void Update()
    {
        if(_time == 0)
        {
            ExplosionSound.Play();
        }
        _time += Time.deltaTime;
        var size = Mathf.Lerp(_initialSize, ExplosionMaxSize, _time / ExplosionTime);
        transform.localScale = new Vector3(size, size, 0);

        if(size >= ExplosionMaxSize)
        {
            HitEnemies();
            Destroy(gameObject);
        }
    }

    private void HitEnemies()
    {
        var enemies = Physics2D.OverlapCircleAll(transform.position, DamageRadius).Where(x => EnemyLayer == (EnemyLayer | (1 << x.gameObject.layer)));
        foreach(var enemy in enemies)
        {
            enemy.gameObject.GetComponent<CharacterHealth>().TakeDamage(ExplosionDamage);
        }
    }
}
