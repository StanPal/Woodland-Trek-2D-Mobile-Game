using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private BoxCollider2D _boxCollider; 

    private bool _isGrounded;
    private float _moveX;
    private float _moveY;
    
    public LayerMask _groundLayer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        _moveX = Input.GetAxisRaw("Horizontal");
        if(IsGrounded() && Input.GetButtonDown("Jump"))
        {
            _rb.velocity = Vector2.up * _jumpForce;
        }
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(_moveX * _moveSpeed, _rb.velocity.y);
        _rb.velocity = movement;

        FlipCharacter();
    }


    public bool IsGrounded()
    {
        float extraHeight = 0.05f;
        RaycastHit2D raycastHit = Physics2D.Raycast(_boxCollider.bounds.center, Vector2.down, _boxCollider.bounds.extents.y + extraHeight, _groundLayer);
        Color rayColor;
        
        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(_boxCollider.bounds.center, Vector2.down * (_boxCollider.bounds.extents.y + extraHeight), rayColor);
        return raycastHit.collider != null;
    }

    private void FlipCharacter()
    {
        if(_rb.velocity.x < 0)
        {
            _sprite.flipX = true;
        }
        else
        {
            _sprite.flipX = false;

        }
    }
}
