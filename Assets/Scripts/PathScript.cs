using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    [SerializeField] GameObject otherPath;

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    Debug.Log("here");
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    Debug.Log("here");
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger!");
    }

    void FixedUpdate()
    {
        //Debug.Log("updating!");
        transform.position += new Vector3(0, 13 * Time.fixedDeltaTime, 0);

        //if (!GetComponent<Renderer>().isVisible)
        //{
        //    Debug.Log("here");
        //    transform.position = otherPath.transform.position - new Vector3(0, 12, 0);
        //}
        
        //if (otherPath.transform.position.y >= -4.7f)
        //{
        //    transform.position = new Vector3(transform.position.x, -4.7f, transform.position.z);
        //}
    }
}
