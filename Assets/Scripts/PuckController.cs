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
        if (transform.position.y <= RedGoal.position.y) { //ball touched red goal
            gameManager.Scored(PlayerId.Blue);
        }
        if (transform.position.y >= BlueGoal.position.y) { //ball touched blue goal
            gameManager.Scored(PlayerId.Red);
        }
    }
}