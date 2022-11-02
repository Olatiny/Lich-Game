using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathTransitionScript : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject ConnectorLeft;
    [SerializeField] GameObject ConnectorRight;

    private bool whichOne = false;

    void Start()
    {
        if (parent.name.Contains("Right"))
        {
            //Debug.Log("starting with right connector.");
            whichOne = false;
        } else if (parent.name.Contains("Left"))
        {
            //Debug.Log("starting with left connector.");
            whichOne = true;
        }
        //Debug.Log("Started Transitioner connecting at " + (whichOne ? "right" : "left"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            //Debug.Log("Player collided!");
            //Debug.Log("before: " + (whichOne ? "right" : "left"));


            if (parent.name.Contains("Right")) 
            {
                //Debug.Log("was right");
                whichOne = true;
            } else if (parent.name.Contains("Left"))
            {
                //Debug.Log("was left");
                whichOne = false;
            }

            //Debug.Log("after: " + (whichOne ? "right" : "left"));

        }
    }

    public GameObject WhichConnector()
    {
        //Debug.Log(whichOne ? "right" : "left");

        if (whichOne)
        {
            return ConnectorRight;
        } else
        {
            return ConnectorLeft;
        }
    }
}
