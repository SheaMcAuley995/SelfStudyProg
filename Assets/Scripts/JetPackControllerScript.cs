using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackControllerScript : MonoBehaviour {

    Rigidbody rb;
    [SerializeField] float movementSpeed;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        jetPackController();
	}


    Vector2 controllerInput()
    {
        float input_Horz = Input.GetAxis("Horizontal");
        float input_Vert = Input.GetAxis("Vertical");

        return new Vector2(input_Horz, input_Vert);
    }

    void jetPackController()
    {
       float h = controllerInput().x;
       float v = controllerInput().y;

        rb.AddForce(transform.right * h * Time.deltaTime * movementSpeed);
        rb.AddForce(transform.forward * v * Time.deltaTime * movementSpeed);
    }

    
}
