using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinking : MonoBehaviour
{
    Rigidbody2D body;
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
        body.velocity = new Vector2(0, -moveSpeed);
    }
}
