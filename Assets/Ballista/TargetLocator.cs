using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    
    private Transform target;

    private void Start() {
        target = FindObjectOfType<EnemyMovement>().transform;
    }

    private void Update() {
        AimWeapon();
    }

    private void AimWeapon()
    {
        weapon.LookAt(target);
    }
}
