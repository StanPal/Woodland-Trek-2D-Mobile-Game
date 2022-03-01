using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destroyEffect;


    public void PlayEffect()
    {
        Instantiate(_destroyEffect, this.transform.position, Quaternion.identity);
    }    
}
