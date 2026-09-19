using UnityEngine;

public class ImpactSong : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
       audioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
          if(collision.gameObject.CompareTag("Sphere")){
             audioSource.Play();
          }
    }
}
