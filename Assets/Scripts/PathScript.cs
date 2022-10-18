using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Collided with Wall Spawn!");
        }

        if (collision.gameObject.name == "Wall Spawn")
        {
            Debug.Log("Collided with Wall Spawn!");
        }

        if (collision.gameObject.name == "Wall Despawn")
        {
            Debug.Log("Collided with Wall Despawn!");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0, 0.1f, 0);
    }
}
