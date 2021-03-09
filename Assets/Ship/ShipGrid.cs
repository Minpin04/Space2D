using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipGrid : Grid
{
    public LayerMask unwalkableMask;

    protected override void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 worldBottomLeft = (Vector2)transform.position - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                Collider2D collider = Physics2D.OverlapBox(worldPoint, Vector2.one * nodeRadius, 0f, unwalkableMask);
                grid[x, y] = new Node(collider == null, worldPoint, x, y, gameObject);
            }
        }
    }
}