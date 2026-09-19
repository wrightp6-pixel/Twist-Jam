// This class ensures that a player moves along with a moving platform when they are on one
// Make sure AutoSyncTransform is on in Project settinsg for this to work
// Tuttorial: https://www.youtube.com/watch?v=s6chmaGuDFY

using UnityEngine;

public class AttactchToPlatform : MonoBehaviour
{
    // The transform component of the platform
    [SerializeField] Transform platform;
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide");
        // Mak eth Player a child of the platform to ensur ethey move together
        if (other.gameObject.CompareTag("PlayerCharacter"))
        {
            other.gameObject.transform.SetParent(platform,false);
            Debug.Log("Parented");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        // Unparent the object so that teh player is no longer connected to platform's movement
        if (other.gameObject.CompareTag("PlayerCharacter"))
        {
            other.gameObject.transform.SetParent(null);
            Debug.Log("Unparented");
        }
    }




}
