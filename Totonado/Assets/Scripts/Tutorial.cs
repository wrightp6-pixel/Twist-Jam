using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public PlayerMove player;
    public string message;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCharacter"))
        {
            player.setTutorialText(message);
        }
    }
}
