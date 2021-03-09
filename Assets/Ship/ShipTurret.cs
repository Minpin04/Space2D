using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTurret : MonoBehaviour
{
    public GameObject ship;
    public float angle = 60f;
    public float range = 30f;
    public float damage = 5f;
    protected Vector2 baseDirection;


    private void Awake()
    {
        baseDirection = Vector2.right;
    }

    public virtual void ShipUpdate(Vector2 mousePos)
    {
        Fire(mousePos, Aim(mousePos));
    }

    public virtual void Fire(Vector2 mousePos, bool inZone)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Vector2.Angle(ship.transform.rotation * baseDirection, transform.right) < angle)
            {
                RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + Vector2.right, transform.right);
                if (hit.collider != null && hit.collider.GetComponent<Health>() != null)
                {
                    hit.collider.GetComponent<Health>().health -= damage;
                }
            }
        }
    }
    
    public virtual bool Aim(Vector2 mousePos)
    {
        if (Vector2.Angle(ship.transform.rotation * baseDirection, mousePos - (Vector2) transform.position) < angle)
        {
            float AngleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);

            float AngleDeg = (180 / Mathf.PI) * AngleRad;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, AngleDeg), 5f * Time.deltaTime);

            return true;
        } else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, ship.transform.rotation * Quaternion.Euler(baseDirection), 5f * Time.deltaTime);

            return false;
        }
    }
}
