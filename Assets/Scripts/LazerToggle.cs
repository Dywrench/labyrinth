using UnityEngine;

public class LazerToggle : MonoBehaviour
{
    [Header("Tiempos")]
    [SerializeField] private float tiempoActivo = 2f;
    [SerializeField] private float tiempoInactivo = 2f;
    [SerializeField] private bool empiezaActivo = true;

    private Renderer[] renderers;
    private Collider[] colliders;
    private float timer;
    private bool activo;

    private void Awake()
    {
        // Busca componentes en este objeto y en todos sus hijos.
        renderers = GetComponentsInChildren<Renderer>(true);
        colliders = GetComponentsInChildren<Collider>(true);

        activo = empiezaActivo;
        timer = activo ? tiempoActivo : tiempoInactivo;
        SetEstado(activo);
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

    private void SetEstado(bool estado)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = estado;
        }

        foreach (Collider collider in colliders)
        {
            collider.enabled = estado;
        }
    }
}
