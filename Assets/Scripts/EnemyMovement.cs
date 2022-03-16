using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] float delay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPath());   
    }

    private IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path) {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(delay);
        }
    }
}
