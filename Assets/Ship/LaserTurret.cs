using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : ShipTurret
{
    public float fireRate = 0.5f;
    public Transform barrel;
    float chargeUpTime = 0f;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }

    public override void Fire(Vector2 mousePos, bool inZone)
    {
        lineRenderer.SetPosition(1, Vector3.zero);

        if (!inZone)
            return;
        RaycastHit2D hit = Physics2D.Raycast(barrel.transform.position, transform.right);

        if (Input.GetMouseButton(0))
        {
            chargeUpTime += Time.deltaTime;
            if (hit.collider != null)
            {
                lineRenderer.SetPosition(1, transform.InverseTransformPoint(hit.point));
                Debug.DrawRay(hit.point, Vector2.up);
            }
            else
            {
                lineRenderer.SetPosition(1, Vector3.right * range);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            chargeUpTime = 0f;
        }

        if (chargeUpTime > 1 / fireRate)
        {
            if (Vector2.Angle(ship.transform.rotation * baseDirection, transform.right) < angle)
            {
                if (hit.collider != null && hit.collider.GetComponent<Health>() != null)
                {
                    hit.collider.GetComponent<Health>().health -= damage;
                }
            }
            chargeUpTime = 0f;
        }
    }
}
