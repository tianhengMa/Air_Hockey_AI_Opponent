using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AIPlayer : Agent
{
    Rigidbody2D rBodyPlayer;
    Rigidbody2D rBodyPuck;
    Rigidbody2D rBodyOpponent;
    [SerializeField] GameObject Puck;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Opponent;

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
        sensor.AddObservation(AIPlayerToPuck.normalized);
        sensor.AddObservation(AIPlayerToPuck.magnitude);
    
        // Velocity Observation
        Vector2 PlayerVelocity = rBodyPlayer.velocity;
        Vector2 PuckVelocity = rBodyPuck.velocity;
        sensor.AddObservation(PlayerVelocity.normalized);
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
