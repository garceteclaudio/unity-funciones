using UnityEngine;

/// <summary>
/// Mueve una plataforma a lo largo de una trayectoria parabólica: y = ax² + bx + c
/// </summary>
public class ParabolaPlatformMover : MonoBehaviour
{
    [Header("Plataforma a mover")]
    public Transform platform; // Referencia al objeto que se moverá
    [Header("Parámetros de la parábola")]
    public float a = -0.1f; // Coeficiente cuadrático
    public float b = 0f;    // Coeficiente lineal
    public float c = 2f;    // Término constante

    [Header("Rango de movimiento")]
    public float minX = -10f;  // Límite izquierdo
    public float maxX = 10f;   // Límite derecho

    [Header("Velocidad")]
    public float speed = 2f;   // Velocidad de movimiento horizontal

    private float currentX; // Posición actual en X
    private int direction = 1; // Dirección: 1 (derecha), -1 (izquierda)

    void Start()
    {
        // Inicializa la posición de la plataforma
        currentX = minX;
        UpdatePlatformPosition();
    }

    void Update()
    {
        MoveAlongParabola();
    }

    /// <summary>
    /// Mueve la plataforma en una parábola de izquierda a derecha y viceversa.
    /// </summary>
    void MoveAlongParabola()
    {
        // Avanza en X según la dirección y velocidad
        currentX += direction * speed * Time.deltaTime;

        // Cambia de dirección al llegar a los bordes
        if (currentX > maxX)
        {
            currentX = maxX;
            direction = -1;
        }
        else if (currentX < minX)
        {
            currentX = minX;
            direction = 1;
        }

        UpdatePlatformPosition();
    }

    /// <summary>
    /// Calcula la posición Y de la parábola y actualiza la posición de la plataforma.
    /// </summary>
    void UpdatePlatformPosition()
    {
        float y = a * currentX * currentX + b * currentX + c;
        platform.position = new Vector3(currentX, y, 0);
    }

    /// <summary>
    /// Calcula y muestra el vértice de la parábola.
    /// </summary>
    void OnDrawGizmos()
    {
        // Vértice de la parábola: x = -b / (2a)
        float vertexX = -b / (2f * a);
        float vertexY = a * vertexX * vertexX + b * vertexX + c;

        // Dibuja una esfera pequeña en el vértice
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(vertexX, vertexY, 0), 0.2f);
    }
}
