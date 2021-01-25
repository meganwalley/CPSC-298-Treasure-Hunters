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

    public float moveSpeed = 5f; //how fast this moves across the map

    public AudioClip swimSound; 
    public AudioClip damageSound;


    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        SoundEffect(); 
    }

    private void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collidedObject = coll.gameObject;
        if (collidedObject.CompareTag("Player"))
        {
            TriggerEffect(collidedObject);
            DamageSoundEffect();
            collidedObject.GetComponent<Player>().DamageEffect(10);
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
        textEffectInstante.GetComponent<SetTextMesh>().SetNewText("-" + damage);
    }
}
