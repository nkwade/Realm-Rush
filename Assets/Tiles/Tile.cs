using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable = false;
    [SerializeField] Tower towerPrefab;

    public bool IsPlaceable { get {return isPlaceable;} }
    
    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coords = new Vector2Int();

    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void Start() {
        if (gridManager != null) {
            coords = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable) {
                gridManager.BlockNode(coords);
            }
        }
    }

    private void OnMouseDown() {
        if (gridManager.GetNode(coords).isWalkable && !pathFinder.WillBlockPath(coords)) {
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);
            if (isSuccessful) {
                gridManager.BlockNode(coords);
                pathFinder.NotifyReceivers();
            }
        }
    }
}
