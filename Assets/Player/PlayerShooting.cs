using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public LayerMask mask;
    public GameObject gunTrail;
    public GameObject sprite;
    // Update is called once per frame
     void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        RaycastHit2D shootHit = Physics2D.Raycast(sprite.transform.position, sprite.transform.right, 100, mask);
        if (shootHit.collider != null)
        {
            Vector2 midPoint = ((Vector2)transform.position + shootHit.point) / 2;
            GameObject bulletTrail = Instantiate(gunTrail, midPoint, sprite.transform.rotation) as GameObject;
            bulletTrail.transform.localScale = new Vector3(shootHit.distance, 0.05f, 1);
        } else
        {
            Vector2 midPoint = (Vector2)(transform.position + sprite.transform.right * 25f/2);
            Debug.DrawRay(midPoint, Vector2.right);
            GameObject bulletTrail = Instantiate(gunTrail, midPoint, sprite.transform.rotation) as GameObject;
            bulletTrail.transform.localScale = new Vector3(25f, 0.05f, 1);
        }
    }
}
