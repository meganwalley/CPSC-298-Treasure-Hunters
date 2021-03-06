using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class Swordfish : NetworkBehaviour
{
    Rigidbody2D body;

    public int damage = 1;
    private AudioSource audioSource;
    //public bool isActive = true;

    public GameObject textEffect;

    public float moveSpeed = 5f; 

    public AudioClip swimSound; 
    public AudioClip damageSound;


    public override void OnStartServer()
    {
        base.OnStartServer();

        body = GetComponent<Rigidbody2D>();
        body.simulated = true;

        audioSource = GetComponent<AudioSource>();
        SoundEffect(); 
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
            collidedObject.GetComponent<Player>().DamageEffect(2);
            AfterEffect();
        }
    }

    private void TriggerEffect(GameObject collidedObject)
    {
        Player player = collidedObject.GetComponent<Player>();
        player.AddScore(-damage);
    }
    private void SoundEffect()
    {
        audioSource.clip = swimSound;
        audioSource.Play();
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
