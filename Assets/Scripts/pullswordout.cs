using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullSwordOut : MonoBehaviour
{

    Rigidbody rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

        //rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            //child.position += Vector3.up * 10.0f;
            //rb.useGravity = true;
            //rb.AddForce(Vector3.up * 200);

        }

    }

}

