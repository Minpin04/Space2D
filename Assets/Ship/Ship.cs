using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public bool canMove = true;

    public float verticalInputAcceleration = 1;
    public float horizontalInputAcceleration = 20;

    public float maxSpeed = 10;
    public float maxRotationSpeed = 100;

    public float velocityDrag = 1;
    public float rotationDrag = 1;

    private Vector3 velocity;
    private float zRotationVelocity;
    private Rigidbody2D rb;
    public List<ShipTurret> guns = new List<ShipTurret>();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        foreach (ShipTurret gun in guns)
        {
            gun.ship = gameObject;
        }
    }

    public void Move(Vector3 movement)
    {
        if (canMove)
        {
            // apply forward input
            Vector3 acceleration = movement.y * verticalInputAcceleration * transform.up;
            velocity += acceleration * Time.deltaTime;

            // apply turn input
            float zTurnAcceleration = -1 * movement.x * horizontalInputAcceleration;
            zRotationVelocity += zTurnAcceleration * Time.deltaTime;
        }
    }

    public void Turrets()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        foreach (ShipTurret gun in guns)
        {
            gun.ShipUpdate(worldPosition);
        }
    }

    private void FixedUpdate()
    {
        // apply velocity drag
        velocity = velocity * (1 - Time.deltaTime * velocityDrag);

        // clamp to maxSpeed
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // apply rotation drag
        zRotationVelocity = zRotationVelocity * (1 - Time.deltaTime * rotationDrag);

        // clamp to maxRotationSpeed
        zRotationVelocity = Mathf.Clamp(zRotationVelocity, -maxRotationSpeed, maxRotationSpeed);

        if (canMove)
        {
            // update transform
            transform.position += velocity * Time.deltaTime;
            transform.Rotate(0, 0, zRotationVelocity * Time.deltaTime);
        }
    }
}