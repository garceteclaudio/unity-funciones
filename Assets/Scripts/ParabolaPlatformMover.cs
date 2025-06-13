using UnityEngine;

/// <summary>
/// Mueve una plataforma a lo largo de una trayectoria parab�lica: y = ax� + bx + c
/// </summary>
public class ParabolaPlatformMover : MonoBehaviour
{
    [Header("Plataforma a mover")]
    public Transform platform; // Referencia al objeto que se mover�
    [Header("Par�metros de la par�bola")]
    public float a = -0.1f; // Coeficiente cuadr�tico
    public float b = 0f;    // Coeficiente lineal
    public float c = 2f;    // T�rmino constante

    [Header("Rango de movimiento")]
    public float minX = -10f;  // L�mite izquierdo
    public float maxX = 10f;   // L�mite derecho

    [Header("Velocidad")]
    public float speed = 2f;   // Velocidad de movimiento horizontal

    private float currentX; // Posici�n actual en X
    private int direction = 1; // Direcci�n: 1 (derecha), -1 (izquierda)

    void Start()
    {
        // Inicializa la posici�n de la plataforma
        currentX = minX;
        UpdatePlatformPosition();
    }

    void Update()
    {
        MoveAlongParabola();
    }

    /// <summary>
    /// Mueve la plataforma en una par�bola de izquierda a derecha y viceversa.
    /// </summary>
    void MoveAlongParabola()
    {
        // Avanza en X seg�n la direcci�n y velocidad
        currentX += direction * speed * Time.deltaTime;

        // Cambia de direcci�n al llegar a los bordes
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
    /// Calcula la posici�n Y de la par�bola y actualiza la posici�n de la plataforma.
    /// </summary>
    void UpdatePlatformPosition()
    {
        float y = a * currentX * currentX + b * currentX + c;
        platform.position = new Vector3(currentX, y, 0);
    }

    /// <summary>
    /// Calcula y muestra el v�rtice de la par�bola.
    /// </summary>
    void OnDrawGizmos()
    {
        // V�rtice de la par�bola: x = -b / (2a)
        float vertexX = -b / (2f * a);
        float vertexY = a * vertexX * vertexX + b * vertexX + c;

        // Dibuja una esfera peque�a en el v�rtice
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(vertexX, vertexY, 0), 0.2f);
    }
}
