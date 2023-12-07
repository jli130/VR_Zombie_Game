using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kill : MonoBehaviour
{

    // Update is called once per framea
    void Update()
    {
        RaycastHit hitLocation;
        bool hit = Physics.Raycast(transform.position, transform.forward, out hitLocation, 10);
        var tagOfHit = hitLocation.rigidbody.tag;
        if (hit && tagOfHit == "Zombie")
        {
            hitLocation.collider.SendMessageUpwards("killZombie", hitLocation, SendMessageOptions.DontRequireReceiver);
        }
    }
}
