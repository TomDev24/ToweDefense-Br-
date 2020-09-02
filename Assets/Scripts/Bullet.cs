using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject impactEffect;
    private Transform target;

    [SerializeField]
    private int damage = 50;

    [SerializeField]
    private float speed = 70f;

    [SerializeField]
    private float explosionRadius = 0f; 

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        var dir = target.position - transform.position;
        float distanceThisFrame = Time.deltaTime * speed;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target); // for missile
    }

    private void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        if (explosionRadius > 0f)
        {
            Explode();
        } else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(var obj in hitObjects)
        {
            if(obj.tag =="Enemy")
            {
                Damage(obj.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy enemyMovement = enemy.GetComponent<Enemy>();

        if (enemyMovement != null)
            enemyMovement.TakeDamage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
