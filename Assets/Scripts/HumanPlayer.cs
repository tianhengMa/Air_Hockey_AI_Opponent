using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : MonoBehaviour
{
    [SerializeField] Transform PauseButton;
    [SerializeField] float forceMultiplier;
    float MIN_XPOS = -3.7f;
    float MAX_XPOS = 3.7f;
    float MIN_YPOS = -7.45f;
    float MAX_YPOS = -0.71f;
    float DIST_TO_PAUSE = 580f; 
    Rigidbody2D rB;
    Vector2 prevPos;
    Vector2 newPos;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        prevPos = newPos = new Vector2(-6, 0);
    }

    // Must use FixedUpdate (called per fixed time period) instead of Update (called per frame) 
    void FixedUpdate()
    {
        bool pressedPausedButton = false;
        // While pressing down the left mouse key
        if (Input.GetMouseButton(0)) {
            // Make sure Player can't move out of bounds
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // If user clicks pause button, don't move the redplayer
            pressedPausedButton = (Vector2.Distance(mousePos, new Vector2(PauseButton.localPosition.x,PauseButton.localPosition.y)) < DIST_TO_PAUSE);
            if (!pressedPausedButton) {
                float xPos = Mathf.Clamp(mousePos.x, MIN_XPOS, MAX_XPOS);
                float yPos = Mathf.Clamp(mousePos.y, MIN_YPOS, MAX_YPOS);

                // Keep track of last mouse position in order to calculate velocity
                newPos = new Vector2(xPos, yPos);
                transform.position = newPos;
                rB.velocity = forceMultiplier * (newPos - prevPos);
            }
        }

        if (!pressedPausedButton) {
            prevPos = newPos;
        }
        //Debug.Log(rB.velocity);
    }
    
}
