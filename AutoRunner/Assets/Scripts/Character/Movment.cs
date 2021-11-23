using UnityEngine;

public class Movment : MonoBehaviour
{
    public Transform Feet;

    private event System.Action OnJump;
    private event System.Action OnHoldJump;

    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    private Animator _animator; 
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
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        OnJump += Movment_OnJump;
        OnHoldJump += Movment_OnHoldJump;
    }    

    private void Update()
    {
        _moveX = SimpleInput.GetAxis("Horizontal");

        if(IsGrounded() && Input.GetButtonDown("Jump"))
        {       
            _rb.velocity = Vector2.up * _jumpForce;
            _animator.SetBool("IsJumping", true);
        }
    
    }

    private void FixedUpdate()
    {
        FlipCharacter();
        Vector2 movement = new Vector2(_moveX * _moveSpeed, _rb.velocity.y);
        _rb.velocity = movement; 
        _animator.SetFloat("Speed", Mathf.Abs(movement.x));
        if(IsGrounded())
        {
            _animator.SetBool("IsJumping", false);
        }
    }

    public void Jump()
    {
        OnJump?.Invoke();
    }

    private void Movment_OnJump()
    {
        if (IsGrounded())
        {
            _rb.velocity = Vector2.up * _jumpForce;
            _animator.SetBool("IsJumping", true);
        }   
    }
    public void HoldJump()
    {
        OnHoldJump?.Invoke();
    }

    private void Movment_OnHoldJump()
    {
        if (IsGrounded())
        {
            _rb.velocity = Vector2.up * _jumpForce;
            _animator.SetBool("IsJumping", true);
        }
    }

    public bool IsGrounded()
    {
        float extraHeight = 0.05f;
        Collider2D groundCheck = Physics2D.OverlapCircle(Feet.position, 0.5f, _groundLayer);
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

        if(groundCheck != null)
        {
            return true;
        }
        else
        {
            return false;
        }


        //return raycastHit.collider != null;
    }

    private void FlipCharacter()
    {
        Vector3 characterScale = transform.localScale;
        if (SimpleInput.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -1;
            
        }
        if (SimpleInput.GetAxis("Horizontal") > 0)
        {
            characterScale.x = 1;
        }

        transform.localScale = characterScale;
    }
}
