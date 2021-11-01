using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

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
    Rigidbody2D rBodyPuck;
    Rigidbody2D rBodyRedPlayer;
    Rigidbody2D rBodyBluePlayer;
    Vector2 RedPlayerStartPos = new Vector2(-4f, 0);
    Vector2 BluePlayerStartPos = new Vector2(4f, 0);
    Vector2 PuckStartPos1 = new Vector2(-2f, 0);
    Vector2 PuckStartPos2 = new Vector2(2f, 0);
    int MAX_STUCK_TIME = 30;
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

        Physics2D.IgnoreCollision(Puck.GetComponent<Collider2D>(), RedGoal.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(Puck.GetComponent<Collider2D>(), BlueGoal.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(Puck.GetComponent<Collider2D>(), HalfCourt.GetComponent<Collider2D>());

        RedPlayerScore = 0;
        BluePlayerScore = 0;
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
            BlueAgent.AddReward(1);
            RedAgent.AddReward(-1);
            //Debug.Log("Blue Score!! Current Score: Red: " + RedPlayerScore + " Blue: " + BluePlayerScore);
        }
        else {
            RedPlayerScore += 1;
            RedAgent.AddReward(1);
            BlueAgent.AddReward(-1);
            //Debug.Log("Red Score!! Current Score: Red: " + RedPlayerScore + " Blue: " + BluePlayerScore);
        }
        RedAgent.EndEpisode();
        BlueAgent.EndEpisode();
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
            //BlueAgent.AddReward(-0.2f);
            //RedAgent.AddReward(-0.2f);
            RestartLevel();
        }
    }

    void RestartLevel(){
        // Randomly choose Puck start position
        if (Random.Range(0,10) > 5){
            Puck.transform.localPosition = PuckStartPos1;
        } else {
            Puck.transform.localPosition = PuckStartPos2;
        }
        rBodyPuck.velocity = Vector2.zero;

        RedPlayer.transform.localPosition = RedPlayerStartPos;
        rBodyRedPlayer.velocity = Vector2.zero;

        BluePlayer.transform.localPosition = BluePlayerStartPos;
        rBodyBluePlayer.velocity = Vector2.zero;

        puckStuck = false;
    }

    /*
    public float distancePuck2RayAI() {
        Vector3 playerDirection = rBodyRedPlayer.velocity.normalized;
        Vector3 playerStartingPoint = RedPlayer.transform.position;
        
        Ray ray = new Ray(playerStartingPoint, playerDirection);
        return Vector3.Cross(ray.direction, Puck.transform.position - ray.origin).magnitude;
    }
    */
}
