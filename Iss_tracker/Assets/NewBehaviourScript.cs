using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 0.1f;
    public float sensitivity = 2.0f;

    
    Vector3 valorRotacao = Vector3.zero;
    float mouseX;
    float mouseY;

    private void Awake()
    {
        Application.targetFrameRate = 30;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            move += Vector3.forward * speed;
        if (Input.GetKey(KeyCode.S))
            move -= Vector3.forward * speed;
        if (Input.GetKey(KeyCode.D))
            move += Vector3.right * speed;
        if (Input.GetKey(KeyCode.A))
            move -= Vector3.right * speed;
        if (Input.GetKey(KeyCode.E))
            move += Vector3.up * speed;
        if (Input.GetKey(KeyCode.Q))
            move -= Vector3.up * speed;
        transform.Translate(move);


        Vector3 valorRotacao = Vector3.zero;
        if (Input.GetKey(KeyCode.RightArrow))
            valorRotacao -= Vector3.up * sensitivity;
        if (Input.GetKey(KeyCode.LeftArrow))
            valorRotacao += Vector3.up * sensitivity;
        if (Input.GetKey(KeyCode.UpArrow))
            valorRotacao += Vector3.right * sensitivity;
        if (Input.GetKey(KeyCode.DownArrow))
            valorRotacao -= Vector3.right * sensitivity;

        //mouseY = Input.GetAxis("Mouse X") * sensitivity;
        //mouseX = Input.GetAxis("Mouse Y") * sensitivity;
        //valorRotacao = new Vector3(mouseX, mouseY *-1, 0);
        transform.eulerAngles = transform.eulerAngles - valorRotacao;


    }
}
