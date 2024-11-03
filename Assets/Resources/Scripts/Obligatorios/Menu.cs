using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Jugar()
    {
        // Corrige el nombre del método: SceneManager.LoadScene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
    public void CargarCreditos()
    {
        // Carga la escena llamada "ZZCreditos"
        SceneManager.LoadScene("Credits");
    }
}
