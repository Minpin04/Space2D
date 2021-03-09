using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public float baseSpeed = 5f;

    public GameObject sprite;

    public bool canMove = true;

    private Ship ship;
    private Vector2 velocity;
    private Controller2D controller2D;
    private CameraControl cameraControl;

    void Start()
    {
        cameraControl = Camera.main.GetComponent<CameraControl>();
        cameraControl.target = gameObject;
        controller2D = GetComponent<Controller2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactable interactable = Interact();

            if (interactable != null)
            {
                ShipControl shipControl;
                if (interactable.TryGetComponent(out shipControl))
                {
                    if (ship == null)
                    {
                        SetShipControl(shipControl.ship);
                    }
                    else
                    {
                        SetPlayerControl();
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (canMove && ship == null)
        {
            Movement();
        }
        else if (ship != null)
        {
            ship.Move(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
            ship.Turrets();
        }
    }

    void Movement()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0f;

        float AngleRad = Mathf.Atan2(worldPosition.y - sprite.transform.position.y, worldPosition.x - sprite.transform.position.x);

        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        sprite.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * baseSpeed * Time.deltaTime;

        controller2D.Move(velocity);
    }

    Interactable Interact()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Camera.main.transform.forward);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                return hit.collider.GetComponent<Interactable>();
            }
        }
        return null;
    }

    void SetPlayerControl()
    {
        ship = null;
        cameraControl.SetZoom(5f);
        cameraControl.target = gameObject;
    }

    void SetShipControl(Ship ship)
    {
        cameraControl.SetZoom(20f);
        cameraControl.target = ship.gameObject;
        this.ship = ship;
    }
}
