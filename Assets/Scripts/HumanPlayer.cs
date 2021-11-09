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
    bool canMove;
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
            // Make sure Player 1 can't move out of bounds
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float xPos = Mathf.Clamp(mousePos.x, MIN_XPOS, MAX_XPOS);
            float yPos = Mathf.Clamp(mousePos.y, MIN_YPOS, MAX_YPOS);

            // Keep track of last mouse position in order to calculate velocity
            newPos = new Vector2(xPos, yPos);
            //prevPos = transform.position;
            transform.position = newPos;
            rB.velocity = forceMultiplier * (newPos - prevPos);
        }

        prevPos = newPos;
        
        //ControlWithMouse();
        //Debug.Log(rB.velocity);
    }

     void ControlWithMouse(){
        //Vector2 prevPos = new Vector2(0,0);
        if (Input.GetMouseButtonDown(0)){
            prevPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)){
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 relativePos = mousePos - prevPos;
            /*
            if (canMove)
                transform.position += forceMultiplier * relativePos.x;
                */
            rB.velocity = forceMultiplier * relativePos;
        }
    }


    void OnCollisionEnter(Collision collision) 
    {
        if(gameObject.CompareTag("Buildings"))
        {
            canMove = false;
        }
        else{
            canMove = true;
        }
            
    }
    
}
