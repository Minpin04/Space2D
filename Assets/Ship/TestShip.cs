using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShip : MonoBehaviour
{
    Ship ship;
    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Ship>();
    }

    // Update is called once per frame
    void Update()
    {
        ship.Move(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }
}
