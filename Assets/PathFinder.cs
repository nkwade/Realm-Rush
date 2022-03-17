using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoords;
    public Vector2Int StartCoords {get {return startCoords;}}
    [SerializeField] Vector2Int destinationCoords;
    public Vector2Int DestinationCoords {get {return destinationCoords;}}

    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Node startNode; 
    Node destinationNode;
    Node currSearchNode;

    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();

    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();

        if (gridManager != null) {
            grid = gridManager.Grid;
            startNode = grid[startCoords];
            destinationNode = grid[destinationCoords];
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetNewPath();
    }

    private void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach(Vector2Int direction in directions) {
            Vector2Int neigborCoords = currSearchNode.coords + direction;

            if (grid.ContainsKey(neigborCoords)) {
                neighbors.Add(grid[neigborCoords]);
            }
        }

        foreach(Node neighbor in neighbors) {
            if (!reached.ContainsKey(neighbor.coords) && neighbor.isWalkable) {
                neighbor.connectedTo = currSearchNode;
                reached.Add(neighbor.coords, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    private void BreadthFirstSearch(Vector2Int coords) {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coords]);
        reached.Add(coords, grid[coords]);

        while (frontier.Count > 0 && isRunning) {
            currSearchNode = frontier.Dequeue();
            currSearchNode.isExplored = true;
            ExploreNeighbors();
            if (currSearchNode.coords == destinationCoords) {isRunning = false;}
        }
    }

    List<Node> BuildPath() {
        List<Node> path = new List<Node>();
        Node currNode = destinationNode;

        path.Add(currNode);
        currNode.isPath = true;

        while (currNode.connectedTo != null) {
            currNode = currNode.connectedTo;
            path.Add(currNode);
            currNode.isPath = true;
        }

        path.Reverse();
        return path;
    }

    public List<Node> GetNewPath() {
        return GetNewPath(startCoords);
    }

    public List<Node> GetNewPath(Vector2Int coords) {
        gridManager.ResetNodes();
        BreadthFirstSearch(coords);
        return BuildPath();
    }

    public bool WillBlockPath(Vector2Int coords) {
        if (grid.ContainsKey(coords)) {
            bool prevState = grid[coords].isWalkable;
            grid[coords].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coords].isWalkable = prevState;

            if (newPath.Count <= 1) {
                GetNewPath();
                return true;
            }
        }

        return false;
    }

    public void NotifyRecievers() {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
