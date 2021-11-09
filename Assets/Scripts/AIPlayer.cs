using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AIPlayer : Agent
{
    public PlayerId playerId; // Red = 0, Blue = 1
    public GameManager gameManager;
    Rigidbody2D rBodyPlayer;
    Rigidbody2D rBodyPuck;
    Rigidbody2D rBodyOpponent;
    [SerializeField] GameObject Puck;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Opponent;
    [SerializeField] GameObject RedGoal;
    [SerializeField] GameObject BlueGoal;

    // Start is called before the first frame update
    void Start()
    {
        rBodyPlayer = Player.GetComponent<Rigidbody2D>();
        rBodyPuck = Puck.GetComponent<Rigidbody2D>();
        rBodyOpponent = Opponent.GetComponent<Rigidbody2D>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // AIPlayer Distance Observation
        
        Vector2 AIPlayerToPuck = Puck.transform.localPosition - Player.transform.localPosition;
        //Vector2 AIPlayerToBlueGoal = BlueGoal.transform.localPosition - Player.transform.localPosition;
        //Vector2 AIPlayerToRedGoal = RedGoal.transform.localPosition - Player.transform.localPosition;
        sensor.AddObservation(AIPlayerToPuck.normalized);
        sensor.AddObservation(AIPlayerToPuck.magnitude);
        //sensor.AddObservation(AIPlayerToBlueGoal);
        //sensor.AddObservation(AIPlayerToRedGoal);
    
        // Position Observation
        //Vector2 PlayerPos = Player.transform.localPosition;
        //Vector2 OpponentPos = Opponent.transform.localPosition;
        //Vector2 PuckPos = Puck.transform.localPosition;
        //Vector2 BlueGoalPos = BlueGoal.transform.localPosition;
        //Vector2 RedGoalPos = RedGoal.transform.localPosition;
        //sensor.AddObservation(PlayerPos.normalized);
        //sensor.AddObservation(OpponentPos.normalized);
        //sensor.AddObservation(PuckPos.normalized);
        //sensor.AddObservation(BlueGoalPos.normalized);
        //sensor.AddObservation(RedGoalPos.normalized);

        // Velocity Observation
        Vector2 PlayerVelocity = rBodyPlayer.velocity;
        //Vector2 OpponentVelocity = rBodyOpponent.velocity;
        Vector2 PuckVelocity = rBodyPuck.velocity;
        sensor.AddObservation(PlayerVelocity.normalized);
        //sensor.AddObservation(OpponentVelocity.normalized);
        sensor.AddObservation(PuckVelocity.normalized);
    }
    
    public float forceMultiplier = 10;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        Vector2 controlSignal = Vector2.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.y = actionBuffers.ContinuousActions[1];
        rBodyPlayer.velocity = controlSignal * forceMultiplier;

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}
