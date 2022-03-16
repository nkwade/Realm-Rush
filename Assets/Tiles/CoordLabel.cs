using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class CoordLabel : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;

    TextMeshPro label;
    private Vector2Int coords = new Vector2Int();
    Waypoint waypoint;
    
    private void Awake() {
        label.enabled = false;
        label = GetComponent<TextMeshPro>();
        waypoint = GetComponentInParent<Waypoint>();
        DisplayCoords();
        UpdateName();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying) {
            DisplayCoords();
            UpdateName();
        }
        ColorCoords();
        ToggleLabels(); 
    }


    private void DisplayCoords()
    {
        coords.x = Mathf.RoundToInt(transform.position.x / UnityEditor.EditorSnapSettings.move.x);
        coords.y = Mathf.RoundToInt(transform.position.z / UnityEditor.EditorSnapSettings.move.z);
        label.text = $"{coords.x},{coords.y}";
    }

    private void UpdateName() {
        transform.parent.name = $"({coords.x},{coords.y})";
    }
    
    private void ColorCoords()
    {
        if (waypoint.IsPlaceable) {
            label.color = defaultColor;
        } else {
            label.color = blockedColor;
        }
    }

    private void ToggleLabels() {
        if (Input.GetKeyDown(KeyCode.C)) {
            label.enabled = !label.IsActive();
        }
    }
}
