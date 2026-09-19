using UnityEngine;

public class LazerToggle : MonoBehaviour
{
    [Header("Tiempos")]
    [SerializeField] private float tiempoActivo = 2f;
    [SerializeField] private float tiempoInactivo = 2f;
    [SerializeField] private bool empiezaActivo = true;

    [Header("Sonido")]
    [SerializeField] private LazerSound lazerSound;
    [SerializeField] private bool detenerSonidoAlDesactivar = false;

    private Renderer[] renderers;
    private Collider[] colliders;
    private AudioSource audioSource;
    private float timer;
    private bool activo;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>(true);
        colliders = GetComponentsInChildren<Collider>(true);

        if (lazerSound == null)
        {
            lazerSound = GetComponent<LazerSound>();
        }

        audioSource = GetComponent<AudioSource>();

        activo = empiezaActivo;
        timer = activo ? tiempoActivo : tiempoInactivo;
        SetEstado(activo, reproducirSonido: false);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            activo = !activo;
            SetEstado(activo);
            timer = activo ? tiempoActivo : tiempoInactivo;
        }
    }

    private void SetEstado(bool estado, bool reproducirSonido = true)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = estado;
        }

        foreach (Collider collider in colliders)
        {
            collider.enabled = estado;
        }

        if (reproducirSonido)
        {
            if (estado)
            {
                lazerSound?.Reproducir();
            }
            else if (detenerSonidoAlDesactivar && audioSource != null)
            {
                audioSource.Stop();
            }
        }
    }
}
