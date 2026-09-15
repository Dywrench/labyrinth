using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private InputSystem_Actions controls;
    public float speed;
    private Rigidbody rb;
    private Vector2 moveInput;
    public Transform particles;
    private ParticleSystem particlesSystem;
    private Vector3 position;
    void Start()
    {
        //se obtiene el componente de la esfera
        rb = GetComponent<Rigidbody>();
        //se obtiene el componente del sistema de particulas
        particlesSystem = particles.GetComponent<ParticleSystem>();
        //este metodo permite detener la emision de particulas al inicio del juego
        particlesSystem.Stop();
    }
    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveInput.x, 0.0f, moveInput.y); //movement input ayuda pa que se genere una fuerza en la esfera
        rb.AddForce(movement * speed);//llama metodo Addforce (añadir fuerza a la direccion del movimiento) del metodo rb (rigidbody) 
    }
    void Awake()
    {
        controls = new InputSystem_Actions();
        // enlaza al input de movimiento, es para detectar cuando el jugador mueve o deja de mover el control
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }
    void OnEnable()
    {
        controls.Enable();
    }
    void OnDisable()
    {
        controls.Disable();
    }
    void OnTriggerEnter(Collider other)
    {
          // el objeto es recolectable
        if (other.gameObject.CompareTag("Collectable"))
        {
            // obtiene la posicion del objeto recolectable
            position = other.gameObject.transform.position; 
            particles.position = position; // se asigna la posicion del objeto recolectable a la posicion del sistema de particulas
            particlesSystem = particles.GetComponent<ParticleSystem>(); // se obtiene el componente del sistema de particulas
            particlesSystem.Play(); // se activa el sistema de particulas
            // desactiva el objeto recolectable (lo oculta de la escena)  
            other.gameObject.SetActive(false);
        }
        else // el objeto no es recolectable
        {
           
        }   
    }

    


}
