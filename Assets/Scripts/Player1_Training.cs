using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_Training : MonoBehaviour
{
    float MIN_YPOS = -2.75f;
    float MAX_YPOS = 2.8f;
    Rigidbody2D rB;
    float forceMultiplier;
    bool goingUp;
    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        forceMultiplier = 15f;
        goingUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Velocity: " + rB.velocity + " Position: " + transform.localPosition + " goingUp: " + goingUp);        

        if (goingUp && transform.localPosition.y <= MAX_YPOS) {
            rB.velocity = forceMultiplier * new Vector2(0, 1);
            //rB.AddForce(forceMultiplier * new Vector2(0, 1));
        } else {
            goingUp = false;
        }

        if (!goingUp && transform.localPosition.y >= MIN_YPOS) {
            rB.velocity = forceMultiplier * new Vector2(0, -1);
            //rB.AddForce(forceMultiplier * new Vector2(0, -1));
        } else {
            goingUp = true;
        }
    }
}
