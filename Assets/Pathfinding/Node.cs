using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Node
{
    public GameObject ship;
    public bool walkable;
    private Vector2 localPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;

    public Node(bool _walkable, Vector2 _worldPos, int _gridX, int _gridY, GameObject ship)
    {
        walkable = _walkable;
        localPosition = _worldPos - (Vector2) ship.transform.position;
        gridX = _gridX;
        gridY = _gridY;
        this.ship = ship;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public Vector2 getWorldPositon()
    {
        return ship.transform.position + ship.transform.rotation * localPosition;
    }

    public Vector2 getLocalPositon()
    {
        return localPosition;
    }
}