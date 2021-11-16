using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : MonoBehaviour
{
    float MIN_XPOS = -3.7f;
    float MAX_XPOS = 3.7f;
    float MIN_YPOS = -7.45f;
    float MAX_YPOS = -0.71f;
    Rigidbody2D rB;
    Vector2 prevPos;
    Vector2 newPos;

    [SerializeField] float forceMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        prevPos = newPos = new Vector2(-6, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // While pressing down the left mouse key
        if (Input.GetMouseButton(0)) {
            // Make sure Player can't move out of bounds
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float xPos = Mathf.Clamp(mousePos.x, MIN_XPOS, MAX_XPOS);
            float yPos = Mathf.Clamp(mousePos.y, MIN_YPOS, MAX_YPOS);

            // Keep track of last mouse position in order to calculate velocity
            newPos = new Vector2(xPos, yPos);
            transform.position = newPos;
            rB.velocity = forceMultiplier * (newPos - prevPos);
        }

        prevPos = newPos;
        //Debug.Log(rB.velocity);
    }
    
}
