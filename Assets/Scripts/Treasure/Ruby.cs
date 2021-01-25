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

    public override void OnStartServer()
    {
        base.OnStartServer();

        body = GetComponent<Rigidbody2D>();
        body.simulated = true;

        audioSource = GetComponent<AudioSource>();
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


    [ServerCallback]
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
        Invoke("SelfDestroy", audioSource.clip.length);
        Color spriteColor = GetComponent<SpriteRenderer>().color;
        spriteColor.a = 0f;
        GetComponent<SpriteRenderer>().color = spriteColor;
        GameObject pickupEffectInstante = Instantiate(pickupEffect, transform.position, Quaternion.identity);
        NetworkServer.Spawn(pickupEffectInstante);

        GameObject textEffectInstante = Instantiate(textEffect, transform.position, Quaternion.identity);
        textEffectInstante.GetComponent<RisingText>().content = "+" + value;
        NetworkServer.Spawn(textEffectInstante);
    }

    private void SelfDestroy()
    {
        NetworkServer.Destroy(gameObject);
    }

}
