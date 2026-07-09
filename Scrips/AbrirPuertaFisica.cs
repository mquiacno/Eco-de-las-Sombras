using UnityEngine;

public class AbrirPuertaFisica : MonoBehaviour
{
    private bool jugadorDetectado = false;
    private Quaternion rotacionInicial;
    private Quaternion rotacionObjetivo;

    [Header("Configuración del Giro")]
    [Tooltip("90 para abrir a la derecha, -90 para abrir a la izquierda")]
    public float anguloApertura = -90f;
    public float velocidadSuave = 3f;

    void Start()
    {
       
        rotacionInicial = transform.rotation;

        
        rotacionObjetivo = rotacionInicial * Quaternion.Euler(0, anguloApertura, 0);
    }

    void Update()
    {
        
        if (jugadorDetectado)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * velocidadSuave);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Player detectado! Abriendo de forma limpia y controlada.");
            jugadorDetectado = true;
        }
    }
}