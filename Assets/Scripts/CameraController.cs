using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Jugador que seguira la camara
    public GameObject player;

    // Distancia de la camara respecto al jugador
    private Vector3 offset;

    // Limites de movimiento de la camara
    public float limiteXMin = -10f;
    public float limiteXMax = 10f;

    public float limiteZMin = -10f;
    public float limiteZMax = 10f;

    // Velocidad con la que la camara sigue al jugador
    public float velocidadCamara = 5f;

    void Start()
    {
        // Guarda la distancia inicial entre la camara y el jugador
        offset = transform.position - player.transform.position;
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        // Posicion que deberia tener la camara siguiendo al jugador
        Vector3 nuevaPosicion = player.transform.position + offset;

        // Limita el movimiento horizontal de la camara
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

        // Movimiento suave hacia la nueva posicion
        transform.position = Vector3.Lerp(
            transform.position,
            nuevaPosicion,
            velocidadCamara * Time.deltaTime
        );
    }
}