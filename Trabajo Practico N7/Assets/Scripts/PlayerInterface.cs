using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerInterface : MonoBehaviour
{

    public TextMeshProUGUI tiempoTexto;
    public TextMeshProUGUI puntajeTexto;
    public GameObject menuPausa;

    public List<Item> inventario = new List<Item>();
    public Transform ranurasParent;
    public GameObject ranuraPrefab;
    public float espacioEntreRanuras;

    public Sprite iconoItem1;
    public Sprite iconoItem2;
    public Sprite iconoItem3;
    public Sprite iconoItem4;

    private float tiempo = 0f;
    private float puntaje = 0f;
    private bool juegoPausado = false;

    public void Start()
    {
        inventario.Add(new Item("Item1", iconoItem1));
        inventario.Add(new Item("Item2", iconoItem2));
        inventario.Add(new Item("Item3", iconoItem3));
        inventario.Add(new Item("Item4", iconoItem4));
        ActualizarInventario();
    }
    public void Update()
    {
        if (!juegoPausado)
        {
            tiempo += Time.deltaTime;
            tiempoTexto.text = "Tiempo: " + Mathf.RoundToInt(tiempo);

            puntajeTexto.text = "Puntaje: " + puntaje;
        }
    }

    public void AumentarPuntaje(int puntos)
    {
        puntaje += puntos;
    }

    public void PausarJuego()
    {
        juegoPausado = true;
        Time.timeScale = 0f;

        menuPausa.SetActive(true);
    }

    public void ReanudarJuego()
    {
        juegoPausado = false;
        Time.timeScale = 1f;

        menuPausa.SetActive(false);
    }

    public void CargarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }

    public void ActualizarInventario()
    {
        foreach (Transform ranura in ranurasParent)
        {
            Destroy(ranura.gameObject);
        }

        for (int i = 0; i < inventario.Count; i++)
        {
            float posX = i * (ranuraPrefab.GetComponent<RectTransform>().rect.width + espacioEntreRanuras);

            GameObject ranura = Instantiate(ranuraPrefab, ranurasParent);

            Image iconoRanura = ranura.GetComponentInChildren<Image>();
            TextMeshProUGUI textoRanura = ranura.GetComponentInChildren<TextMeshProUGUI>();

            RectTransform ranuraRectTransform = ranura.GetComponent<RectTransform>();
            ranuraRectTransform.anchoredPosition = new Vector2(posX, 0f);

            iconoRanura.sprite = inventario[i].icono;
            textoRanura.text = inventario[i].cantidad.ToString();

        }
    }
}
