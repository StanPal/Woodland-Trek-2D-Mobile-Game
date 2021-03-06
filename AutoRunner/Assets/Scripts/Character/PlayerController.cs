using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private event System.Action OnJump;
    private event System.Action OnHoldJump;
    private event System.Action OnReleaseJump;

    public ParticleSystem DustEffect;
    public ParticleSystem JumpEffect;
    private ParticleSystem _jumpEffectModifier; 

    public LayerMask _groundLayer;
    public Transform Feet;

    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    private Animator _animator; 
    private CapsuleCollider2D _capsuleCollider;
    private PlayerCollision _playerCollision;
    private SpawnManager _spawnManager;
    private Color _originalSpriteColor;
    
    private float _moveX;
    private float _groundRadius = 0.5f;
    [SerializeField] private float _holdTime; 
    [SerializeField] private bool _isHoldJump;
    private bool _isFacingRight;
    private bool _isControllerDisabled;

    [Header("For Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _airMoveSpeed;
    [SerializeField] private float rayLength = 0.05f;

    [Header("For Jumping")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _originalJumpForce;
    [SerializeField] private float _jumpMultiplier;
    [SerializeField] private float _jumpChargeSpeed;
    [SerializeField] private float _coyoteTime = 0.2f;
    [SerializeField] private float _jumpBufferTime = 0.2f;
    [SerializeField] private float _jumpCoolDown = 0.2f;
    [SerializeField] private float jumpTimer;
    private bool isJumpReady; 
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private bool canPlayJumpEffect;
    

    [Header("Wall Jump")]
    [SerializeField] private LayerMask _wallLayer;
    //[SerializeField] private Vector2 _wallJumpAngle;
    [SerializeField] private float _wallJumpForce;
    [SerializeField] private float _wallDistance;
    [SerializeField] private float _wallSlideSpeed;
    [SerializeField] private float xwallForce;
    [SerializeField] private float yWallForce;
    [SerializeField] private float _wallJumpTime;
    private RaycastHit2D _wallCheckHit;
    //private float _wallJumpDirection;
    private bool _isWallSliding;
    private bool _wallJumping; 

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        _playerCollision = GetComponent<PlayerCollision>();
        _jumpEffectModifier = JumpEffect.GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();

        _originalSpriteColor = _sprite.color;
       // _wallJumpAngle.Normalize();

        _playerCollision.DisableControls += DisableControls;

        _spawnManager.OnRespawn += OnRespawn;
        OnJump += Movment_OnJump;
        OnHoldJump += HoldJump;
        OnReleaseJump += ReleaseHoldJump;
    }


    private void OnDestroy()
    {
        _playerCollision.DisableControls -= DisableControls;
        _spawnManager.OnRespawn -= OnRespawn;
        OnJump -= Movment_OnJump;
        OnHoldJump -= HoldJump;
        OnReleaseJump -= ReleaseHoldJump;
    }
    private void OnRespawn()
    {
        _isControllerDisabled = false;
    }

    private void DisableControls()
    {
        _isControllerDisabled = true;
    }

    private void Update()
    {
        if (GameManager.Instance.State == GameState.Playable)
        {
            _moveX = SimpleInput.GetAxis("Horizontal");
            if(IsGrounded())
            {
                coyoteTimeCounter = _coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }
            if (Input.GetButtonDown("Jump"))
            {
                jumpBufferCounter = _jumpBufferTime;
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
            }
            if (coyoteTimeCounter > 0f && Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            if(jumpTimer >= _jumpCoolDown)
            {
                isJumpReady = true; 
            }
            else
            {
                jumpTimer = jumpTimer + Time.deltaTime;
                isJumpReady = false; 
            }
            if ((_isWallSliding || _wallCheckHit) && Input.GetButtonDown("Jump"))
            {
                _wallJumping = true;
                Invoke("SetWallJumpingToFalse", _wallJumpTime);
            }
            if (_wallJumping == true)
            {
                WallJump();
            }

        }
    }

    private void SetWallJumpingToFalse()
    {
        _wallJumping = false; 
    }

    private void FixedUpdate()
    {
        FlipCharacter();
        if (IsGrounded())
        {
            Movement();
        }
        else if (!IsGrounded() && !_isWallSliding && _moveX != 0)
        {
            _rb.AddForce(new Vector2(_airMoveSpeed * _moveX, 0));
            if (Mathf.Abs(_rb.velocity.x) > _moveSpeed)
            {
                _rb.velocity = new Vector2(_moveX * _moveSpeed, _rb.velocity.y);
            }
        }
        FacingWallCheck();
        WallSlide();

    }

    #region Movement
    private void Movement()
    {
        Vector2 movement = new Vector2(_moveX * _moveSpeed, _rb.velocity.y);
        _rb.velocity = movement;
        _animator.SetFloat("Speed", Mathf.Abs(movement.x));
        if (IsGrounded())
        {
            if (_rb.velocity.x > 0.3f || _rb.velocity.x < -0.3f)
            {
                CreateDust();
            }
            else
            {
                StopDust();
            }
        }
    }

    private bool IsGrounded()
    {        
        Collider2D groundCheck = Physics2D.OverlapCircle(Feet.position, _groundRadius, _groundLayer);
        RaycastHit2D raycastHit = Physics2D.Raycast(_capsuleCollider.bounds.center, Vector2.down, _capsuleCollider.bounds.extents.y + rayLength, _groundLayer);
        Color rayColor;

        if (raycastHit.collider != null)
        {
            _animator.SetBool("IsGrounded", true);
            rayColor = Color.green;
        }
        else
        {
            _animator.SetBool("IsGrounded", false);
            StopDust();
            rayColor = Color.red;
        }
        Debug.DrawRay(_capsuleCollider.bounds.center, Vector2.down * (_capsuleCollider.bounds.extents.y + rayLength), rayColor);

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

    private void FlipCharacter()
    {
        if (!_isWallSliding)
        {
            Vector3 characterScale = transform.localScale;
            if (SimpleInput.GetAxis("Horizontal") < 0)
            {
                //_wallJumpDirection = 1;
                characterScale.x = -1;
                _isFacingRight = false;
            }
            if (SimpleInput.GetAxis("Horizontal") > 0)
            {
                //_wallJumpDirection = -1;
                characterScale.x = 1;
                _isFacingRight = true;
            }
            transform.localScale = characterScale;
        }
    }
    #endregion

    #region Jump

    public void Jump()
    {
        if (isJumpReady)
        {
            OnJump?.Invoke();
        }
    }

    public void HoldJumpPress()
    {
        OnHoldJump?.Invoke();
    }

    public void ReleaseHoldJumpPress()
    {
        OnReleaseJump?.Invoke();
    }

    private void Movment_OnJump()
    {
        if (GameManager.Instance.State == GameState.Playable)
        {
            if (!_isControllerDisabled)
            {
                jumpBufferCounter = _jumpBufferTime;
                if (coyoteTimeCounter > 0f && !_isHoldJump)
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                    _animator.SetTrigger("JumpTrigger");
                    CreateJumpDust();
                    StopCoroutine(HoldingJump());
                    coyoteTimeCounter = 0f;
                    jumpBufferCounter = 0f;
                    jumpTimer = 0f;
                    SoundManager.Instance.PlayCharacterEffect(0);

                }
                else if ((_isWallSliding || _wallCheckHit) && _moveX != 0)
                {
                    _wallJumping = true;
                    Invoke("SetWallJumpingToFalse", _wallJumpTime);
                }                
            }
        }
    }

    private void HoldJump()
    {         
        if (IsGrounded())
        {
            _isHoldJump = true;
            ModifyJumpParticle(0.1f);
        }
        if(_isHoldJump)
        {
            _jumpForce = _originalJumpForce;
            StartCoroutine(StartHoldTime());
        }
    }

    private void ReleaseHoldJump()
    {
        _isHoldJump = false;
        _holdTime = 0;
        StopCoroutine(HoldingJump());
        if(canPlayJumpEffect)
        {
            canPlayJumpEffect = false;
            SoundManager.Instance.PlayCharacterEffect(1);
        }
    }
    
    private IEnumerator StartHoldTime()
    {
        _holdTime = 0;
        while(_holdTime < 2.0f)
        {
            _holdTime += 1.0f;
            yield return new WaitForSeconds(1.0f);
        }
        StartCoroutine(HoldingJump());
        StartCoroutine(FlashingSprite());
    }

    private IEnumerator HoldingJump()
    {
        while (_isHoldJump)
        {
            if (_jumpForce >= _originalJumpForce * _jumpMultiplier)
            {
                _jumpForce = _originalJumpForce * _jumpMultiplier;
                canPlayJumpEffect = true;
            }
            _jumpForce += _jumpChargeSpeed;
            ModifyJumpParticle(0.5f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator FlashingSprite()
    {
        while (_isHoldJump)
        {
            for (int i = 0; i < 2; i++)
            {
                SetSpriteColor(Color.yellow);
                yield return new WaitForSeconds(0.1f);
                SetSpriteColor(_originalSpriteColor);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private void SetSpriteColor(Color spriteColor)
    {
        _sprite.color = spriteColor;
    }

    #endregion

    #region WallJump

    private void FacingWallCheck()
    {
        if (_isFacingRight)
        {
            _wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(_wallDistance, 0), _wallDistance, _wallLayer);
            Debug.DrawRay(transform.position, new Vector3(_wallDistance, 0), Color.blue);
        }
        else
        {
            _wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-_wallDistance, 0), _wallDistance, _wallLayer);
        }
    }

    private void WallSlide()
    {
        if(_wallCheckHit && !IsGrounded() && _rb.velocity.y < 0)
        {
            _isWallSliding = true;
        }
        else
        {
            _isWallSliding = false; 
        }

        if(_isWallSliding)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y,_wallSlideSpeed,float.MaxValue));
        }
    }

    private void WallJump()
    {
        _rb.velocity = new Vector2(xwallForce * -_moveX, yWallForce);
        //_rb.AddForce(new Vector2(
        //    _wallJumpForce * _wallJumpDirection * _wallJumpAngle.x, _wallJumpForce * _wallJumpAngle.y), ForceMode2D.Impulse);
    }
    #endregion

    #region Particle Effects
    private void CreateDust()
    {
        DustEffect.Play();
    }

    private void CreateJumpDust()
    {
        JumpEffect.Play();
    }

    private void StopDust()
    {
        DustEffect.Stop();
    }

    private void ModifyJumpParticle(float size)
    {
        var mainSizePS = _jumpEffectModifier.main;
        mainSizePS.startSize = size;
    }
    #endregion
}
