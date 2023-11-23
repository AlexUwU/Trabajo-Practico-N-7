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
}
