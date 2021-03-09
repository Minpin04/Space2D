using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingNode : Node
{
    private bool canStartWalking = false;
    public CrossingNode(bool _walkable, Vector2 _worldPos, int _gridX, int _gridY, GameObject ship) : base(_walkable, _worldPos, _gridX, _gridY, ship)
    {

    }
}
