using UnityEngine; // Importa el espacio de nombres necesario para usar funciones y clases de Unity

// Define una clase llamada EjesCartesianos que hereda de MonoBehaviour (base para scripts en Unity)
public class EjesCartesianos : MonoBehaviour
{
    // Límites visibles del eje X (mínimo y máximo)
    public float xMin = -6f;
    public float xMax = 6f;

    // Límites visibles del eje Y (mínimo y máximo)
    public float yMin = -4f;
    public float yMax = 4f;

    // Grosor de las líneas que representan los ejes
    public float lineWidth = 0.02f;

    // Material que se usará para renderizar las líneas (debe asignarse en el Inspector)
    public Material ejeMaterial;

    // Método que se ejecuta automáticamente al iniciar la escena
    void Start()
    {
        // Crea el eje X desde una posición muy a la izquierda hasta una muy a la derecha (línea horizontal)
        CrearEje(Vector3.left * 1000, Vector3.right * 1000, "EjeX");

        // Crea el eje Y desde muy abajo hasta muy arriba (línea vertical)
        CrearEje(Vector3.down * 1000, Vector3.up * 1000, "EjeY");
    }

    // Método para crear una línea entre dos puntos usando LineRenderer
    void CrearEje(Vector3 inicio, Vector3 fin, string nombre)
    {
        // Crea un nuevo GameObject en la escena con el nombre proporcionado
        GameObject eje = new GameObject(nombre);

        // Agrega un componente LineRenderer al GameObject para dibujar la línea
        var lr = eje.AddComponent<LineRenderer>();

        // Define que el LineRenderer tendrá dos puntos (inicio y fin)
        lr.positionCount = 2;

        // Asigna la posición de inicio de la línea
        lr.SetPosition(0, inicio);

        // Asigna la posición final de la línea
        lr.SetPosition(1, fin);

        // Establece el grosor inicial de la línea
        lr.startWidth = lineWidth;

        // Establece el grosor final de la línea (igual al inicial para mantener consistencia)
        lr.endWidth = lineWidth;

        // Asigna el material configurado desde el Inspector
        lr.material = ejeMaterial;

        // Indica que las posiciones están en coordenadas del mundo (no locales)
        lr.useWorldSpace = true;

        // Define el orden de renderizado para que la línea se dibuje encima de otros objetos (opcional)
        lr.sortingOrder = 10;
    }
}
