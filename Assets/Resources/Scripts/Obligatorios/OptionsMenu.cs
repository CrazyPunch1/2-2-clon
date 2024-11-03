using UnityEngine;
using UnityEngine.UI;
using TMPro; // Si usas TextMeshPro para el label

public class OptionsMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider volumeSlider;
    public TMP_Text fullScreenLabel; // O Text si no usas TextMeshPro
    public Toggle fullScreenToggle; // Para activar/desactivar pantalla completa

    private void Start()
    {
        // Inicializa el slider y el label con los valores actuales
        volumeSlider.value = AudioListener.volume;
        fullScreenToggle.isOn = Screen.fullScreen;

        UpdateFullScreenLabel(fullScreenToggle.isOn);

        // Añade listeners para que detecten los cambios
        volumeSlider.onValueChanged.AddListener(SetVolume);
        fullScreenToggle.onValueChanged.AddListener(SetFullScreen);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        UpdateFullScreenLabel(isFullScreen);
    }

    private void UpdateFullScreenLabel(bool isFullScreen)
    {
        fullScreenLabel.text = isFullScreen ? "Pantalla Completa: Activado" : "Pantalla Completa: Desactivado";
    }
}
