using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Ship ship;
    public float speed = 1f;
    Pathfinding pathfinding;
    Vector3 nextNode;
    // Start is called before the first frame update
    void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
        pathfinding.grid = ship.GetComponent<ShipGrid>();
        if (pathfinding.seeker != null && pathfinding.target != null)
            pathfinding.FindPath(pathfinding.seeker.position, pathfinding.target.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (pathfinding.path != null && pathfinding.path.Count != 0)
        {
            Vector3 worldPosition = pathfinding.path[0].getWorldPositon();
            worldPosition.z = 0f;

            float AngleRad = Mathf.Atan2(worldPosition.y - transform.position.y, worldPosition.x - transform.position.x);

            float AngleDeg = (180 / Mathf.PI) * AngleRad;

            transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

            Vector3 movement = (worldPosition - transform.position).normalized * speed * Time.deltaTime;

            transform.position += movement;

            Debug.DrawRay(worldPosition, Vector3.right, Color.cyan);
            Debug.DrawRay(transform.position, movement.normalized, Color.green);

            if (Vector2.Distance(transform.position, pathfinding.path[0].getWorldPositon()) < 0.1f)
            {
                pathfinding.path.RemoveAt(0);
            }
        }
    }
}
