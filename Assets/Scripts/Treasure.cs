using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{

    public int value;
    private AudioSource audioSource;
    public bool isActive = true; 

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
            TriggerEffect(collidedObject);
            SoundEffect();

        }

        AfterEffect(); 
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
    }

}
