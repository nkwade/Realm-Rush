using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;

    private int curHitPoints;

    // Start is called before the first frame update
    void OnEnable()
    {
        curHitPoints = maxHitPoints;
    }

    private void OnParticleCollision(GameObject other) {
        ProcessHit();
    }

    private void ProcessHit()
    {
        curHitPoints--;
        if (curHitPoints <= 0) {
            gameObject.SetActive(false);
        }
    }
}
