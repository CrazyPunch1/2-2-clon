using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeToScene : MonoBehaviour
{
    // Nombre de la escena a la que se va a cargar
    public string targetScene;

    void Update()
    {
        // Detecta si se presiona la tecla "Esc"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Carga la escena especificada
            SceneManager.LoadScene(targetScene);
        }
    }
}
