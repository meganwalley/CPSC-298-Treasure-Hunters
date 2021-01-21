using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform following;

    private void FixedUpdate()
    {
        this.transform.position = new Vector2(following.position.x, transform.position.y);
    }
}
