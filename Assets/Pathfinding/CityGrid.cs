using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGrid : Grid
{
    public LayerMask unwalkable;

    public string crossing;

    protected override void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 worldBottomLeft = (Vector2)transform.position - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Camera.main.transform.forward, ~unwalkable);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.tag);
                    if (hit.collider.tag.CompareTo(crossing) == 0)
                    {
                        grid[x, y] = new CrossingNode(hit.collider != null, worldPoint, x, y, gameObject);
                    }
                    else
                    {
                        grid[x, y] = new Node(hit.collider != null, worldPoint, x, y, gameObject);
                    }
                } else
                {
                    grid[x, y] = new Node(hit.collider != null, worldPoint, x, y, gameObject);
                }
            }
        }
    }

    protected override void OnDrawGizmos()
    {
        if (viewGrid)
        {
            Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));

            if (grid != null)
            {
                foreach (Node n in grid)
                {
                    Gizmos.color = (n.walkable) ? Color.white : Color.red;
                    try
                    {
                        CrossingNode node = (CrossingNode)n;
                        Gizmos.color = Color.green;
                    }
                    catch (Exception e)
                    {

                    }
                    Gizmos.DrawCube(n.getWorldPositon(), Vector2.one * (nodeDiameter - .1f));
                }
            }
        }
    }
}
