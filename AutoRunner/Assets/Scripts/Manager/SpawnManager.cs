using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public event System.Action OnRespawn;
    public static SpawnManager Instance;

    public Transform RespawnPoint;
    public GameObject Player { get; private set; }
    public bool IsTutorialOn;

    [SerializeField] private float _transformSpeed = 1.0f; 
    private PlayerCollision _playerCollision;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnPlayer()
    {
        Player = Instantiate(GameManager.Instance.player, RespawnPoint.position, Quaternion.identity);
        _playerCollision = Player.GetComponent<PlayerCollision>();
        _playerCollision.OnDeath += _playerCollision_OnDeath;
    }

    private void OnDestroy()
    {        
        if (_playerCollision)
        {
            _playerCollision.OnDeath -= _playerCollision_OnDeath;
        }
    }

    private void _playerCollision_OnDeath(GameObject player)
    {
        player.GetComponent<CapsuleCollider2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().isKinematic = true;
        StartCoroutine(Respawn(player));
    }
 
    private IEnumerator Respawn(GameObject player)
    {
        yield return new WaitForSeconds(0.2f);
        while (Vector2.Distance(RespawnPoint.position, player.transform.position) > 0.1f)
        {

            player.transform.position = Vector2.MoveTowards(player.transform.position, RespawnPoint.transform.position, _transformSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        player.GetComponent<CapsuleCollider2D>().enabled = true;
        player.GetComponent<Rigidbody2D>().isKinematic = false;
        player.GetComponent<PlayerController>().enabled = true;
        OnRespawn?.Invoke();
    }
}
