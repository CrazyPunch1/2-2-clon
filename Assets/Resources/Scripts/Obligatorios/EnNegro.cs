using UnityEngine;
using UnityEngine.UI; // Para usar la clase Image
using System.Collections;

public class EnNegro : MonoBehaviour
{
    public Image blackImage; // Imagen negra que cubre la pantalla
    public float fadeDuration = 2f; // Duración del efecto de desvanecimiento

    private void Start()
    {
        // Asegúrate de que la imagen negra está activa al inicio
        if (blackImage != null)
        {
            blackImage.gameObject.SetActive(true);
            StartCoroutine(FadeOutBlackScreen());
        }
    }

    private IEnumerator FadeOutBlackScreen()
    {
        Color startColor = blackImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f); // Hacer el color transparente

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            blackImage.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Espera al siguiente frame
        }

        blackImage.color = endColor; // Asegúrate de que el color final es transparente
        blackImage.gameObject.SetActive(false); // Desactiva la imagen cuando termine el fade
    }
}
