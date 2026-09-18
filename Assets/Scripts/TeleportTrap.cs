using UnityEngine;

public class TeleportTrap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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
 
         }

     }
}

