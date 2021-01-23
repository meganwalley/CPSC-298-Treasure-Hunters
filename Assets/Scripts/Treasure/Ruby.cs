using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Ruby : NetworkBehaviour
{
    Rigidbody2D body;

    public int value=10;
    private AudioSource audioSource;
    public bool isActive = true;

    public GameObject pickupEffect;
    public GameObject textEffect; 

    public float moveSpeed = 10f; //how fast this moves across the map
    public float moveCount = 180f; //how long it will keep on current movement direction
    public float currMoveCount = 0f; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
        MoveToNewDirection(); 
    }

    private void Update()
    {
        UpdatePosition();
    }





    private void UpdatePosition()
    {
        currMoveCount += 1; 
        if(currMoveCount >= moveCount)
        {
            MoveToNewDirection(); 
            currMoveCount = 0; 
        }
    }
    private void MoveToNewDirection()
    {
        float nextY = Random.Range(-moveSpeed, moveSpeed);
        body.velocity = new Vector2(-moveSpeed, nextY);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!isActive)
        {
            return;
        }
        GameObject collidedObject = coll.gameObject;
        if (collidedObject.CompareTag("Player") && collidedObject.GetComponent<Player>().CheckCanPickUp())
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
        GameObject textEffectInstante = Instantiate(textEffect, transform.position, Quaternion.identity);
        textEffectInstante.GetComponent<SetTextMesh>().SetNewText("+" + value); 
    }

}
