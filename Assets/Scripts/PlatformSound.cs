using UnityEngine;


public class PlatformSound : MonoBehaviour
{
    private AudioSource audioSource;
    private bool platformWasActive;
    public GameObject platform;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        platformWasActive = platform != null && platform.activeSelf;
    }

    void Update()
    {
        if (platform == null)
        {
            return;
        }

        if (platformWasActive && !platform.activeSelf && audioSource != null)
        {
            audioSource.Play();
        }

        platformWasActive = platform.activeSelf;
    }
}
