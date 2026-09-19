using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioClip msuicClip;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = msuicClip;
        audioSource.Play();
    }

    
}
