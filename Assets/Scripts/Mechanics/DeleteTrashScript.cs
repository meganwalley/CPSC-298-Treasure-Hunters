using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class DeleteTrashScript : NetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        NetworkServer.Destroy(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        NetworkServer.Destroy(collision.gameObject);
    }
}
