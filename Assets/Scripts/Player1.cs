using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    float MIN_XPOS = -5.3f;
    float MAX_XPOS = -0.7f;
    float MIN_YPOS = -2.75f;
    float MAX_YPOS = 2.8f;
    Rigidbody2D rB;
    Vector2 oldPos;
    [SerializeField] float forceMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        oldPos = new Vector2(-4, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = oldPos;
        // While pressing down the left mouse key
        if (Input.GetMouseButton(0)) {
            // Make sure Player 1 can't move out of bounds
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float xPos = Mathf.Clamp(mousePos.x, MIN_XPOS, MAX_XPOS);
            float yPos = Mathf.Clamp(mousePos.y, MIN_YPOS, MAX_YPOS);

            // Keep track of last mouse position in order to calculate velocity
            newPos = new Vector2(xPos, yPos);
            oldPos = transform.position;
            transform.position = newPos;
        }

        rB.velocity = forceMultiplier * (newPos - oldPos);
        //Debug.Log(rB.velocity);


        
    }
    
}
