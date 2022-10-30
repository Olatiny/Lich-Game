using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField][Tooltip("the name of the player's tag")]private string playerTag;

    void OnCollisionEnter(Collision col){
        if(col.collider.gameObject.tag == playerTag){
            col.collider.gameObject.GetComponent<PlayerHealth>().Die();
        }
    }
}
