using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordLabel : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);

    TextMeshPro label;
    private Vector2Int coords = new Vector2Int();
    //Waypoint waypoint;
    GridManager gridManager;

    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        //waypoint = GetComponentInParent<Waypoint>();
        DisplayCoords();
        UpdateName();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying) {
            DisplayCoords();
            UpdateName();
            label.enabled = true;
        }   
        SetLabelColor();
        ToggleLabels(); 
    }


    private void DisplayCoords()
    {
        if (gridManager == null) {return;}
        coords.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coords.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        label.text = $"{coords.x},{coords.y}";
    }

    private void UpdateName() {
        transform.parent.name = $"({coords.x},{coords.y})";
    }
    
    private void SetLabelColor()
    {
        if (gridManager == null) {return;}

        Node node = gridManager.GetNode(coords);

        if (node == null) {return;}

        if (!node.isWalkable) {
            label.color = blockedColor;
        } else if (node.isPath) {
            label.color = pathColor;
        } else if (node.isExplored) {
            label.color = exploredColor;
        } else {
            label.color = defaultColor;
        }



        // if (waypoint.IsPlaceable) {
        //     label.color = defaultColor;
        // } else {
        //     label.color = blockedColor;
        // }
    }

    private void ToggleLabels() {
        if (Input.GetKeyDown(KeyCode.C)) {
            label.enabled = !label.IsActive();
        }
    }
}
