using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    [Header("Parameters")]
    [SerializeField] float fireRate = 1f;
    [SerializeField] float bulletSpeed = 20f;

    float fireCooldown;

    void FixedUpdate()
    {
        fireCooldown -= Time.fixedDeltaTime;

        Transform target = GetClosestEnemy();

        if (target != null && fireCooldown <= 0f)
        {
            Shoot(target);
            fireCooldown = 1f / fireRate;
        }
    }

    Transform GetClosestEnemy()
    {
        Transform bestTarget = null;
        float closestDist = Mathf.Infinity;

        foreach (Transform enemy in EnemyManager.Enemies)
        {
            if (enemy == null) continue;

            float dist = Vector2.Distance(transform.position, enemy.position); 
            if (dist < closestDist)
            {
                closestDist = dist;
                bestTarget = enemy;
            }
        }
        return bestTarget;
    }

    void Shoot(Transform target)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Vector2 dir = (target.position - firePoint.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = dir * bulletSpeed;
        }
    }
}