using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinking : MonoBehaviour
{
    Rigidbody2D body;
    public float moveSpeed = 5f; //how fast this moves across the map
    public bool isSinking = true; 


    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (isSinking)
        {
            UpdatePosition();
        }
        
    }


    private void UpdatePosition()
    {
        Vector2 force = body.velocity;
        force.y = -moveSpeed;
        body.velocity = force;
    }

    public void StopSinking()
    {
        isSinking = false; 
    }

    public void StartSinking()
    {
        isSinking = true; 
    }
}
