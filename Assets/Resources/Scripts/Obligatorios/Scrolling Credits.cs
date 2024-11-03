using UnityEngine;
using TMPro; // Asegúrate de usar TextMeshPro
using UnityEngine.UI; // Para el uso de Color

public class ScrollingCredits : MonoBehaviour
{
    public float scrollSpeed = 50f; // Velocidad de desplazamiento
    public float delayBeforeFinalText = 2f; // Tiempo en segundos antes de mostrar el texto final
    public float fadeDuration = 2f; // Duración del efecto de desvanecimiento
    public TMP_Text[] creditTexts; // Array de textos para los créditos
    public TMP_Text finalText; // Texto final que se queda en pantalla

    private RectTransform[] creditRects; // RectTransform de los textos para los créditos
    private RectTransform finalRect; // RectTransform del texto final
    private Color originalColor; // Color original del texto final
    private Color targetColor = Color.white; // Color objetivo para el texto final (blanco)
    private bool finalTextShown = false;
    private bool finalTextPositioned = false;
    private bool isFading = false;
    private float finalTextTimer = 0f;
    private float fadeTimer = 0f;

    void Start()
    {
        // Obtener el RectTransform de cada texto de créditos
        creditRects = new RectTransform[creditTexts.Length];
        for (int i = 0; i < creditTexts.Length; i++)
        {
            creditRects[i] = creditTexts[i].GetComponent<RectTransform>();
        }

        // Inicializa el texto final
        finalRect = finalText.GetComponent<RectTransform>();
        originalColor = finalText.color;
        finalText.color = Color.black; // Comienza con el texto en negro
        finalText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!finalTextShown)
        {
            // Desplaza los textos de créditos hacia arriba
            foreach (RectTransform rect in creditRects)
            {
                rect.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

                // Desaparecer cuando el texto está fuera de la pantalla
                if (rect.anchoredPosition.y > Screen.height)
                {
                    rect.gameObject.SetActive(false);
                }
            }

            // Comprueba si todos los textos están fuera de la pantalla
            if (AllCreditsOffScreen())
            {
                finalTextShown = true;
                finalTextTimer = 0f;
            }
        }
        else
        {
            // Mostrar y posicionar el texto final en el centro
            if (!finalTextPositioned)
            {
                finalText.gameObject.SetActive(true);
                finalRect.anchoredPosition = new Vector2(0, 0); // Centro de la pantalla
                finalTextPositioned = true;
                isFading = true;
            }

            // Mueve el texto final hacia arriba y detente en el centro
            finalRect.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            // Detén el movimiento cuando el texto final esté en el centro
            if (finalRect.anchoredPosition.y >= 0)
            {
                finalRect.anchoredPosition = new Vector2(0, 0); // Asegúrate de que se quede en el centro
            }

            // Efecto de desvanecimiento
            if (isFading)
            {
                fadeTimer += Time.deltaTime;
                float lerpValue = Mathf.Clamp01(fadeTimer / fadeDuration);
                finalText.color = Color.Lerp(Color.black, originalColor, lerpValue);

                if (lerpValue >= 1f)
                {
                    isFading = false;
                }
            }
        }
    }

    // Comprueba si todos los textos de créditos están fuera de la pantalla
    private bool AllCreditsOffScreen()
    {
        foreach (RectTransform rect in creditRects)
        {
            if (rect.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}
