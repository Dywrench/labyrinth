using System.Collections;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    // Controles del jugador
    private InputSystem_Actions controls;
    public float speed;
    private Rigidbody rb;
    private Vector2 moveInput;

    // Particulas
    public Transform particles;

    // Control de las particulas
    private ParticleSystem particlesSystem;

    private Vector3 position;

    // Golpes recibidos
    public int golpes = 0;

    // Golpes para volver al inicio
    public int golpesMaximos = 3;

    // Posicion inicial
    private Vector3 posicionInicial;
    // Objetos que faltan
    private int numeroObjetos = 10;
    private int cantidadObjetos = 0;
    public GameObject Final;
    public GameObject platform;
    // Sonido de la pared
    [SerializeField] private AudioSource audioImpact;
    [SerializeField] private AudioSource audioPlatform;
    [SerializeField] private AudioSource damageSound;
    
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Guarda la posicion inicial
        posicionInicial = transform.position;

        particlesSystem = particles.GetComponent<ParticleSystem>();

        var main = particlesSystem.main;
        main.loop = false;

        particlesSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        if (audioImpact == null)
        {
            audioImpact = GetComponent<AudioSource>();
        }

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
        InicializarControles();
    }

    private void InicializarControles()
    {
        if (controls != null)
        {
            return;
        }

        controls = new InputSystem_Actions();

        // Cuando el jugador se mueve
        controls.Player.Move.performed += ctx =>
            moveInput = ctx.ReadValue<Vector2>();

        // Cuando deja de moverse
        controls.Player.Move.canceled += ctx =>
            moveInput = Vector2.zero;
    }

    void OnEnable()
    {
        InicializarControles();
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
        // Recoger objeto
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
            cantidadObjetos++;
            if (cantidadObjetos == numeroObjetos)
            {
                Final.SetActive(false);
            }
        }

        // Pincho
        if (other.gameObject.CompareTag("Damage"))
        {
            damageSound.Play();
            golpes++;

            Debug.Log("Golpe recibido: " + golpes + "/" + golpesMaximos);

            // Si recibe los golpes necesarios
            if (golpes >= golpesMaximos)
            {
                // Volver al inicio
                transform.position = posicionInicial;

                // Parar movimiento
                rb.linearVelocity = Vector3.zero;

                // Reiniciar los objetos
                golpes = 0;
                cantidadObjetos = 0;
                platform.SetActive(true);
               
            }
        }
        // Laser
        if (other.gameObject.CompareTag("LazerDamage"))
        {
            platform.SetActive(true);
            // Volver al inicio
            transform.position = posicionInicial;

            // Parar movimiento
            rb.linearVelocity = Vector3.zero;

        }

        // Terminar nivel 1
        if (other.gameObject.CompareTag("winL1"))
        {
            // Ir al siguiente punto
            transform.position = new Vector3(-54.4f, 9.81f, -27f);

            // Parar movimiento
            rb.linearVelocity = Vector3.zero;

            Debug.Log("¡Nivel 1 completado!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Sonido al chocar con una pared
        if (collision.gameObject.CompareTag("Wall") && audioImpact != null)
        {
            audioImpact.Play();
        }
        if (collision.gameObject.CompareTag("plane") && platform != null && platform.activeSelf)
        {
            platform.SetActive(false);

            if (audioPlatform != null)
            {
                audioPlatform.Play();
            }
        }
    }
}