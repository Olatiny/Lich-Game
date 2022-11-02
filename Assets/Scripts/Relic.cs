using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic : MonoBehaviour
{
    [SerializeField][Tooltip("this relic's name")]private string relicName;
    [SerializeField][Tooltip("this relic's lore")]private string relicLore;
    public string playerTag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetName(){
        return relicName;
    }

    public string GetLore(){
        return relicLore;
    }

    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == playerTag){
            GameManager.Instance.FindRelic(this);
            Destroy(gameObject);
        }
    }
}
