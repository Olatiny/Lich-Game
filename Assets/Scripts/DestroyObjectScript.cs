using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectScript : MonoBehaviour
{
    [SerializeField] GameObject parent;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Spawn"))
        {
            Destroy(parent);
        }
    }
}
