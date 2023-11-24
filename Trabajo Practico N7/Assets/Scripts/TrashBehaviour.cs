using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehaviour : MonoBehaviour
{

    MovementControl player;
    PlayerInterface playerInterface;
    AudioSource sonidoRecoger;

    private void Start()
    {
        player = FindObjectOfType<MovementControl>();
        playerInterface = FindObjectOfType<PlayerInterface>();
        sonidoRecoger = FindObjectOfType<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Amarillo") && player.contAmarillo < 5)
        {
            player.contAmarillo++;
            playerInterface.ActualizarCantidadItem("Amarillo", player.contAmarillo);
            playerInterface.AumentarPuntaje(10);
            Destroy(this.gameObject);
            Debug.Log("Amarillo: " + player.contAmarillo);
            Debug.Log("Destroy Amarillo");
            sonidoRecoger.Play();
        }

        if (other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Verde") && player.contVerde < 5)
        {
            player.contVerde++;
            playerInterface.ActualizarCantidadItem("Verde", player.contVerde);
            playerInterface.AumentarPuntaje(10);
            Destroy(this.gameObject);
            Debug.Log("Verde: " + player.contVerde);
            Debug.Log("Destroy Verde");
            sonidoRecoger.Play();
        }

        if (other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Azul") && player.contAzul < 5)
        {
            player.contAzul++;
            playerInterface.ActualizarCantidadItem("Azul", player.contAzul);
            playerInterface.AumentarPuntaje(10);
            Destroy(this.gameObject);
            Debug.Log("Azul: " + player.contAzul);
            Debug.Log("Destroy Azul");
            sonidoRecoger.Play();
        }

        if (other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Rojo") && player.contRojo < 5)
        {
            player.contRojo++;
            playerInterface.ActualizarCantidadItem("Rojo", player.contRojo);
            playerInterface.AumentarPuntaje(10);
            Destroy(this.gameObject);
            Debug.Log("Rojo: " + player.contRojo);
            Debug.Log("Destroy Rojo");
            sonidoRecoger.Play();
        }
    }
}
