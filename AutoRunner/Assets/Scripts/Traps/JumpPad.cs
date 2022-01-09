using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float _bounce;
    private Animator _animator;
    private bool _isActive; 

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isActive)
        {
            if (collision.gameObject.GetComponent<PlayerCollision>())
            {
                _animator.SetTrigger("PadTrigger");
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _bounce, ForceMode2D.Impulse);
                //collision.gameObject.GetComponent<Rigidbody2D>().velocity = (Vector2.up * _bounce);
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.ClampMagnitude(collision.gameObject.GetComponent<Rigidbody2D>().velocity, _bounce);
                _isActive = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerCollision>())
        {
            _isActive = false;
        }
    }
}
