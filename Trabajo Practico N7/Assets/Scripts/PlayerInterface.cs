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
    public GameObject menuTutorial;

    public List<Item> inventario = new List<Item>();
    public Transform ranurasParent;
    public GameObject ranuraPrefab;
    public float espacioEntreRanuras;

    public Sprite iconoItem1;
    public Sprite iconoItem2;
    public Sprite iconoItem3;
    public Sprite iconoItem4;

    public float tiempo;
    private float puntaje = 0f;
    private bool juegoPausado = true;


    MovementControl player;

    public AudioSource fxSource;
    public AudioClip sonidoBoton;
    public AudioSource musica;
    public AudioSource motor;


    public void Start()
    {
        inventario.Add(new Item("Amarillo", iconoItem1));
        inventario.Add(new Item("Azul", iconoItem2));
        inventario.Add(new Item("Verde", iconoItem3));
        inventario.Add(new Item("Rojo", iconoItem4));
        ActualizarInventario();

        player = FindObjectOfType<MovementControl>();

        menuTutorial.SetActive(true);
        Time.timeScale = 0f;

    }
    public void Update()
    {
        if (!juegoPausado)
        {
            tiempo -= Time.deltaTime;
            tiempoTexto.text = "" + Mathf.RoundToInt(tiempo);

            puntajeTexto.text = "" + puntaje;

            if (tiempo <= 0)
            {
                MostrarPantallaDerrota(puntaje);
            }
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

        PlaySoundButton();
        musica.Pause();
        motor.Pause();
    }

    public void ReanudarJuego()
    {
        juegoPausado = false;
        Time.timeScale = 1f;

        menuPausa.SetActive(false);

        PlaySoundButton();
        musica.Play();
        motor.Play();
    }

    public void EmpezarJuego()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        menuTutorial.SetActive(false);
        PlaySoundButton();
        motor.Play();
    }

    public void CargarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }

    public void PlaySoundButton()
    {
        fxSource.PlayOneShot(sonidoBoton);
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

    public void ActualizarCantidadItem(string nombre, int cantidad)
    {
        Item item = inventario.Find(item => item.nombre == nombre);

        if (item != null)
        {
            item.cantidad = cantidad;
            ActualizarInventario();
        }
    }

    public void MostrarPantallaDerrota(float puntaje)
    {
        PlayerPrefs.SetFloat("Puntaje",puntaje);

        SceneManager.LoadScene("Derrota");
    }
}
