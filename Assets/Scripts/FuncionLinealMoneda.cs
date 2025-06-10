using UnityEngine;

public class FuncionLinealMoneda : MonoBehaviour
{
    [Header("Par�metros de la funci�n lineal")]
    [Tooltip("Pendiente de la funci�n (m en y = mx + b)")]
    public float pendiente = 0.5f;
    [Tooltip("T�rmino independiente (b en y = mx + b)")]
    public float terminoIndependiente = 1f;

    [Header("Rango de movimiento")]
    [Tooltip("Valor m�nimo de X")]
    public float xMin = -5f;
    [Tooltip("Valor m�ximo de X")]
    public float xMax = 5f;

    [Header("Configuraci�n de movimiento")]
    [Tooltip("Velocidad de movimiento")]
    public float velocidad = 2f;
    [Tooltip("Precisi�n para considerar que lleg� al punto")]
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
            Debug.LogError("No se encontr� ning�n objeto con tag 'monedaFuncionLineal'");
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

        // Verificar si lleg� al punto destino
        if (Vector2.Distance(moneda.transform.position, puntoDestino) < precisionLlegada)
        {
            // Cambiar direcci�n
            yendoHaciaMax = !yendoHaciaMax;
            CalcularPuntosDestino();
        }
    }

    void CalcularPuntosDestino()
    {
        // Calcular el pr�ximo punto en X
        float x = yendoHaciaMax ? xMax : xMin;

        // Calcular Y usando la funci�n lineal y = mx + b
        float y = pendiente * x + terminoIndependiente;

        // Actualizar puntos
        puntoActual = moneda.transform.position;
        puntoDestino = new Vector2(x, y);
    }

    // M�todo para dibujar la l�nea en el editor (solo para visualizaci�n)
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.green;

        // Calcular puntos extremos de la funci�n
        Vector2 puntoInicio = new Vector2(xMin, pendiente * xMin + terminoIndependiente);
        Vector2 puntoFin = new Vector2(xMax, pendiente * xMax + terminoIndependiente);

        // Dibujar l�nea que representa la funci�n
        Gizmos.DrawLine(puntoInicio, puntoFin);

        // Dibujar punto de destino actual
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(puntoDestino, 0.2f);
    }
}