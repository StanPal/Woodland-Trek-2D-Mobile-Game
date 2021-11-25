using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public event System.Action<GameObject> OnDeath;
    public event System.Action OnCoinPickup;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Coin>(out Coin coin))
        {
            Debug.Log("HitCoin");
            OnCoinPickup?.Invoke();
            Destroy(collision.gameObject);
        }

        if(collision.tag == "Trap")
        {
            OnDeath?.Invoke(this.gameObject);
        }
    }
}
