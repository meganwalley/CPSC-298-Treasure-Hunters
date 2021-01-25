using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Player : NetworkBehaviour
{
    //for movements and physics
    Rigidbody2D body;
    public int rawMoveSpeed = 5; 
    private int moveSpeed = 5; 
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
    bool inWallCollision = false;
    bool canPickUp = true;
    bool isFlashing = false; 

    //for UI
    int currScore = 0;
    public GameObject ScoreBoard; 



    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        moveSpeed = rawMoveSpeed; 
    }
    void Update()
    {
        ReadMovementInput();
    }
    private void FixedUpdate()
    {
        ProcessMovementInput();
    }



    //movements
    private void ReadMovementInput()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                direction = "N";
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                direction = "S";
            }
            else
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
    }
    private void ProcessMovementInput()
    {
        switch (direction)
        {
            case "N":
                body.velocity = new Vector2(0, moveSpeed * movement);
                break;
            case "NE":
                body.velocity = new Vector2(moveSpeed * movement, moveSpeed * movement);
                break;
            case "E":
                body.velocity = new Vector2(moveSpeed * movement, 0);
                break;
            case "SE":
                body.velocity = new Vector2(moveSpeed * movement, -moveSpeed * movement);
                break;
            case "S":
                body.velocity = new Vector2(0, -moveSpeed * movement);
                break;
            case "SW":
                body.velocity = new Vector2(-moveSpeed * movement, -moveSpeed * movement);
                break;
            case "W":
                body.velocity = new Vector2(-moveSpeed * movement, 0);
                break;
            case "NW":
                body.velocity = new Vector2(-moveSpeed * movement, moveSpeed * movement);
                break;
            default:
                body.velocity = new Vector2(0, 0);
                break;
        }
        body.angularVelocity = 0;
    }


    //collision and triggers
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("WallCollider"))
        {
            inWallCollision = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("WallCollider"))
            inWallCollision = false;
    }

    //Receiving Damage
    public bool CheckCanPickUp()
    {
        return canPickUp; 
    }

    public void DamageEffect(int lastingTime)
    {
        if(canPickUp == false)
        {
            return; 
        }
        canPickUp = false;
        InvokeRepeating("SpriteFlashSwitch", 0, 0.1f);
        Invoke("FinishDamage", lastingTime);
    }
    public void FinishDamage()
    {
        CancelInvoke("SpriteFlashSwitch");
        SpriteVisible();
        canPickUp = true; 
    }
    private void SpriteFlashSwitch()
    {
        Color temp = GetComponentInChildren<SpriteRenderer>().material.color; 
        if (temp.a != 1)
        {
            temp.a = 1;
        }
        else
        {
            temp.a = 0;
        }
        GetComponentInChildren<SpriteRenderer>().material.color = temp;
    }
    private void SpriteVisible()
    {
        Color temp = GetComponentInChildren<SpriteRenderer>().material.color;
        temp.a = 1;
        GetComponentInChildren<SpriteRenderer>().material.color = temp;
    }
    public void SlowDownMoveSpeed(int amount)
    {
        moveSpeed -= amount; 
    }
    public void ResetMoveSpeed()
    {
        moveSpeed = rawMoveSpeed; 
    }



    //Score
    public void AddScore(int value)
    {
        currScore += value;
        UpdateUI(); 
    }


    //UI
    public void UpdateUI()
    {
        Text scoreBoard = ScoreBoard.GetComponent<Text>(); 
        scoreBoard.text = "Current Score: " + currScore; 
    }
}

