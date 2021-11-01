using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTracker : MonoBehaviour
{
    public GameObject table;
    float startTime;
    public float report_period = 5;

    [HideInInspector]
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = table.transform.GetChild(0).GetComponent<GameManager>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {   
        if (Time.time - startTime > report_period) {
            startTime = Time.time;
            Debug.Log("Round: " + gameManager.num_rounds);
        }
    }
}
