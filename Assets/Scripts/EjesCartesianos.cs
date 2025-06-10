using UnityEngine; // Importa el espacio de nombres necesario para usar funciones y clases de Unity

// Define una clase llamada EjesCartesianos que hereda de MonoBehaviour (base para scripts en Unity)
public class EjesCartesianos : MonoBehaviour
{
    // L�mites visibles del eje X (m�nimo y m�ximo)
    public float xMin = -6f;
    public float xMax = 6f;

    // L�mites visibles del eje Y (m�nimo y m�ximo)
    public float yMin = -4f;
    public float yMax = 4f;

    // Grosor de las l�neas que representan los ejes
    public float lineWidth = 0.02f;

    // Material que se usar� para renderizar las l�neas (debe asignarse en el Inspector)
    public Material ejeMaterial;

    // M�todo que se ejecuta autom�ticamente al iniciar la escena
    void Start()
    {
        // Crea el eje X desde una posici�n muy a la izquierda hasta una muy a la derecha (l�nea horizontal)
        CrearEje(Vector3.left * 1000, Vector3.right * 1000, "EjeX");

        // Crea el eje Y desde muy abajo hasta muy arriba (l�nea vertical)
        CrearEje(Vector3.down * 1000, Vector3.up * 1000, "EjeY");
    }

    // M�todo para crear una l�nea entre dos puntos usando LineRenderer
    void CrearEje(Vector3 inicio, Vector3 fin, string nombre)
    {
        // Crea un nuevo GameObject en la escena con el nombre proporcionado
        GameObject eje = new GameObject(nombre);

        // Agrega un componente LineRenderer al GameObject para dibujar la l�nea
        var lr = eje.AddComponent<LineRenderer>();

        // Define que el LineRenderer tendr� dos puntos (inicio y fin)
        lr.positionCount = 2;

        // Asigna la posici�n de inicio de la l�nea
        lr.SetPosition(0, inicio);

        // Asigna la posici�n final de la l�nea
        lr.SetPosition(1, fin);

        // Establece el grosor inicial de la l�nea
        lr.startWidth = lineWidth;

        // Establece el grosor final de la l�nea (igual al inicial para mantener consistencia)
        lr.endWidth = lineWidth;

        // Asigna el material configurado desde el Inspector
        lr.material = ejeMaterial;

        // Indica que las posiciones est�n en coordenadas del mundo (no locales)
        lr.useWorldSpace = true;

        // Define el orden de renderizado para que la l�nea se dibuje encima de otros objetos (opcional)
        lr.sortingOrder = 10;
    }
}
