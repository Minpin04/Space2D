using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public bool viewGrid = false;

    public Vector2 gridWorldSize;
    public float nodeRadius = 1;
    protected Node[,] grid;

    protected float nodeDiameter;
    protected int gridSizeX, gridSizeY;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }
    protected virtual void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 worldBottomLeft = (Vector2)transform.position - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Camera.main.transform.forward);
                bool obstacle = hit.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle");
                grid[x, y] = new Node(!obstacle, worldPoint - (Vector2) transform.position, x, y, gameObject);
            }
        }
    }



    public virtual List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                //if (x == 1 && y == 1)
                //    continue;
                //if (x == -1 && y == -1)
                //    continue;
                //if (x == 1 && y == -1)
                //    continue;
                //if (x == -1 && y == 1)
                //    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }


    public virtual Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    public virtual Node WalkableNodeFromWorldPoint(Vector3 worldPosition)
    {
        Node nodePoint = NodeFromWorldPoint(worldPosition);

        List<Node> surroundingNodes = new List<Node>();

        for(int x = -1; x < 1; x++)
        {
            for (int y = -1; y < 1; y++)
            {
                surroundingNodes.Add(grid[nodePoint.gridX + x, nodePoint.gridY + y]);
            }
        }

        surroundingNodes.OrderBy(n => (Vector2.Distance(n.getWorldPositon(), worldPosition)));

        for(int i =0; i < surroundingNodes.Count; i++)
        {
            if (surroundingNodes[i].walkable)
            {
                return surroundingNodes[i];
            }
        }

        return null;
    }

    protected virtual void OnDrawGizmos()
    {
        if (viewGrid)
        {
            Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));

            if (grid != null)
            {
                foreach (Node n in grid)
                {
                    Gizmos.color = (n.walkable) ? Color.white : Color.red;
                    Gizmos.DrawCube(n.getWorldPositon(), Vector2.one * (nodeDiameter - .1f));
                }
            }
        }
    }
}
