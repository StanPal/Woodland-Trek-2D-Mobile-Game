using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public event System.Action<GameObject> OnDeath;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<Trap>())
        {
            OnDeath?.Invoke(this.gameObject);
        }

        if(collision.collider.tag == "MovingPlatform")
        {
            transform.parent = collision.collider.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.parent = null;
    }
}
