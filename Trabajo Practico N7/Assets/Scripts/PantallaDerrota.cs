using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaDerrota : MonoBehaviour
{
    public TextMeshProUGUI puntajeTexto;
    void Start()
    {
        float puntaje = PlayerPrefs.GetFloat("Puntaje", 0f);

        puntajeTexto.text = "Puntaje: " + puntaje.ToString();
    }

    public void CargarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }

    public void SalirAplicacion()
    {
        Debug.Log("Se Cerro la Application");
        Application.Quit();
    }

}
