using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehaviour : MonoBehaviour
{

    public MovementControl player;
    public PlayerInterface playerInterface;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Amarillo") && player.contAmarillo < 5)
        {
            player.contAmarillo++;
            playerInterface.ActualizarCantidadItem("Amarillo", player.contAmarillo);
            Destroy(this.gameObject);
            Debug.Log("Amarillo: " + player.contAmarillo);
            Debug.Log("Destroy Amarillo");
        }

        if (other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Verde") && player.contVerde < 5)
        {
            player.contVerde++;
            Destroy(this.gameObject);
            Debug.Log("Verde: " + player.contVerde);
            Debug.Log("Destroy Verde");
        }

        if (other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Azul") && player.contAzul < 5)
        {
            player.contAzul++;
            Destroy(this.gameObject);
            Debug.Log("Azul: " + player.contAzul);
            Debug.Log("Destroy Azul");
        }

        if (other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Rojo") && player.contRojo < 5)
        {
            player.contRojo++;
            Destroy(this.gameObject);
            Debug.Log("Rojo: " + player.contRojo);
            Debug.Log("Destroy Rojo");
        }
    }
}
