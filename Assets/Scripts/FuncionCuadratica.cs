using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(LineRenderer))]
public class FuncionCuadratica : MonoBehaviour
{
    [Header("Coeficientes de la funci�n")]
    public float a = 1f;
    public float b = 0f;
    public float c = 0f;

    [Header("Rango de X")]
    public float xMin = -10f;
    public float xMax = 10f;
    public int resolution = 100;

    [Header("Animaci�n")]
    public float drawSpeed = 0.05f;
    public bool animateOnReset = true;

    [Header("Visualizaci�n de Coordenadas")]
    [Tooltip("Actualizar coordenadas durante la animaci�n")]
    public bool actualizarDuranteAnimacion = true;
    [Tooltip("Formato de visualizaci�n de coordenadas")]
    public string formatoCoordenadas = "X: {0:F2}\nY: {1:F2}";

    private LineRenderer lineRenderer;
    private Coroutine currentAnimation;
    private Button resetButton;
    private TMP_Text textoCoordenadas;
    private Vector2 ultimasCoordenadas;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Buscar el texto de coordenadas por tag
        GameObject coordenadasObj = GameObject.FindWithTag("valorCoordenadas");
        if (coordenadasObj != null)
        {
            textoCoordenadas = coordenadasObj.GetComponent<TMP_Text>();
            if (textoCoordenadas == null)
            {
                Debug.LogError("El objeto con tag 'valorCoordenada' no tiene componente TMP_Text");
            }
        }
        else
        {
            Debug.LogWarning("No se encontr� ning�n objeto con tag 'valorCoordenada'");
        }

        // Buscar el bot�n por tag
        GameObject buttonObj = GameObject.FindWithTag("botonReiniciar");
        if (buttonObj != null)
        {
            resetButton = buttonObj.GetComponent<Button>();
            if (resetButton != null)
            {
                resetButton.onClick.AddListener(ReiniciarFuncion);
            }
            else
            {
                Debug.LogError("No se encontr� el componente Button en el objeto con tag 'botonReiniciar'");
            }
        }

        InicializarTextoCoordenadas();

        if (animateOnReset)
        {
            currentAnimation = StartCoroutine(AnimateQuadraticFunction());
        }
        else
        {
            DibujarFuncionCuadratica();
            ActualizarTextoCoordenadas(xMax, CalcularY(xMax));
        }
    }

    void InicializarTextoCoordenadas()
    {
        if (textoCoordenadas != null)
        {
            textoCoordenadas.text = string.Format(formatoCoordenadas, 0f, 0f);
        }
    }

    float CalcularY(float x)
    {
        return a * x * x + b * x + c;
    }

    void ActualizarTextoCoordenadas(float x, float y)
    {
        ultimasCoordenadas = new Vector2(x, y);

        if (textoCoordenadas != null)
        {
            textoCoordenadas.text = string.Format(formatoCoordenadas, x, y);
        }
    }

    public void ReiniciarFuncion()
    {
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }

        if (animateOnReset)
        {
            currentAnimation = StartCoroutine(AnimateQuadraticFunction());
        }
        else
        {
            DibujarFuncionCuadratica();
            ActualizarTextoCoordenadas(xMax, CalcularY(xMax));
        }
    }

    IEnumerator AnimateQuadraticFunction()
    {
        lineRenderer.positionCount = 0;
        float step = (xMax - xMin) / resolution;

        for (int i = 0; i <= resolution; i++)
        {
            float x = xMin + step * i;
            float y = CalcularY(x);

            lineRenderer.positionCount = i + 1;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));

            if (actualizarDuranteAnimacion)
            {
                ActualizarTextoCoordenadas(x, y);
            }

            yield return new WaitForSeconds(drawSpeed);
        }

        if (!actualizarDuranteAnimacion)
        {
            ActualizarTextoCoordenadas(xMax, CalcularY(xMax));
        }
    }

    void DibujarFuncionCuadratica()
    {
        lineRenderer.positionCount = resolution + 1;
        float step = (xMax - xMin) / resolution;

        for (int i = 0; i <= resolution; i++)
        {
            float x = xMin + step * i;
            float y = CalcularY(x);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }

    void OnMouseOver()
    {
        if (lineRenderer == null || lineRenderer.positionCount == 0) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // Encontrar el punto m�s cercano en la l�nea
        int indiceMasCercano = 0;
        float distanciaMasCercana = Vector3.Distance(mousePos, lineRenderer.GetPosition(0));

        for (int i = 1; i < lineRenderer.positionCount; i++)
        {
            float distancia = Vector3.Distance(mousePos, lineRenderer.GetPosition(i));
            if (distancia < distanciaMasCercana)
            {
                distanciaMasCercana = distancia;
                indiceMasCercano = i;
            }
        }

        Vector3 punto = lineRenderer.GetPosition(indiceMasCercano);
        ActualizarTextoCoordenadas(punto.x, punto.y);
    }

    void OnDestroy()
    {
        if (resetButton != null)
        {
            resetButton.onClick.RemoveListener(ReiniciarFuncion);
        }
    }
}