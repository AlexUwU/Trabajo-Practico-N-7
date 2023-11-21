using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedX = 1.0f;
    public float speedY = 1.0f;
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
}
