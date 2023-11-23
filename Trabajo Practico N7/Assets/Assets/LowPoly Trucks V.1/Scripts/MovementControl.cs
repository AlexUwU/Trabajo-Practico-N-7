using UnityEngine;
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

    public PlayerInterface playerInterface;
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.centerOfMass = centerOfMass.localPosition;
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
            contAmarillo = 0;
            playerInterface.ActualizarCantidadItem("Amarillo", contAmarillo);
            Debug.Log("Amarillo: " + contAmarillo);
        }

        if (other.gameObject.CompareTag("DepositoVerde") && contVerde <= 5)
        {
            contVerde = 0;
            Debug.Log("Verde: " + contVerde);
        }

        if (other.gameObject.CompareTag("DepositoAzul") && contAzul <= 5)
        {
            contAzul = 0;
            Debug.Log("Azul: " + contAzul);
        }

        if (other.gameObject.CompareTag("DepositoRojo") && contRojo <= 5)
        {
            contRojo = 0;
            Debug.Log("Rojo: " + contRojo);
        }
    }
}
