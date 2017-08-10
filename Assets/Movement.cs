using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour 
{
    // Characteristics of the ball
    public Rigidbody ballRB;

    private ScorePoint scorePoint;

    // Will be determined based on difficulty Level
    float speed;

    // Construction
    private void Awake()
    {
        // TODO: Implement speed difficulty level switch 
        speed = 5f;

        ballRB = GetComponent<Rigidbody>();

        scorePoint = GetComponent<ScorePoint>();
    }

    // Initialization
    void Start()
    {
        StartCoroutine(scorePoint.resetPositions());
    }

    private void OnTriggerEnter(Collider other)
    {
        // To help us determine where the ball goes after hitting a paddle
        Vector3 newDirection = Random.onUnitSphere;

        // The ball's horizontal velocity should never be too low
        newDirection.x = Mathf.Min(newDirection.x, 0.8f);

        // The ball shouldn't have strictly vertical movement
        newDirection.y = Mathf.Clamp(newDirection.y, -0.4f, 0.4f);
        // The ball only moves in the XY plane
        newDirection.z = 0;

        // To ensure the ball always has enough speed
        newDirection.Normalize();
        
        switch (other.gameObject.tag)
        {
            case "Wall":
                ballRB.velocity = new Vector3(ballRB.velocity.x, -ballRB.velocity.y, 0f);
                break;

            case "Player": 

                // After hitting the player the ball always goes to the right
                newDirection.x = Mathf.Abs(newDirection.x);

                newDirection *= speed;
                ballRB.velocity = newDirection;
                break;

            case "Opponent": 

                // After hitting the opponent the ball always goes to the left
                newDirection.x = -1 * Mathf.Abs(newDirection.x);

                newDirection *= speed;
                ballRB.velocity = newDirection;
                break;
        }
    }
}
