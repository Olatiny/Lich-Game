using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField][Tooltip("the name of the player's tag")]private string playerTag;

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == playerTag){
            col.gameObject.GetComponent<PlayerHealth>().Die();
        }
    }

    void OnTriggerExit2D(Collider2D col){
        Debug.Log("trigger left top");
        if(col.GetComponent<FallingObject>()){
            Destroy(col.gameObject);
        }
    }
}
