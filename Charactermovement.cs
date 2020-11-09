using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactermovement : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody _rigidbody;
    public float speed , sensitivity,xsensitivity,jumpforce;
    Vector2 xmove , ymove , velocity;
    public Camera cam;
    float yRot , xRot,mouseY;
    public bool IsGrounded;
    public float groundist;
    float initailspeed;
    public float sprintspeed;

    [SerializeField]
    float min , max;
     
    void Start()
    {
        _rigidbody= GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        initailspeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
       
        CameraLook();
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
          Sprint();
        }
          if(Input.GetKeyUp(KeyCode.LeftShift))
        {
           speed = initailspeed;
        }
        
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
        Jump();
    }

    void Move()
    {
         
        xmove = new Vector2( Input.GetAxisRaw("Horizontal") * transform.right.x,Input.GetAxis("Horizontal")* transform.right.z);
        ymove = new Vector2( Input.GetAxisRaw("Vertical") * transform.forward.x,Input.GetAxis("Vertical")* transform.forward.z); 
        velocity = (xmove+ymove).normalized*speed*Time.deltaTime;
        _rigidbody.velocity = new Vector3(velocity.x,_rigidbody.velocity.y,velocity.y);
    }

    void Rotate()
    {
        yRot = Input.GetAxis("Mouse X")*sensitivity;
       _rigidbody.rotation *= Quaternion.Euler(0,yRot*Time.deltaTime,0);
    }

    void CameraLook()
    {
       
    xRot += -Input.GetAxis("Mouse Y") * xsensitivity;
    xRot = Mathf.Clamp(xRot ,-min, max);
    cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
    }

    void Jump()
    {
        IsGrounded = Physics.Raycast(transform.position,Vector3.down,groundist);
        if(Input.GetKeyDown(KeyCode.Space)&&IsGrounded)
        {
         _rigidbody.AddForce(new Vector3(0,jumpforce,0));
        }
    }
    void Sprint()
    {
           speed = sprintspeed;
    }
}
