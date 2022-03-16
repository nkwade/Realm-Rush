using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem bullets;
    [SerializeField] float range = 15;
    
    private Transform target;

    private void Update() {
        FindClosestTarget();
        AimWeapon();
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDist = Mathf.Infinity;

        foreach (Enemy enemy in enemies) {
            float targDis = Vector3.Distance(transform.position, enemy.transform.position);

            if (targDis < maxDist) {
                closestTarget = enemy.transform;
                maxDist = targDis;
            }
        }

        target = closestTarget;
    }

    private void AimWeapon()
    {
        float targDis = Vector3.Distance(transform.position, target.position);

        weapon.LookAt(target);

        if (targDis <= range) {
            Attack(true);
        } else {
            Attack(false);
        }
    }

    private void Attack(bool isActive) {
        var emmisionMod = bullets.emission;
        emmisionMod.enabled = isActive;
        
    }
}
