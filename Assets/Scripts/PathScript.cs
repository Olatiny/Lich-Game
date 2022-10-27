using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    [SerializeField] GameObject thisPath;

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
        //Debug.Log("trigger!");

        if (collision.gameObject.name.Equals("Spawn"))
        {
            //Debug.Log("trigger!");

            Transform t = transform;

            t.SetPositionAndRotation(thisPath.transform.GetChild(2).transform.position, thisPath.transform.rotation);

            t.position = new Vector2(transform.position.x, t.position.y - 10);

            Debug.Log(t);

            Instantiate(thisPath, t);
        }
    }

    void FixedUpdate()
    {
        //Debug.Log("updating!");
        transform.position += new Vector3(0, 13 * Time.fixedDeltaTime, 0);

        //if (!GetComponent<Renderer>().isVisible)
        //{
        //    //Debug.Log("here");
        //    //transform.position = otherPath.transform.position - new Vector3(0, 12, 0);
        //    Destroy(thisPath);
        //}

        //if (otherPath.transform.position.y >= -4.7f)
        //{
        //    transform.position = new Vector3(transform.position.x, -4.7f, transform.position.z);
        //}
    }
}
