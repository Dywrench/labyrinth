using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Referencia al sistema de controles
    private InputSystem_Actions controls;

    public float speed;
    private Rigidbody rb;
    private Vector2 moveInput;

    // Referencia al objeto de particulas
    public Transform particles;

    // Sistema de particulas
    private ParticleSystem particlesSystem;

    private Vector3 position;

    // GOLPES RECIBIDOS
    public int golpes = 0;

    // Golpes necesarios para volver al inicio
    public int golpesMaximos = 3;

    // Posicion inicial del jugador
    private Vector3 posicionInicial;
    //Desaparecer objeto
    private int numeroObjetos = 10;
    private int cantidadObjetos = 0;
    public GameObject Final;
    //Sistema de auido
    private AudioSource audioImpact;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Guarda la posicion inicial
        posicionInicial = transform.position;

        particlesSystem = particles.GetComponent<ParticleSystem>();

        var main = particlesSystem.main;
        main.loop = false;

        particlesSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        audioImpact = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(
            moveInput.x,
            0.0f,
            moveInput.y
        );

        rb.AddForce(movement * speed);
    }

    void Awake()
    {
        controls = new InputSystem_Actions();

        // Detecta cuando el jugador se mueve
        controls.Player.Move.performed += ctx =>
            moveInput = ctx.ReadValue<Vector2>();

        // Detecta cuando deja de moverse
        controls.Player.Move.canceled += ctx =>
            moveInput = Vector2.zero;
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        if (controls != null)
        {
            controls.Disable();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // COLECCIONABLE
        if (other.gameObject.CompareTag("Collectable"))
        {
            position = other.gameObject.transform.position;

            particles.position = position;

            particlesSystem.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear
            );

            particlesSystem.Play();

            other.gameObject.SetActive(false);
            audioImpact.Play();
        }
        
         if (other.gameObject.CompareTag("Collectable2"))
        {
            position = other.gameObject.transform.position;
            particles.position = position;
            particlesSystem.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear
            );
            particlesSystem.Play();
            other.gameObject.SetActive(false);
            audioImpact.Play();
            cantidadObjetos ++;
               if(cantidadObjetos == numeroObjetos)
               {
                Final.SetActive(false);
               }
        }
       
        // DAÑO DEL PINCHO
        if (other.gameObject.CompareTag("Damage"))
        {
            golpes++;

            Debug.Log("Golpe recibido: " + golpes + "/" + golpesMaximos);

            // Si llega a 3 golpes
            if (golpes >= golpesMaximos)
            {
                // Volver al inicio
                transform.position = posicionInicial;

                // Detener movimiento
                rb.linearVelocity = Vector3.zero;

                // Reiniciar contador
                golpes = 0;
                cantidadObjetos = 0;
                Debug.Log("¡Tres golpes! Volviendo al inicio.");
            }
        }

        // PLACEHOLDER PARA GANAR EL NIVEL
        if (other.gameObject.CompareTag("winL1"))
        {
            // Volver al inicio
            transform.position = posicionInicial;

            // Detener movimiento
            rb.linearVelocity = Vector3.zero;

            Debug.Log("¡Nivel 1 completado!");
        }
    }
}