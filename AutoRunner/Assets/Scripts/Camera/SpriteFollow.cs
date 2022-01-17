using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFollow : MonoBehaviour
{

    private Camera _camera;
    private void Start()
    {
        _camera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        this.transform.position = new Vector2(_camera.transform.position.x, this.transform.position.y);
    }
}
