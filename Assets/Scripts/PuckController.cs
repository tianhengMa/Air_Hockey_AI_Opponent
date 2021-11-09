using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckController : MonoBehaviour
{
    public GameObject table;
    [SerializeField] Transform RedGoal;
    [SerializeField] Transform BlueGoal;

    private float lastPosY;
    [HideInInspector]
    public GameManager gameManager;

    void Start()
    {
        gameManager = table.transform.GetChild(0).GetComponent<GameManager>();
        lastPosY = transform.localPosition.y;
    }

    void Update() {
        if (transform.localPosition.y <= RedGoal.localPosition.y) { //ball touched red goal
            gameManager.Scored(PlayerId.Blue);
        }
        if (transform.localPosition.y >= BlueGoal.localPosition.y) { //ball touched blue goal
            gameManager.Scored(PlayerId.Red);
        }

        // Puck switched from Red half court to Blue half court, award Red
        if (lastPosY <= 0 && transform.localPosition.y > 0) {
            //Debug.Log("Puck switch halfcourt: Red -> Blue award red 0.0002");
            gameManager.RedReward(0.0002f);
        } 
        // Puck switched from Blue half court to Red half court, award Blue
        else if (lastPosY >= 0 && transform.localPosition.y < 0) {
            //Debug.Log("Puck switch halfcourt: Blue -> Red, award blue 0.0002");
            gameManager.BlueReward(0.0002f);
        }
        lastPosY = transform.localPosition.y;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "BluePlayer") {
            //Debug.Log("Blue Player Hit Puck, award blue 0.0001");
            gameManager.BlueReward(0.0001f);
        }
        else if (col.gameObject.tag == "RedPlayer") {
            //Debug.Log("Red Player Hit Puck, award red 0.0001");
            gameManager.RedReward(0.0001f);
        }
    }
}