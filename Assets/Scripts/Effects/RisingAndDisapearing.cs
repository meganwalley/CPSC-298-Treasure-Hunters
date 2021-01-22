using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingAndDisapearing : MonoBehaviour
{
    Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5); 
    }
    void Update()
    {
        UpdatePosition();
        UpdateAlpha();
    }

    private void UpdatePosition()
    {
        body.velocity = new Vector2(0, 1);
    }

    private void UpdateAlpha()
    {
        Color temp = GetComponent<SpriteRenderer>().material.color;
        temp.a -= 0.001f;
        GetComponent<SpriteRenderer>().material.color = temp; 
    }
}
