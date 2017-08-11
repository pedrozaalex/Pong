using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePoint : MonoBehaviour
{
    private int PlayerOneScore = 0, PlayerTwoScore = 0;

    // This will aid in sending the ball towards the loser
    private int LastWinner = 0;

    public float Speed;

    // For keeping track of the ball's position
    public Rigidbody BallRB;

    public Text PlayerOneScoreText;
    public Text PlayerTwoScoreText;
    public Text CountdownText;

    private void Awake()
    {
        // TODO: Implement speed difficulty level switch 
        Speed = 5f;
        CountdownText.text = "";
    }

    private void Start()
    {
        if (CompareTag("Ball"))
        {
            BallRB = GetComponent<Rigidbody>();
        }
        else

        PlayerOneScoreText.color = Color.white;
        PlayerTwoScoreText.color = Color.white;

        updateScores();
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("PlayerOneWinCondition"))
        {
            ++PlayerOneScore;
            LastWinner = 1;
            updateScores();
            StartCoroutine(resetPositions());
            
        }
        else if (collider.CompareTag("PlayerTwoWinCondition"))
        {
            ++PlayerTwoScore;
            LastWinner = 2;
            updateScores();
            StartCoroutine(resetPositions());
        }


    }

    private void updateScores()
    {
        // As implied by the name, simply changes sends the values contained
        // in the playerXScore variables to the objects player playerXScoreText
        PlayerOneScoreText.text = PlayerOneScore.ToString();
        PlayerTwoScoreText.text = PlayerTwoScore.ToString();
    }

    public IEnumerator resetPositions()
    {
        StartCoroutine(startCountdown());
        yield return new WaitForSecondsRealtime(3);
        // For randomizing values with only two possible values
        bool rand = (Random.value > 0.5);

        // The ball should always start either at the top or at the bottom
        if (rand)
            BallRB.position = new Vector3(0, 4.8f, 0);
        else
            BallRB.position = new Vector3(0, -4.8f, 0);


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


        int direction = 300;
        

        /*
         * My requirements for the ball's initial speed were:
         * # Horizontal velocity component always equal to prevent matches
         * in which the ball takes too long to travel between each racket
         * 
         * # In every match the ball should start with the same velocity vector magnitude
         * 
         * # The ball should go towards the loser, but randomly at the start
         */

        if ((PlayerOneScore == 0) && (PlayerTwoScore == 0))
        {
            if (rand)
            direction = -1;
        else
            direction = 1;
        }
        else
        {
            if (LastWinner == 1)
                // Points right when multiplied by Vector3.right
                direction = 1;
            if (LastWinner == 2)

                // Points left when multiplied by Vector3.right
                direction = -1;
        }

        BallRB.velocity = Vector3.right * direction + Vector3.up * Random.Range(-1f, 1f);
        BallRB.velocity.Normalize();
        BallRB.velocity *= Speed;
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
