using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTransitionScript : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject ConnectorLeft;
    [SerializeField] GameObject ConnectorRight;

    private bool whichOne = false;

    void Start()
    {
        if (name.Contains("Right"))
        {
            whichOne = false;
        } else if (name.Contains("Left"))
        {
            whichOne = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            if (parent.name.Contains("Right")) 
            {
                whichOne = true;
            } else if (parent.name.Contains("Left"))
            {
                whichOne = false;
            }
        }
    }

    public GameObject WhichConnector()
    {
        if (whichOne)
        {
            return ConnectorRight;
        } else
        {
            return ConnectorLeft;
        }
    }
}
