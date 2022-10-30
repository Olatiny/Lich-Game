using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] Camera cam;

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
        if (GameManager.Instance.fallSpeed == 0)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W) && transform.position.y <= 1.5)
        {
            //body.MovePosition(transform.position + new Vector3(speed, 0, 0));
            body.AddForce(new Vector2(0, speed), ForceMode2D.Force);
            //moved = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            //body.MovePosition(transform.position + new Vector3(-speed, 0, 0));
            body.AddForce(new Vector2(-speed, 0), ForceMode2D.Force);

            if (transform.position.x <= cam.transform.position.x - 3.62)
            {
                cam.transform.position = new Vector2(cam.transform.position.x - speed, cam.transform.position.y);
            }
            //moved = true;
        }

        if (Input.GetKey(KeyCode.S) && transform.position.y >= -7.9)
        {
            //body.MovePosition(transform.position + new Vector3(speed, 0, 0));
            body.AddForce(new Vector2(0, -speed), ForceMode2D.Force);
            //moved = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //body.MovePosition(transform.position + new Vector3(speed, 0, 0));
            body.AddForce(new Vector2(speed, 0), ForceMode2D.Force);

            if (transform.position.x <= cam.transform.position.x - 3.62)
            {
                cam.transform.position = new Vector2(cam.transform.position.x + speed, cam.transform.position.y);
            }

            //moved = true;
        }
    }
}
