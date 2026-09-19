using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Jugador que sigue la camara
    public GameObject player;

    // Distancia entre la camara y el jugador
    private Vector3 offset;

    // Limites de la camara
    public float limiteXMin = -10f;
    public float limiteXMax = 10f;

    public float limiteZMin = -10f;
    public float limiteZMax = 10f;

    // Velocidad de la camara
    public float velocidadCamara = 5f;

    void Start()
    {
        // Guarda la distancia inicial
        offset = transform.position - player.transform.position;
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        // Nueva posicion de la camara
        Vector3 nuevaPosicion = player.transform.position + offset;

        // No deja que la camara salga de los limites
        nuevaPosicion.x = Mathf.Clamp(
            nuevaPosicion.x,
            limiteXMin,
            limiteXMax
        );

        nuevaPosicion.z = Mathf.Clamp(
            nuevaPosicion.z,
            limiteZMin,
            limiteZMax
        );

        // Mueve la camara suavemente
        transform.position = Vector3.Lerp(
            transform.position,
            nuevaPosicion,
            velocidadCamara * Time.deltaTime
        );
    }
}