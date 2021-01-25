using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seaweed : MonoBehaviour
{
    Rigidbody2D body;

    public int type = 1; //1 for big, 2 for small, this value is for position adjustment
    public int damage = 1;
    private AudioSource audioSource;
    //public bool isActive = true;

    public GameObject textEffect;

    public int moveSpeedDeduction = 5; 

    public AudioClip damageSound;


    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
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
        textEffectInstante.GetComponent<SetTextMesh>().SetNewText("-" + damage);
    }
}
