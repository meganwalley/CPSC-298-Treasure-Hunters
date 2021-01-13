using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D body;
    public float movementX = -0.25F;
    float movementSpeed = 4;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.velocity = new Vector2(movementX*movementSpeed,0);
    }
}
