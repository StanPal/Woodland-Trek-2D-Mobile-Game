using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private event System.Action OnJump;

    public LayerMask _groundLayer;
    public LayerMask _wallLayer;
    public Transform Feet;


    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    private Animator _animator; 
    private CapsuleCollider2D _capsuleCollider;
    private PlayerCollision _playerCollision;
    private SpawnManager _spawnManager; 

    private bool _isGrounded;
    private float _moveX;
    private float _moveY;
    private float _groundRadius = 0.5f;
    private float _jumpTime;
    private bool _onHoldJump;

    [Header("Horizontal PlayerController")]
    [SerializeField] private float _moveSpeed;

    [Header("Vertical PlayerController")]
    [SerializeField] private float _jumpForce;

    [Header("Wall Jump")]
    [SerializeField] private float _wallJumpTime = 0.0f;
    [SerializeField] private float _wallPushX = 0.0f;
    [SerializeField] private float _wallPushY = 0.0f;


    private RaycastHit2D _wallCheckHit;
    private bool _isWallSliding;
    private bool _isControllerDisabled; 

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        _playerCollision = GetComponent<PlayerCollision>();
    }

    private void Start()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        OnJump += Movment_OnJump;
        _playerCollision.DisableControls += DisableControls;
        _spawnManager.OnRespawn += OnRespawn;
    }

    private void OnRespawn()
    {
        _isControllerDisabled = false;
    }

    private void DisableControls()
    {
        _isControllerDisabled = true;
    }

    private void OnDestroy()
    {
       // _playerCollision.DisableControls -= DisableControls;
       // _spawnManager.OnRespawn -= OnRespawn;

    }

    private void Update()
    {
        _moveX = SimpleInput.GetAxis("Horizontal");

        if(IsGrounded() && Input.GetButtonDown("Jump"))
        {
            Jump();
            _animator.SetBool("IsJumping", true);
        }

        if (_wallJumpTime > 0.2f)
        {
            _rb.velocity = new Vector2(_moveX * _moveSpeed, _rb.velocity.y);

            if (OnWall() && !IsGrounded())
            {
                _rb.gravityScale = 0.5f;
                _rb.velocity = Vector2.zero;
            }
            else
            {
                _rb.gravityScale = 1;
            }
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            _wallJumpTime += Time.deltaTime;
        }

        if(_onHoldJump)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        FlipCharacter();
        Vector2 movement = new Vector2(_moveX * _moveSpeed, _rb.velocity.y);
        _rb.velocity = movement; 
        _animator.SetFloat("Speed", Mathf.Abs(movement.x));
                  
    }

    public void Jump()
    {
        OnJump?.Invoke();
    }

    private void Movment_OnJump()
    {
            if (IsGrounded())
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                _animator.SetBool("IsJumping", true);
                _animator.SetBool("IsGrounded", false);
            }
            else if (OnWall() && !IsGrounded())
            {
                if (_moveX == 0)
                {
                    _rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * _wallPushX * 10, 0);
                    transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);

                }
                else
                {
                    _rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * _wallPushX, _wallPushY);

                }
                _wallJumpTime = 0;
            }        

    }

    public void HoldJump()
    {
        _onHoldJump = true;
    }

    public void ReleaseHoldJump()
    {
        _onHoldJump = false;
    }

    public bool IsGrounded()
    {
        _animator.SetBool("IsGrounded",true);
        float extraHeight = 0.05f;
        Collider2D groundCheck = Physics2D.OverlapCircle(Feet.position, _groundRadius, _groundLayer);
        RaycastHit2D raycastHit = Physics2D.Raycast(_capsuleCollider.bounds.center, Vector2.down, _capsuleCollider.bounds.extents.y + extraHeight, _groundLayer);
        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(_capsuleCollider.bounds.center, Vector2.down * (_capsuleCollider.bounds.extents.y + extraHeight), rayColor);

        if (groundCheck != null)
        {
            _animator.SetBool("IsJumping", false);
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_capsuleCollider.bounds.center, _capsuleCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, _wallLayer);
        return raycastHit.collider != null;
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
