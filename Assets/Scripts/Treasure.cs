using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{

    public int value;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("coin");
        GameObject collidedObject = collision.gameObject; 
        if (collidedObject.CompareTag("Player"))
        {
            TriggerEffect(collidedObject);
            SoundEffect();

        }

        Destroy(gameObject); 
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

}
