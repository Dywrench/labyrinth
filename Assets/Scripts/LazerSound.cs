using UnityEngine;

public class LazerSound : MonoBehaviour
{
    private AudioSource audioSource;

   private void Awake()
    {
      audioSource = GetComponent<AudioSource>();
    }

   public void Reproducir()
    {
      if (audioSource != null)
      {
         audioSource.Play();
      }
    }
}
