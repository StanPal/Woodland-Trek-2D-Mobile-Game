using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    [SerializeField] private float _timeToTogglePlatform;
    [SerializeField] private float _fadeSpeed;
    [SerializeField] private float _returnDelay;
    [SerializeField] private float _alphaFadeValue;
    private float _currentTime = 0;
    private bool _isEnabled = true;
    private bool _isFading; 

    private void Start()
    {
        _isEnabled = true; 
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime >= _timeToTogglePlatform && !_isFading)
        {
            _currentTime = 0;
            TogglePlatform();
        }
    }

    private void TogglePlatform()
    {
        _isEnabled = !_isEnabled;
        foreach(Transform child in gameObject.transform)
        {
            StartCoroutine(PlatformFade(child.gameObject, _isEnabled));
            //child.gameObject.SetActive(_isEnabled);
        }
    }

    private IEnumerator PlatformFade(GameObject platform, bool enabled)
    {
        float alphaValue = 1.0f;
        SpriteRenderer platformSprite = platform.GetComponent<SpriteRenderer>();
        if (enabled)
        {
            yield return new WaitForSeconds(_returnDelay);
            platform.SetActive(enabled);
            platformSprite.material.color = new Color(platformSprite.material.color.r,
                  platformSprite.material.color.b, platformSprite.material.color.g, alphaValue);
        }
        else
        {
            _isFading = true;
            while (platformSprite.material.color.a > 0)
            {
                platformSprite.material.color = new Color(platformSprite.material.color.r,
                    platformSprite.material.color.b, platformSprite.material.color.g, alphaValue);
                yield return new WaitForSeconds(_fadeSpeed);
                alphaValue -= _alphaFadeValue;
            }
            platform.SetActive(enabled);
            _isFading = false;
        }
    }

}
