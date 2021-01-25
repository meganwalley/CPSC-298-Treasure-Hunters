using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class JellyFish : NetworkBehaviour
{
    Rigidbody2D body;
    Sinking sinking; 

    public int damage = 1;
    private AudioSource audioSource;
    //public bool isActive = true;

    public GameObject textEffect;

    public float moveSpeed = 5f; //how fast this moves across the map

    public bool isPopping = false;
    private int popupLimit = 40;
    private int popupCount = 0;

    public AudioClip popSound; 
    public AudioClip damageSound; 


    private void Start()
    {
        body = GetComponent<Rigidbody2D>(); 
        audioSource = GetComponent<AudioSource>();
        sinking = GetComponent<Sinking>(); 
    }

    private void Update()
    {
        CheckHeight();
        if (isPopping)
        {
            Popup();
            return; 
        }

        RandomPopup(); 
    }





    private void CheckHeight()
    {
        if(transform.position.y <= -10)
        {
            if (!isPopping)
            {
                StartPopup();
            }
        }
    }

    private void StartPopup()
    {
        SoundEffect();
        isPopping = true;
        sinking.StopSinking(); 
    }

    private void Popup()
    {
        Vector2 force = body.velocity;
        force.y += moveSpeed;
        body.velocity = force;
        popupCount += 1; 
        if(popupCount >= popupLimit)
        {
            FinishPopup(); 
        }
    }

    void FinishPopup()
    {
        isPopping = false;
        popupCount = 0;
        sinking.StartSinking(); 
    }

    void RandomPopup()
    {
        int randomInt = Random.Range(1, 400); 
        if(1<= randomInt && randomInt <= 1)
        {
            StartPopup(); 
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collidedObject = coll.gameObject;
        if (collidedObject.CompareTag("Player"))
        {
            TriggerEffect(collidedObject);
            DamageSoundEffect();
            collidedObject.GetComponent<Player>().DamageEffect(5); 
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
        audioSource.clip = popSound;
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
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
