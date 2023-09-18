using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public PlayerBehavior pb;
    public GameManager gameManager;
    public float initialSpeed = 10.0f;
    public float zSpeed = 10.0f;
    //public Vector3 initialPosition = new Vector3(0, .795, -23.458);
    public GameObject paddle;
    public float resetThreshold = -26.0f;
    //public bool hasStarted = false;



    private Rigidbody ballRb;

    void Start()
    {
        ballRb = GetComponent<Rigidbody>();


    }
    void Update()
    {
        Vector3 paddlePosition = paddle.transform.position;
        if (ballRb.transform.position.z < resetThreshold)
        {
            gameManager.LoseLife();
            ResetBall();
        }
        if (!pb.hasStarted) {
            transform.position = new Vector3(paddlePosition.x, paddlePosition.y, paddlePosition.z + .5f);
        }
        // if (!hasStarted && Input.GetButtonDown("Fire1"))
        // {
        //     ballRb.AddForce(new Vector3(0.0f, 0.0f, initialSpeed));
        //     hasStarted = true;
        // }
    }

   void OnCollisionEnter(Collision collision)
{
    //if (collision.gameObject.tag == "Paddle" || collision.gameObject.tag == "Brick")
    //{
        // Get the point of contact
    Vector3 contactPoint = collision.contacts[0].point;

    // Calculate the hit factor
    float xhitFactor = (contactPoint.x - collision.transform.position.x) / collision.collider.bounds.size.x;
    float yhitFactor = (contactPoint.y - collision.transform.position.y) / collision.collider.bounds.size.y;

    // Calculate new direction
    Vector3 dir = new Vector3(xhitFactor, yhitFactor, 1).normalized;

    // Update ball velocity while maintaining the current speed
    float currentSpeed = ballRb.velocity.magnitude;


    if (collision.gameObject.tag == "Paddle") {
        ballRb.velocity = dir * currentSpeed;
        // Ensure the ball doesn't move in the Y-axis
        ballRb.velocity = new Vector3(ballRb.velocity.x, ballRb.velocity.y, zSpeed);
    }
    else if (collision.gameObject.tag == "Brick") {
        ballRb.velocity = dir * currentSpeed;
        ballRb.velocity = new Vector3(ballRb.velocity.x, ballRb.velocity.y, ballRb.velocity.z);
    }
    else if (collision.gameObject.tag == "Back") {
        ballRb.velocity = new Vector3(ballRb.velocity.x, ballRb.velocity.y, zSpeed * -1);

    }
    else {
        ballRb.velocity = new Vector3(ballRb.velocity.x, ballRb.velocity.y, ballRb.velocity.z);
    }
}
void ResetBall()
    {
        Debug.Log("reset");
        // Reset position to be above the center of the paddle
        Vector3 paddlePosition = paddle.transform.position;
        transform.position = new Vector3(paddlePosition.x, paddlePosition.y, paddlePosition.z + .5f); // +1 can be adjusted based on your needs

        // Reset velocity
        ballRb.velocity = Vector3.zero;

        // Reset the 'hasStarted' flag so the ball can be launched again
        pb.hasStarted = false;
    }

}

