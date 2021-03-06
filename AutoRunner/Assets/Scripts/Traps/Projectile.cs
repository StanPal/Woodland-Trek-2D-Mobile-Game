using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _resetTime;
    //private float _lifeTime; 

    private ArrowTrap _arrowTrap;

    private void Awake()
    {
        _arrowTrap = GetComponentInParent<ArrowTrap>();
    }

    private void Start()
    {
        _speed = _arrowTrap.ArrowSpeed;
    }

    public void ActivateProjectile()
    {
        //_lifeTime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float movementSpeed = _speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        //_lifeTime += Time.deltaTime;
        //if(_lifeTime > _resetTime)
        //{
        //    gameObject.SetActive(false);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerCollision>())
        {
            collision.gameObject.GetComponent<PlayerCollision>().InvokeOnDeath();
            gameObject.SetActive(false);
        } 
        if(collision.tag.Equals("Wall"))
        {
            gameObject.SetActive(false);
        }  
    }
}
