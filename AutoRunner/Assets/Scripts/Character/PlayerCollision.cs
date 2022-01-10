using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public event System.Action<GameObject> OnDeath;
    public event System.Action OnCoinPickup;
    private Animator _animator;    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<Trap>())
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.GetComponent<PlayerController>().enabled = false;
            _animator.SetBool("IsDead",true);
            //gameObject.SetActive(false);
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

        if (collision.tag == "Trap")
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.GetComponent<PlayerController>().enabled = false;
            _animator.SetBool("IsDead", true);
            OnDeath?.Invoke(this.gameObject);
        }
    }
}
