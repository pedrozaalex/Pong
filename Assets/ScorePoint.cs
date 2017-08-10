using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePoint : MonoBehaviour
{
    int playerOneScore = 0, playerTwoScore = 0;

    public float speed;

    // For keeping track of the ball's position
    Transform ball;
    public Rigidbody ballRB;

    public Text PlayerOneScoreText;
    public Text PlayerTwoScoreText;
    public Text CountdownText;

    private void Awake()
    {
        // TODO: Implement speed difficulty level switch 
        speed = 5f;
        CountdownText.text = "";
    }

    private void Start()
    {
        if (CompareTag("Ball"))
        {
            ball = GetComponent<Transform>();
        }
        else
            ball = null;

        PlayerOneScoreText.color = Color.white;
        PlayerTwoScoreText.color = Color.white;

        updateScores();
    }

    private void FixedUpdate()
    {
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("PlayerOneWinCondition"))
        {
            ++playerOneScore;
            updateScores();
            StartCoroutine(resetPositions());

        }
        else if (collider.CompareTag("PlayerTwoWinCondition"))
        {
            ++playerTwoScore;
            updateScores();
            StartCoroutine(resetPositions());
        }


    }

    private void updateScores()
    {
        // As implied by the name, simply changes sends the values contained
        // in the playerXScore variables to the objects player playerXScoreText
        PlayerOneScoreText.text = playerOneScore.ToString();
        PlayerTwoScoreText.text = playerTwoScore.ToString();
    }

    public IEnumerator resetPositions()
    {
        StartCoroutine(startCountdown());
        yield return new WaitForSecondsRealtime(3);
        // For randomizing values with only two possible values
        bool rand = (Random.value > 0.5);

        // The ball should always start either at the top or at the bottom
        if (rand)
            ballRB.position = new Vector3(0, 4.8f, 0);
        else
            ballRB.position = new Vector3(0, -4.8f, 0);


        /*
         * Generates the velocity the ball should have at the start.
         * 
         * The X component will always be the same, only the Y component 
         * will vary from match to match. The speed value will be adjusted 
         * according to the difficulty level selected. 
         * 
         * The direction and rand values randomize whether the ball 
         * goes left or right at the start.
         */


        int direction;
        if (rand)
            direction = -1;
        else
            direction = 1;

        /*
         * My requirements for the ball's initial speed were:
         * # Horizontal velocity component always equal to prevent matches
         * in which the ball takes too long to travel between each racket
         * 
         * # In every match the ball should start with the same velocity vector magnitude
         */

        ballRB.velocity = Vector3.left * direction + Vector3.up * Random.Range(-1f, 1f);
        ballRB.velocity.Normalize();
        ballRB.velocity *= speed;
    }

    private IEnumerator startCountdown()
    {
        CountdownText.text = "3";
        yield return new WaitForSeconds(0.75f);
        CountdownText.text = "2";
        yield return new WaitForSeconds(0.75f);
        CountdownText.text = "1";
        yield return new WaitForSeconds(0.75f);
        CountdownText.text = "GO!";
        yield return new WaitForSeconds(0.5f);
        CountdownText.text = "";
    }
}
