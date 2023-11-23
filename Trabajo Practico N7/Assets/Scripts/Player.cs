using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedX = 1.0f;
    public float speedY = 1.0f;


    public int contAmarillo = 0;
    public int contVerde = 0;
    public int contAzul = 0;
    public int contRojo = 0;


    void Start()
    {
        //coment
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;

        dir.x = -Input.acceleration.x;
        dir.y = Input.acceleration.y;
        dir.z = Input.acceleration.z;


        Debug.Log(dir.x.ToString());

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        dir *= Time.deltaTime;


        transform.Translate(new Vector3(dir.x * speedX,0, 0));


        //transform.Translate(-Vector3.forward * speed * Time.deltaTime);


    }


    Vector3 GetAccelerometerValue()
    {
        Vector3 acc = Vector3.zero;
        float period = 0.0f;

        foreach (AccelerationEvent evnt in Input.accelerationEvents)
        {
            acc += evnt.acceleration * evnt.deltaTime;
            period += evnt.deltaTime;
        }
        if (period > 0)
        {
            acc *= 1.0f / period;
        }
        return acc;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Depositos

        if (other.gameObject.CompareTag("DepositoAmarillo") && contAmarillo <= 5)
        {
            contAmarillo = 0;
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
