using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class Anchor : NetworkBehaviour
{
    Rigidbody2D body;

    public int damage = 1;
    private AudioSource audioSource;
    //public bool isActive = true;

    public GameObject textEffect;

    public float moveSpeed = 5f; //how fast this moves across the map

    public AudioClip swimSound;
    public AudioClip damageSound;
    public AudioClip landSound;


    private bool isActive = true;


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
        if (isActive)
        {
            CheckLand();
        }
    }


    [ServerCallback]
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!isActive)
        {
            return;
        }
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
        Vector2 force = new Vector2(0, -moveSpeed); 
        player.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse); 
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

    private void CheckLand()
    {
        if(transform.position.y <= -8.98 && isActive)
        {
            LandEffect(); 
        }
    }
    private void LandEffect()
    {
        isActive = false;
        if (body != null)
        {
            body.velocity = Vector2.zero;
        }
        GetComponent<Sinking>().enabled = false;
        GetComponent<LeftMoving>().enabled = true;
        if(audioSource == null)
        {
            return; 
        }
        audioSource.clip = landSound;
        audioSource.Play();
        Invoke("SelfDestroy", 5); 
    }

    private void SelfDestroy()
    {
        NetworkServer.Destroy(gameObject);
    }
}
