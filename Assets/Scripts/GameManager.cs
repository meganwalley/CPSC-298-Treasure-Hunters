using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class GameManager : NetworkBehaviour
{
    public int gameTimeLength = 60; //how long the game will be
    public int currTime = 0;

    public bool isGameEnd = false; 


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        UpdateTime();
        CheckGameEnd(); 
    }


    private void UpdateTime()
    {
        currTime++; 
    }

    private void CheckGameEnd()
    {
        if(currTime >= gameTimeLength)
        {
            isGameEnd = true; 
        }
    }



}
