using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]
    [SerializeField]
    private float range = 15f;

    [Header("Use bullets(Default)")]
    [SerializeField]
    private float fireRate = 1f;
    private float fireCountDown;
    [SerializeField]
    private GameObject bulletPrefab;

    [Header("Use laser")]
    [SerializeField]
    private float slowFactor = 0.4f;
    [SerializeField]
    private bool useLaser = false;
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private ParticleSystem laserImpactEffect;
    [SerializeField]
    private int DamageOverTime = 30;
    private Light laserLigthing;

    [Header("Unity system")]
    [SerializeField]
    private string enemyTag = "Enemy";
    private float turnSpeed = 10f;
    [SerializeField]
    private Transform partToRotate;
    [SerializeField]
    private Transform firePoint;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); // UpdateTarget is pretty heavy, so instead of calling it 60 per second, we will use this

        if (useLaser)
            laserLigthing = laserImpactEffect.GetComponentInChildren<Light>();
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach(var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < nearestDistance)
            {
                nearestEnemy = enemy;
                nearestDistance = distanceToEnemy;
            }
        }

        if (nearestEnemy != null && nearestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        } else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserImpactEffect.Stop();
                    laserImpactEffect.GetComponentInChildren<Light>().enabled = false;
                }
            }
            return; 
        }


        LockOnTarget();
        //set rotation to PartToRotate in inspector

        if (useLaser)
        {
            Laser();
        } else
        {
            if (fireCountDown <= 0f)
            {
                Shoot();
                fireCountDown = 1 / fireRate;
            }

            fireCountDown -= Time.deltaTime;
        }
    }

    private void Laser()
    {
        targetEnemy.TakeDamage(DamageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowFactor);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            laserImpactEffect.Play();
            laserLigthing.enabled = true;// may cause bag //getting component every frame not cheap operation cache it
        }

        Vector3 dir = firePoint.position - target.position;

        laserImpactEffect.transform.rotation = Quaternion.LookRotation(dir);
        laserImpactEffect.transform.position = target.position + dir.normalized; // not *.5f because radius is one

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
    }

    private void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bulletGO.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetTarget(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
