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
    }    
}
