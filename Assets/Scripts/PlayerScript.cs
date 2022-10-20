using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D body;
    private int speed = 500;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //bool moved = false;

        if (Input.GetKey(KeyCode.W))
        {
            //body.MovePosition(transform.position + new Vector3(speed, 0, 0));
            body.AddForce(new Vector2(0, speed), ForceMode2D.Force);
            //moved = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            //body.MovePosition(transform.position + new Vector3(-speed, 0, 0));
            body.AddForce(new Vector2(-speed, 0), ForceMode2D.Force);
            //moved = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            //body.MovePosition(transform.position + new Vector3(speed, 0, 0));
            body.AddForce(new Vector2(0, -speed), ForceMode2D.Force);
            //moved = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //body.MovePosition(transform.position + new Vector3(speed, 0, 0));
            body.AddForce(new Vector2(speed, 0), ForceMode2D.Force);
            //moved = true;
        }
    }
}
