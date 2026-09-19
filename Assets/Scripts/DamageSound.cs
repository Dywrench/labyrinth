using UnityEngine;

public class DamageSound : MonoBehaviour
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
          if(collision.gameObject.CompareTag("Damage")){
             audioSource.Play();
          }
    }
}
