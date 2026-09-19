using UnityEngine;

public class TeleportTrap : MonoBehaviour
{
    private AudioSource audioTeleport;
    void Start()
    {
        audioTeleport = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public Transform teleportTarget;
 
 
     public GameObject Player;
     
 
 
     void OnTriggerEnter(Collider other)
     {
         if (other.gameObject.CompareTag("Sphere"))
         {
          
             Player.transform.position = teleportTarget.transform.position;
            audioTeleport.Play();
         }

     }
}

