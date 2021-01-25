using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror; 

public class RisingText : NetworkBehaviour
{
    Rigidbody2D body;

    private int timeBeforeFadingOut = 60;
    private bool isFadingOut = false;

    [SyncVar]
    public string content; 


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Invoke("SelfDestroy", 5);
        Invoke("StartFadingOut", timeBeforeFadingOut / 60); 
    }

    void Update()
    {
        SetText(); 
        UpdatePosition();
        if (isFadingOut)
        {
            UpdateAlpha();
        }
    }


    private void StartFadingOut()
    {
        isFadingOut = true; 
    }
    private void UpdatePosition()
    {
        body.velocity = new Vector2(0, 0.25f);
    }
    private void UpdateAlpha()
    {
        Color temp = GetComponent<MeshRenderer>().material.color;
        temp.a -= 0.015f;
        GetComponent<MeshRenderer>().material.color = temp;
    }


    public void SetText()
    {
        TextMesh textMesh = GetComponent<TextMesh>();
        textMesh.text = content;
    }

    private void SelfDestroy()
    {
        NetworkServer.Destroy(gameObject);
    }
}
