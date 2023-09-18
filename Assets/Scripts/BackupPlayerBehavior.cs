using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackupPlayerBehavior : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 150;
    public Rigidbody ballRb;
    public bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //m_EulerAngleVelocity = new Vector3(0, 1000000, 0);

        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null)
        {
            ballRb = ball.GetComponent<Rigidbody>();
        }
        ballRb.AddForce(new Vector3(0.0f, 0.0f, 5.0f) * 100);
    }

    private void OnMove(InputValue movementValue)
    {

       Vector2 movementVector = movementValue.Get<Vector2>();
       movementX = movementVector.x;
       movementY = movementVector.y;


    }

    private void OnFire(InputValue value) {
        //Debug.Log("hi");
        if (!hasStarted) {
            //Debug.Log("test");
            ballRb.velocity = (new Vector3(0.0f, 0.0f, 10.0f));
            hasStarted = true;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //ballRb.AddForce(new Vector3(0.0f, 0.0f, 5.0f) * 5);
       //Debug.Log(ballRb.velocity);
        Vector3 movement = new Vector3(movementX, movementY, 0.0f);
        rb.AddForce(movement * speed);




    }
};
