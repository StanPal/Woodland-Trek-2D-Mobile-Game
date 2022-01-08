using UnityEngine;

public class Movment : MonoBehaviour
{
    private event System.Action OnJump;
    private event System.Action OnHoldJump;

    public LayerMask _groundLayer;
    public LayerMask _wallLayer;
    public Transform Feet;


    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    private Animator _animator; 
    private BoxCollider2D _boxCollider;

    private bool _isTouchingFront = true;
    private bool _isWallSliding;
    private bool _isGrounded;
    private float _moveX;
    private float _moveY;
    private float _groundRadius = 0.5f;
    private float _jumpTime;
    private RaycastHit2D _wallCheckHit; 

    [Header("Horizontal Movement")]
    [SerializeField] private float _moveSpeed;

    [Header("Vertical Movement")]
    [SerializeField] private float _jumpForce;

    [Header("Wall Jump")]
    [SerializeField] private float _wallJumpTime = 0.2f;
    [SerializeField] private float _wallSlidingSpeed = 0.3f;
    [SerializeField] private float _wallDistance = 0.5f;

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

        if(IsGrounded() && Input.GetButtonDown("Jump") || _isWallSliding && Input.GetButton("Jump"))
        {       
            _rb.velocity = Vector2.up * _jumpForce;
            _animator.SetBool("IsJumping", true);
        }

        //_isTouchingFront = Physics2D.OverlapCircle(FrontCheck.position, _groundRadius);

    }

    private void FixedUpdate()
    {
        FlipCharacter();
        Vector2 movement = new Vector2(_moveX * _moveSpeed, _rb.velocity.y);
        _rb.velocity = movement; 
        _animator.SetFloat("Speed", Mathf.Abs(movement.x));
   
        if (_isTouchingFront)
        {
            _wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(_wallDistance, 0), _wallDistance, _wallLayer);
        }
        else
        {
            _wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-_wallDistance, 0), _wallDistance, _wallLayer);
        }

        if (_wallCheckHit && !IsGrounded() && _moveX != 0)
        {
            _isWallSliding = true;
            _jumpTime = Time.time + _wallJumpTime;
        }
        else if (_jumpTime < Time.time)
        {
            _isWallSliding = false;
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

        if (_isWallSliding)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, _wallSlidingSpeed, float.MaxValue));
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
        Collider2D groundCheck = Physics2D.OverlapCircle(Feet.position, _groundRadius, _groundLayer);
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
            _animator.SetBool("IsJumping", false);
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
