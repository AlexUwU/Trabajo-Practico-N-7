using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private int win;
    private bool cantPizza;
    private GameObject point;
    [SerializeField]
    private GameObject Mochila;
    [SerializeField]
    private float timer;
    [SerializeField]
    private string loseText;
    private bool isLosing;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float aceleration;
    [SerializeField]
    private float deceleration;
    [SerializeField]
    private float speedJump;
    private Vector3 direction;
    private Rigidbody rigidbodyPlayer;
    public bool isGrounded;
    [SerializeField]
    private float jumpTime;
    private float jumpCounter;
    private bool isJumping;
    [SerializeField]
    private float speedDown;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private string winText;
    private bool isWinning;
    private int score;
    [SerializeField]
    private Transform cameraPlayer;
    [SerializeField]
    private Vector3 cameraPlayerPosition;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody>();
        jumpCounter = jumpTime;
        cameraPlayerPosition = cameraPlayer.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        InvertCamera();

        transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0);

        Jump();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            isLosing = true;
            EndGame();
        }
        if (win==5)
        {
            isWinning=true;
            EndGame();
        }
    }
    private void FixedUpdate()
    {
        Move();
        if (!isGrounded)
        {
            rigidbodyPlayer.AddForce(Vector3.down * speedDown, ForceMode.Impulse);
        }
    }
    public void Move()
    {
        if (Input.GetAxisRaw("Vertical") < 0 && speed < maxSpeed)
        {
            speed = speed - aceleration * Time.deltaTime;
        }
        else if (Input.GetAxisRaw("Vertical") > 0 && speed > -maxSpeed)
        {
            speed = speed + aceleration * Time.deltaTime;
        }
        else
        {
            if (speed > deceleration * Time.deltaTime)
            {
                speed = speed - deceleration * Time.deltaTime;
            }
            else if (speed < -deceleration * Time.deltaTime)
            {
                speed = speed + deceleration * Time.deltaTime;
            }
            else
            {
                speed = 0;
            }
        }

        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        else if (speed < -maxSpeed)
        {
            speed = -maxSpeed;
        }

        direction = transform.forward * speed;

        rigidbodyPlayer.velocity = direction;
    }
    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidbodyPlayer.AddForce(speedJump * Vector3.up * Time.deltaTime, ForceMode.Impulse);
            isGrounded = false;
            isJumping = true;
            jumpCounter=jumpTime;
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpCounter > 0)
            {
                {
                    rigidbodyPlayer.AddForce(Vector3.up * speedJump,ForceMode.Impulse);
                    jumpCounter -= Time.deltaTime;
                }
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }
    private void InvertCamera()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            cameraPlayer.localPosition = new Vector3(cameraPlayerPosition.x * -1, cameraPlayerPosition.y, cameraPlayerPosition.z * -1);
            cameraPlayer.Rotate(new Vector3(0, 180, 0));
        }
        else if (Input.GetKeyUp(KeyCode.V))
        {
            cameraPlayer.localPosition = new Vector3(cameraPlayerPosition.x, cameraPlayerPosition.y, cameraPlayerPosition.z);
            cameraPlayer.Rotate(new Vector3(0, 180, 0));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle1"))
        {
            speed = speed + deceleration * Time.deltaTime * -maxSpeed;
        }

        if (other.gameObject.CompareTag("Obstacle2"))
        {
            speed *= 0.9f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Money"))
        {
            score += 10;
        }
        if (other.gameObject.CompareTag("Goal"))
        {
            isWinning = true;
            EndGame();
        }
        if (other.gameObject.CompareTag("Pizza") && cantPizza==false)
        {
            other.gameObject.transform.SetParent(Mochila.transform,false);
            other.gameObject.transform.localPosition = Vector3.zero;
            point=other.gameObject;
            cantPizza=true;

        }
        if (other.gameObject.CompareTag("People") && cantPizza==true)
        {
            point.transform.SetParent(other.GetComponentInChildren<Transform>().Find("point"), false);
            other.gameObject.GetComponent<Collider>().enabled=false;
            point.GetComponent<Collider>().enabled = false;
            win++;
            score += 10;
            cantPizza=false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }
    void OnGUI()
    {
        GUIStyle MoneyStyle = new GUIStyle();
        MoneyStyle.fontSize=35;
        GUI.Label(new Rect(0, 50, 100, 100), "Puntaje: " + score.ToString(),MoneyStyle);
        GUI.Label(new Rect(0, 0, 100, 100), "Pizzas Entregadas: " + win.ToString()+"/5",MoneyStyle);
        if (isWinning)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 100;
            style.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2 - 50, 100, 100), winText, style);
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 3 + Screen.height / 3 - 50, 100, 100), "Puntaje: "+ score.ToString(), style);
        }
        GUI.Label(new Rect(Screen.width - 100,0, 100, 100), "Tiempo restante: " + Mathf.Round(timer*100)/100);
        if (isLosing)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 75;
            style.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2 - 50, 100, 100), loseText, style);
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 3 + Screen.height / 3 - 50, 100, 100), "Puntaje: "+ score.ToString(), style);
        }
    }
    void EndGame()
    {
        Time.timeScale = 0;
    }
}
