using UnityEngine;

public class ImpactSong : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       audioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
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
