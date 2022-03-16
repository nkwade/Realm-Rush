using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable = false;
    [SerializeField] Tower towerPrefab;

    public bool IsPlaceable { get {return isPlaceable;} }
    

    private void OnMouseDown() {
        if (isPlaceable) {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlaceable = !isPlaced;
        }
    }
}
