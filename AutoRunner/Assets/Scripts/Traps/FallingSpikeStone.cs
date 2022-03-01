using System.Collections;
using UnityEngine;

public class FallingSpikeStone : MonoBehaviour
{
    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _targetPosition; 
    [SerializeField] private float _blinkTimer;
    [SerializeField] private float _returnSpeed;
    [SerializeField] private float _moveSpeed;
    private Animator _animator;
    private BoxCollider2D _boxCollider;
    private PolygonCollider2D _polygonCollider2D;
    private bool _isMovingBack;
    private bool _isBlinking;
    private bool _isActivated; 

    void Start()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMovingBack)
        {
            StartCoroutine(ReturnToStart());
        }

        if(_isActivated && !_isMovingBack)
        {
            StartCoroutine(MoveToPoint());
        }

        if (!_isBlinking)
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
        while (Vector2.Distance(transform.position, _startPos.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPos.position, _returnSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        _isMovingBack = false;
    }

    private IEnumerator MoveToPoint()
    {
        _isActivated = true;
        while(Vector2.Distance(transform.position, _targetPosition.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPosition.position, _moveSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        _isActivated = false;
        _isMovingBack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isMovingBack)
        {
            if (collision.gameObject.GetComponent<Character>())
            {
                _isActivated = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_isMovingBack)
        {
            if (collision.gameObject.GetComponent<Character>())
            {
                _isActivated = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isMovingBack)
        {
            if (collision.gameObject.GetComponent<Character>())
            {
                collision.gameObject.GetComponent<PlayerCollision>().InvokeOnDeath();
            }
        }
    }
}
