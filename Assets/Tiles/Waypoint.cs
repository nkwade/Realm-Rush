using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable = false;
    [SerializeField] GameObject towerPrefab;

    public bool IsPlaceable { get {return isPlaceable;} }

    private void OnMouseDown() {
        if (isPlaceable) {
            Instantiate(towerPrefab, transform.position, Quaternion.identity);
            isPlaceable = false;
        }
    }
}
