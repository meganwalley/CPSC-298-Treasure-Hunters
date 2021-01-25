using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class LeftMoving : NetworkBehaviour
{
    Rigidbody2D body;
    [SyncVar]
    public float moveSpeed = 5f; //how fast this moves across the map


    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        UpdatePosition();
    }


    private void UpdatePosition()
    {
        Vector2 force = body.velocity;
        force.x = -moveSpeed;
        body.velocity = force; 
    }
}
