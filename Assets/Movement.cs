using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float force = 100f;
    [SerializeField] float rotation = 100f;
    Rigidbody playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
    }

    void Inputs()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerRigidbody.AddRelativeForce(Vector3.up * force * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Rotate(1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rotate(-1);
        }
    }


    void Rotate(int direction)
    {
        playerRigidbody.freezeRotation = true;
        transform.Rotate(direction * Vector3.forward * rotation * Time.deltaTime);
        playerRigidbody.freezeRotation = false;
    }

}
