using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f,10f)] float speed = 1f; 

    private Enemy enemy;

    // Start is called before the first frame update
    private void OnEnable() {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());   
    }

    private void Start() {
        enemy = GetComponent<Enemy>();
    }

    private void FindPath() {
        path.Clear();

        GameObject paths = GameObject.FindGameObjectWithTag("Path");

        for (int i = 0; i < paths.transform.childCount; i++) {
            path.Add(paths.transform.GetChild(i).GetComponent<Waypoint>());
        }
    }
    
    private void ReturnToStart() {
        transform.position = path[0].transform.position;
    }

    private IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path) {
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;
            float travelPerc = 0f;
            
            transform.LookAt(endPos);

            while (travelPerc < 1f) {
                travelPerc += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPerc);
                yield return new WaitForEndOfFrame();
            }
           
        }

        EndOfPath();
    }

    private void EndOfPath()
    {
        gameObject.SetActive(false);
        enemy.RemoveReward();
    }
}
