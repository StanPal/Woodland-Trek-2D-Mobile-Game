using UnityEngine;


public class Serizawa : MonoBehaviour
{
    public GameObject Message;

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
