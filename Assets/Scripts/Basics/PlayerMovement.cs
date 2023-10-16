using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    private float xInput;
    public float moveVelocity;
    public float speed;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        
    }
 
    // Update is called once per frame
    void Update()
    {
        GetInputs();
        Vector3 Desplazamiento = Vector3.forward * speed;
        Vector3 Parar = Vector3.zero;
        
        if (GameObject.FindGameObjectWithTag("PointManager").GetComponent<PointManager>().StopRunning == false)
        {
            transform.Translate(Desplazamiento);
        }

        if (GameObject.FindGameObjectWithTag("PointManager").GetComponent<PointManager>().StopRunning == true)
        {
            Debug.Log("Parate");
            
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void GetInputs()
    {
        xInput = Input.GetAxis("Horizontal");
        
    }

    void Move()
    {
        rb.AddForce(new Vector3(0f, 0f, xInput) * moveVelocity);
    }
}
