using Cinemachine;
using UnityEngine;


public class Serizawa : MonoBehaviour
{
    public GameObject Message;
    private CinemachineVirtualCamera _cinmemachineVirtualCamera;

    private void Start()
    {
        _cinmemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        if(GameManager.Instance.State == GameState.Tutorial)
        {
            _cinmemachineVirtualCamera.Follow = this.gameObject.transform;
            _cinmemachineVirtualCamera.LookAt = this.gameObject.transform;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerCollision>(out PlayerCollision player))
        {
            Message.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerCollision>(out PlayerCollision player))
        {
            Message.SetActive(false);
        }
    }
}
