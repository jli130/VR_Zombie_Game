using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public CharacterController player;
    public Transform headset;
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var joystickAxis = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick, OVRInput.Controller.RTouch);
        Vector3 position = (transform.right * joystickAxis.x + transform.forward * joystickAxis.y) * speed * Time.deltaTime;
        player.Move(position);
    }

}
