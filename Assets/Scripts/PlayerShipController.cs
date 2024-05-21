using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    public float speed = 4;
    public float angularSpeed = 2;

    private float verticalInput;
    private float horizontalInput;

    public bool shipCanMove;

    public GameObject player;

    Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        shipCanMove = player.GetComponent<PlayerController>().playerIsOnShip;
    }

    private void Update()
    {
        //Input
        if (shipCanMove) 
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");
        }
        
    }

    void FixedUpdate()
    {
        //On applique la velocité au rigidbody pour avancer

        Vector3 velocity = new Vector3(-(verticalInput * speed), rb.velocity.y, 0);
        rb.velocity = rb.transform.TransformDirection(velocity);

        //On applique la rotation
        Vector3 angularVel = new Vector3(0, horizontalInput * angularSpeed, 0);
        rb.angularVelocity = angularVel;
    }
}