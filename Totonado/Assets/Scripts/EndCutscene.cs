using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutscene : MonoBehaviour
{
    [SerializeField] private  AudioClip water;
    [SerializeField] private AudioClip bark;
    [SerializeField] private AudioClip melting;
    private AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();

        // Cutscene
        source.PlayOneShot(bark);
        StartCoroutine(waitAudio(0.35f, water));
        StartCoroutine(waitAudio(1f, melting));
        StartCoroutine(waitTitleScreen());


    }

    IEnumerator waitAudio(float seconds, AudioClip clip)
    {
        yield return new WaitForSeconds(seconds);
        source.PlayOneShot(clip);
    }

    IEnumerator waitTitleScreen()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadSceneAsync("TitleScreen");
    }

    
}
