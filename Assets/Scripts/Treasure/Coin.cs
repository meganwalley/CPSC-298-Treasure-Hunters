using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class Coin : NetworkBehaviour
{
    Rigidbody2D body;

    public int value=1;
    private AudioSource audioSource;
    public bool isActive = true;

    public GameObject pickupEffect; 

    public float moveSpeed = 5f; //how fast this moves across the map

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdatePosition(); 
    }





    private void UpdatePosition()
    {
        body.velocity = new Vector2(-moveSpeed, 0);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!isActive)
        {
            return; 
        }
        GameObject collidedObject = coll.gameObject; 
        if (collidedObject.CompareTag("Player"))
        {
            isActive = false; 
            TriggerEffect(collidedObject);
            SoundEffect();
            AfterEffect();
        }
    }

    private void TriggerEffect(GameObject collidedObject)
    {
        Player player = collidedObject.GetComponent<Player>();
        player.AddScore(value);
    }

    private void SoundEffect()
    {
        audioSource.Play();
    }

    private void AfterEffect()
    {
        Destroy(gameObject, audioSource.clip.length);
        Color spriteColor = GetComponent<SpriteRenderer>().color;
        spriteColor.a = 0f;
        GetComponent<SpriteRenderer>().color = spriteColor;
        Instantiate(pickupEffect, transform.position, Quaternion.identity);
    }

}
