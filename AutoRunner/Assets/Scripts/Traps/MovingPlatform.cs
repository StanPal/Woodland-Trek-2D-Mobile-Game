using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IMovable
{
    public List<Transform> Points;
    public Transform platform;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _endPoint = 0;
    [SerializeField] private float _distanceBetweenPoints = 0.1f;

    // Update is called once per frame
    void Update()
    {
        MoveToNextPoint();
    }
    

    public void MoveToNextPoint()
    {
        platform.position = Vector2.MoveTowards(platform.position, Points[_endPoint].position, Time.deltaTime * _moveSpeed);

        if (Vector2.Distance(platform.position, Points[_endPoint].position) < _distanceBetweenPoints)
        {
            if (_endPoint == Points.Count - 1)
            {
                _endPoint = 0;
            }
            else
            {
                _endPoint++;
            }
        }
    }
}
