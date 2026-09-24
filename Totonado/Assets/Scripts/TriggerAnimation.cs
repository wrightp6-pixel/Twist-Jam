using System.Collections;
using UnityEngine;

// Trigger an animation with a sound effect at a specific point
public class TriggerAnimation : MonoBehaviour
{
    [SerializeField] private GameObject animatedObject;
    [SerializeField] private AudioClip clip;
    [SerializeField] private float audioDelay;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerCharacter"))
        {
            animatedObject.SetActive(true);
            StartCoroutine(waitSound());
        }
    }

    IEnumerator waitSound()
    {
        yield return new WaitForSeconds(audioDelay);
        source.PlayOneShot(clip);
    }
}
