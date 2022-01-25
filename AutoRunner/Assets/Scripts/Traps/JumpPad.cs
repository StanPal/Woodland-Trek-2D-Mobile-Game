using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float _bounce;
    private Animator _animator;
    private bool _isActive;
    [SerializeField] private BounceDir _bounceDirection = BounceDir.up;

    enum BounceDir
    {
        left,
        right,
        up,
        down
    }

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
                switch (_bounceDirection)
                {
                    case BounceDir.left:
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * _bounce, ForceMode2D.Impulse);
                        break;
                    case BounceDir.right:
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _bounce, ForceMode2D.Impulse);
                        break;
                    case BounceDir.up:
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _bounce, ForceMode2D.Impulse);
                        break;
                    case BounceDir.down:
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * _bounce, ForceMode2D.Impulse);
                        break;
                    default:
                        break;
                }
                _animator.SetTrigger("PadTrigger");
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
