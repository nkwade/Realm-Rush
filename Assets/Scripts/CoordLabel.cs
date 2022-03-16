using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class CoordLabel : MonoBehaviour
{
    TextMeshPro label;
    private Vector2Int coords = new Vector2Int();
    
    private void Awake() {
        label = GetComponent<TextMeshPro>();
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
}
