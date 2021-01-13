using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    new Rigidbody2D body;
    string direction;
    string colDirection;
    float movement = 4;
    /*
     height = 1;
     left = 0.5;
     right = 1.5;
     height left = 0.75;
     height right = 1.25;
     */

    bool inControl = true;
    bool col = false;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            direction = "N";
        } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            direction = "S";
        } else
        {
            direction = "";
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction += "E";
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction += "W";
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("WallCollider"))
        {
            col = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("WallCollider"))
            col = false;
    }
    private void FixedUpdate()
    {

        switch(direction)
        {
            case "N":
                body.velocity = new Vector2(0, 1F* movement);
                break;
            case "NE":
                body.velocity = new Vector2(1F* movement, 0.5F * movement);
                break;
            case "E":
                body.velocity = new Vector2(1.5F * movement, 0);
                break;
            case "SE":
                body.velocity = new Vector2(1F * movement, -0.5F * movement);
                break;
            case "S":
                body.velocity = new Vector2(0, -1F * movement);
                break;
            case "SW":
                body.velocity = new Vector2(-0.5F * movement, -0.5F * movement);
                break;
            case "W":
                body.velocity = new Vector2(-0.6F * movement, 0);
                break;
            case "NW":
                body.velocity = new Vector2(-0.5F * movement, 0.5F * movement);
                break;
            default:
                body.velocity = new Vector2(0, 0);
                break;
        }
        body.angularVelocity = 0;
    }

}
