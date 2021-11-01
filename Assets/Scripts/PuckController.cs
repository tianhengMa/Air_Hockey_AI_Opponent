using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckController : MonoBehaviour
{
    public GameObject table;
    [SerializeField] Transform RedGoal;
    [SerializeField] Transform BlueGoal;
    [HideInInspector]
    public GameManager gameManager;

    void Start()
    {
        gameManager = table.transform.GetChild(0).GetComponent<GameManager>();
    }

    void Update() {
        if (transform.position.x <= RedGoal.position.x) { //ball touched red goal
            gameManager.Scored(PlayerId.Blue);
        }
        if (transform.position.x >= BlueGoal.position.x) { //ball touched blue goal
            gameManager.Scored(PlayerId.Red);
        }
    }
}