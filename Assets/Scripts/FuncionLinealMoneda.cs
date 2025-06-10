using UnityEngine;

public class FuncionLinealMoneda : MonoBehaviour
{
    [Header("Parámetros de la función lineal")]
    [Tooltip("Pendiente de la función (m en y = mx + b)")]
    public float pendiente = 0.5f;
    [Tooltip("Término independiente (b en y = mx + b)")]
    public float terminoIndependiente = 1f;

    [Header("Rango de movimiento")]
    [Tooltip("Valor mínimo de X")]
    public float xMin = -5f;
    [Tooltip("Valor máximo de X")]
    public float xMax = 5f;

    [Header("Configuración de movimiento")]
    [Tooltip("Velocidad de movimiento")]
    public float velocidad = 2f;
    [Tooltip("Precisión para considerar que llegó al punto")]
    public float precisionLlegada = 0.1f;

    private GameObject moneda;
    private Vector2 puntoActual;
    private Vector2 puntoDestino;
    private bool yendoHaciaMax = true;

    void Start()
    {
        moneda = GameObject.FindWithTag("monedaFuncionLineal");

        if (moneda == null)
        {
            Debug.LogError("No se encontró ningún objeto con tag 'monedaFuncionLineal'");
            return;
        }

        CalcularPuntosDestino();

        // Posicionar la moneda en el punto inicial
        moneda.transform.position = puntoActual;
    }

    void Update()
    {
        if (moneda == null) return;

        // Mover la moneda hacia el punto destino
        moneda.transform.position = Vector2.MoveTowards(
            moneda.transform.position,
            puntoDestino,
            velocidad * Time.deltaTime
        );

        // Verificar si llegó al punto destino
        if (Vector2.Distance(moneda.transform.position, puntoDestino) < precisionLlegada)
        {
            // Cambiar dirección
            yendoHaciaMax = !yendoHaciaMax;
            CalcularPuntosDestino();
        }
    }

    void CalcularPuntosDestino()
    {
        // Calcular el próximo punto en X
        float x = yendoHaciaMax ? xMax : xMin;

        // Calcular Y usando la función lineal y = mx + b
        float y = pendiente * x + terminoIndependiente;

        // Actualizar puntos
        puntoActual = moneda.transform.position;
        puntoDestino = new Vector2(x, y);
    }

    // Método para dibujar la línea en el editor (solo para visualización)
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.green;

        // Calcular puntos extremos de la función
        Vector2 puntoInicio = new Vector2(xMin, pendiente * xMin + terminoIndependiente);
        Vector2 puntoFin = new Vector2(xMax, pendiente * xMax + terminoIndependiente);

        // Dibujar línea que representa la función
        Gizmos.DrawLine(puntoInicio, puntoFin);

        // Dibujar punto de destino actual
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(puntoDestino, 0.2f);
    }
}