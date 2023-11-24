﻿using UnityEngine;
using System.Collections;

public class MovementControl : MonoBehaviour {
    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public Transform[] tyreMeshes = new Transform[4];
    public float maxTorque = 50.0f;
    private Rigidbody m_rigidbody;
    public Transform centerOfMass;
    public float acclSensitivity = 5.0f;
    public float speed = 10.0f;


    public int contAmarillo = 0;
    public int contVerde = 0;
    public int contAzul = 0;
    public int contRojo = 0;

    PlayerInterface playerInterface;
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.centerOfMass = centerOfMass.localPosition;

        playerInterface = FindObjectOfType<PlayerInterface>();
    }

    void Update()
    {
        UpdateMeshesPositions();
    }

    void FixedUpdate()
    {
        
        float steer = Input.acceleration.x;
        float fixedAngle = steer * 45f;
        wheelColliders[0].steerAngle = fixedAngle;
        wheelColliders[1].steerAngle = fixedAngle;

        
        float acceleration = -Input.acceleration.y * speed;
        for (int i = 0; i < 4; i++)
        {
            wheelColliders[i].motorTorque = acceleration * maxTorque;
        }
    }

    void UpdateMeshesPositions()
    {
        for (int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 pos;
            wheelColliders[i].GetWorldPose(out pos, out quat);
            tyreMeshes[i].position = pos;
            tyreMeshes[i].rotation = quat;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Depositos

        if (other.gameObject.CompareTag("DepositoAmarillo") && contAmarillo <= 5)
        {
            if (contAmarillo != 0)
            {
                playerInterface.AumentarPuntaje(contAmarillo * 10);
                playerInterface.tiempo = playerInterface.tiempo + 10 * contAmarillo;
            }
            contAmarillo = 0;
            playerInterface.ActualizarCantidadItem("Amarillo", contAmarillo);
            Debug.Log("Amarillo: " + contAmarillo);
        }

        if (other.gameObject.CompareTag("DepositoVerde") && contVerde <= 5)
        {
            if (contVerde != 0)
            {
                playerInterface.AumentarPuntaje(contVerde * 10);
                playerInterface.tiempo = playerInterface.tiempo + 10 * contVerde;
            }
            contVerde = 0;
            playerInterface.ActualizarCantidadItem("Verde", contVerde);
            Debug.Log("Verde: " + contVerde);
        }

        if (other.gameObject.CompareTag("DepositoAzul") && contAzul <= 5)
        {
            if (contAzul != 0)
            {
                playerInterface.AumentarPuntaje(contAzul * 10);
                playerInterface.tiempo = playerInterface.tiempo + 10 * contAzul;
            }
            contAzul = 0;
            playerInterface.ActualizarCantidadItem("Azul", contAzul);
            Debug.Log("Azul: " + contAzul);
        }

        if (other.gameObject.CompareTag("DepositoRojo") && contRojo <= 5)
        {
            if (contRojo != 0)
            {
                playerInterface.AumentarPuntaje(contRojo * 10);
                playerInterface.tiempo = playerInterface.tiempo + 10 * contRojo;
            }
            contRojo = 0;
            playerInterface.ActualizarCantidadItem("Rojo", contRojo);
            contRojo = 0;
            Debug.Log("Rojo: " + contRojo);
        }
    }
}
