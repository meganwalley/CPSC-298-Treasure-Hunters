using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class Coin : NetworkBehaviour
{

    public int value=1;
    private AudioSource audioSource;
    public bool isActive = true;

    public GameObject pickupEffect;
    public GameObject textEffect;

    public float moveSpeed = 5f; //how fast this moves across the map

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        GameObject textEffectInstante = Instantiate(textEffect, transform.position, Quaternion.identity);
        textEffectInstante.GetComponent<SetTextMesh>().SetNewText("+" + value);
    }

}
