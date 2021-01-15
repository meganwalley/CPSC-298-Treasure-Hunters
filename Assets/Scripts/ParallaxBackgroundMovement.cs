using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgroundMovement : MonoBehaviour
{
    private float length, startpos;
    public float fixedAmount;

    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float reset = transform.position.x + (fixedAmount/32);
        if ((reset >= startpos + length) || (reset <= startpos - length))
        {
            reset = startpos;
        }

        transform.position = new Vector2(reset, transform.position.y);
    }

}
