using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class Seaweed : NetworkBehaviour
{
    Rigidbody2D body;

    public int type = 1; //1 for big, 2 for small, this value is for position adjustment
    public int damage = 1;
    private AudioSource audioSource;
    //public bool isActive = true;

    public GameObject textEffect;

    public int moveSpeedDeduction = 5; 

    public AudioClip damageSound;


    public override void OnStartServer()
    {
        base.OnStartServer();

        body = GetComponent<Rigidbody2D>();
        body.simulated = true;

        audioSource = GetComponent<AudioSource>();

        float adjustment = 0; 
        switch (type)
        {
            case 1:
                adjustment = 2.5f; 
                break;
            case 2:
                adjustment = 1;
                break; 
        }
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y + adjustment);
        transform.position = newPosition; 
    }

    private void Update()
    {

    }

    [ServerCallback]
    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collidedObject = coll.gameObject;
        if (collidedObject.CompareTag("Player"))
        {
            TriggerEffect(collidedObject);
            DamageSoundEffect();
            AfterEffect();
        }
    }
    [ServerCallback]
    private void OnTriggerExit2D(Collider2D coll)
    {
        GameObject collidedObject = coll.gameObject;
        if (collidedObject.CompareTag("Player"))
        {
            DeTriggerEffect(collidedObject); 
        }
    }

    private void TriggerEffect(GameObject collidedObject)
    {
        Player player = collidedObject.GetComponent<Player>();
        player.AddScore(-damage);
        player.SlowDownMoveSpeed(moveSpeedDeduction); 
    }
    private void DeTriggerEffect(GameObject collidedObject)
    {
        Player player = collidedObject.GetComponent<Player>();
        player.AddScore(-damage);
        player.ResetMoveSpeed(); 
    }

    private void DamageSoundEffect()
    {
        audioSource.clip = damageSound;
        audioSource.Play();
    }

    private void AfterEffect()
    {
        GameObject textEffectInstante = Instantiate(textEffect, transform.position, Quaternion.identity);
        textEffectInstante.GetComponent<RisingText>().content = "-" + damage;
        NetworkServer.Spawn(textEffectInstante);
    }
}
