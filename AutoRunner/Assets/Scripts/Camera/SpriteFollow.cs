using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFollow : MonoBehaviour
{
    private Camera _camera;
    private Animator _animator; 


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _camera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        this.transform.position = new Vector2(_camera.transform.position.x - 15.0f, this.transform.position.y);
    }

    private void FixedUpdate()
    {
        _animator.SetFloat("Speed", 1.0f);
    }
}
