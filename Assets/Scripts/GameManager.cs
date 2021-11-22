using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public enum PlayerId
{
    Red = 0,
    Blue = 1
}
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Puck;
    [SerializeField] GameObject RedPlayer;
    [SerializeField] GameObject BluePlayer;
    [SerializeField] Agent RedAgent;
    [SerializeField] Agent BlueAgent;
    [SerializeField] GameObject RedGoal;
    [SerializeField] GameObject BlueGoal;
    [SerializeField] GameObject HalfCourt;
    [SerializeField] Text BlueScoreText;
    [SerializeField] Text RedScoreText;
    Rigidbody2D rBodyPuck;
    Rigidbody2D rBodyRedPlayer;
    Rigidbody2D rBodyBluePlayer;
    Vector2 RedPlayerStartPos = new Vector2(0f, -6);
    Vector2 BluePlayerStartPos = new Vector2(0f, 6);
    Vector2 PuckStartPosRed = new Vector2(0f, -4f);
    Vector2 PuckStartPosBlue = new Vector2(0f, 4f);
    PlayerId PuckStartSide = PlayerId.Red;
    int MAX_STUCK_TIME = 5;
    int RedPlayerScore;
    int BluePlayerScore;
    float stuckStartTime;
    bool puckStuck;

    [HideInInspector]
    public int num_rounds;

    // Start is called before the first frame update
    void Start()
    {
        num_rounds = 0;
        rBodyPuck = Puck.GetComponent<Rigidbody2D>();
        rBodyRedPlayer = RedPlayer.GetComponent<Rigidbody2D>();
        rBodyBluePlayer = BluePlayer.GetComponent<Rigidbody2D>();

        // Allow puck to pass through halfcourt line and goal lines
        Physics2D.IgnoreCollision(Puck.GetComponent<Collider2D>(), RedGoal.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(Puck.GetComponent<Collider2D>(), BlueGoal.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(Puck.GetComponent<Collider2D>(), HalfCourt.GetComponent<Collider2D>());

        RedPlayerScore = 0;
        BluePlayerScore = 0;

        BlueScoreText.text = BluePlayerScore.ToString();
        RedScoreText.text =  RedPlayerScore.ToString();

        puckStuck = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckStuckedPuck();
    }

    public void Scored(PlayerId scoredPlayer){
        num_rounds += 1;
        // Keep track of scores and assign rewards
        if (scoredPlayer == PlayerId.Blue) {
            BluePlayerScore += 1;
            BlueScoreText.text = BluePlayerScore.ToString();
            BlueAgent.AddReward(1);
            RedAgent.AddReward(-1);
            PuckStartSide =  PlayerId.Red; // Losing side start the game next round
            Debug.Log("Blue Score!! Current Score: Red: " + RedPlayerScore + " Blue: " + BluePlayerScore);
        }
        else {
            RedPlayerScore += 1;
            RedScoreText.text =  RedPlayerScore.ToString();
            RedAgent.AddReward(1);
            BlueAgent.AddReward(-1);
            PuckStartSide =  PlayerId.Blue;
            Debug.Log("Red Score!! Current Score: Red: " + RedPlayerScore + " Blue: " + BluePlayerScore);
        }
        // Uncomment the following two lines for training
        // RedAgent.EndEpisode();
        // BlueAgent.EndEpisode();
        RestartLevel();
    }

    public void BlueReward(float reward) {
        BlueAgent.AddReward(reward);
    }

    public void RedReward(float reward) {
        RedAgent.AddReward(reward);
    }


    // Check if Puck is stuck
    void CheckStuckedPuck(){
        if (!puckStuck && rBodyPuck.velocity.magnitude <= 0.1) {
            puckStuck = true;
            stuckStartTime = Time.time;
        } else if (puckStuck && rBodyPuck.velocity.magnitude > 0.1) {
            puckStuck = false;
        }

        if (puckStuck && Time.time - stuckStartTime >= MAX_STUCK_TIME) {
            //Debug.Log ("PUCK IS STUCK!!!");
            RestartLevel();
        }
    }

    void RestartLevel(){
        // Losing side start the game next round
        if (PuckStartSide ==  PlayerId.Red){
            Puck.transform.localPosition = PuckStartPosRed;
        } else {
            Puck.transform.localPosition = PuckStartPosBlue;
        }
        rBodyPuck.velocity = Vector2.zero;

        RedPlayer.transform.localPosition = RedPlayerStartPos;
        rBodyRedPlayer.velocity = Vector2.zero;

        BluePlayer.transform.localPosition = BluePlayerStartPos;
        rBodyBluePlayer.velocity = Vector2.zero;

        puckStuck = false;
    }
}
