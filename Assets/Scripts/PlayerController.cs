using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public float angularSpeed = 0.5f;

    private float verticalInput;
    private float horizontalInput;

    public float sensitivity = 1f;
    public float jumpForce = 200f;

    public bool playerIsOnShip = false;
    public bool isOnGround;

    public Transform floorDetector;
    public LayerMask layerGround;

    public Camera playerCam;
    Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Input
        if (!playerIsOnShip) 
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");

            /////////////////////////////////////////////////////////////////////////////////////////////
            float newRotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
            //float newRotationX = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * sensitivity;

            gameObject.transform.localEulerAngles = new Vector3(0f, newRotationY);
            /////////////////////////////////////////////////////////////////////////////////////////////
            
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                isOnGround = Physics.CheckSphere(floorDetector.position, 0.1f, layerGround);

                if (isOnGround) 
                {
                    //Rajouter ForceMode.Impulse en 2ème param
                    Vector3 force = new Vector3(0f, jumpForce, 0f);
                    rb.AddForce(force, ForceMode.Impulse);
                } 
            }

        }



    }

    void FixedUpdate()
    {
        //On applique la velocité au rigidbody pour avancer
        Vector3 velocity = new Vector3(horizontalInput * speed, rb.velocity.y, verticalInput * speed);
        rb.velocity = rb.transform.TransformDirection(velocity);
    }
}
