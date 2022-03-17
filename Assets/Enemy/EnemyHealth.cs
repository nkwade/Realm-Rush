using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    
    [Tooltip("Adds amounts to max hit points when enemy dies.")]
    [SerializeField] int diffRamp = 1;

    private Enemy enemy;
    private int curHitPoints;

    private void Start() {
        enemy = GetComponent<Enemy>();
    }

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
            enemy.AddReward();
            maxHitPoints += diffRamp;
        }
    }
}
