using System.Collections;
using UnityEngine;

public class FallingStone : MonoBehaviour
{
    public Transform _startPos;
    [SerializeField] private float _rayLength;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _gravity;
    [SerializeField] private float _blinkTimer;
    [SerializeField] private float _returnSpeed;

    private Animator _animator; 
    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider;
    private bool _isMovingBack;
    private bool _isBlinking; 

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if(IsGrounded())
        {
            StartCoroutine(ReturnToStart());
        }

        if(!_isBlinking)
        {
            StartCoroutine(Blink());
        }
    }

    private IEnumerator Blink()
    {
        _isBlinking = true;
        _animator.SetBool("OnBlink", true);
        yield return new WaitForSeconds(_blinkTimer);
        _animator.SetBool("OnBlink", false);
        _isBlinking = false;
    }

    private IEnumerator ReturnToStart()
    {
      _isMovingBack = true;
      _rb.bodyType = RigidbodyType2D.Static;
        while (Vector2.Distance(transform.position, _startPos.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPos.position, _returnSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
      _isMovingBack = false;

    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(_boxCollider.bounds.center, Vector2.down, _boxCollider.bounds.extents.y + _rayLength, _groundLayer);
        Color rayColor; 
        if(raycastHit.collider != null)
        {
            rayColor = Color.green;

        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(_boxCollider.bounds.center, Vector2.down * (_boxCollider.bounds.extents.y + _rayLength), rayColor);
        return (raycastHit);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isMovingBack)
        {
            if (collision.gameObject.GetComponent<Character>())
            {
                _rb.isKinematic = false;
                //_rb.gravityScale = _gravity;
                Debug.Log("Player Entered");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!_isMovingBack)
        {
            if(collision.gameObject.GetComponent<Character>())
            {
                collision.gameObject.GetComponent<PlayerCollision>().InvokeOnDeath();
            }
        }
    }
}
