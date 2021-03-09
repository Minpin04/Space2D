using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityMovement : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ship"))
        {
            transform.SetParent(collision.transform.parent, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ship"))
        {
            transform.parent = null;
        }
    }
}
