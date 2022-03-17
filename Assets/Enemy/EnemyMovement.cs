using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] [Range(0f,10f)] float speed = 1f; 

    List<Node> path = new List<Node>();

    private Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;

    // Start is called before the first frame update
    private void OnEnable() {
        ReturnToStart();
        RecalculatePath(true);
    }

    private void Awake() {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void Start() {
        enemy = GetComponent<Enemy>();
    }

    private void RecalculatePath(bool resetPath) {
        Vector2Int coords = new Vector2Int();
        if (resetPath) {
            coords = pathFinder.StartCoords;
        } else {
            coords = gridManager.GetCoordsFromPos(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coords);
        StartCoroutine(FollowPath());   
    }
    
    private void ReturnToStart() {
        transform.position = gridManager.GetPosFromCoords(pathFinder.StartCoords);
    }

    private IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++) {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPosFromCoords(path[i].coords);
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
