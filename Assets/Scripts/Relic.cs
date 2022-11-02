using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic : MonoBehaviour
{
    [SerializeField][Tooltip("this relic's name")]private string relicName;
    [SerializeField][Tooltip("this relic's lore")]private string relicLore;
    [SerializeField][Tooltip("this relic's starting position")]private Vector3 relicMenuSpawnPos;
    [SerializeField][Tooltip("if this can collide")]private bool canCollide = true;
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
        Debug.Log("relic");
        if(canCollide && col.gameObject.tag == playerTag && GameManager.Instance.CanMove()){
            GameManager.Instance.FindRelic(this);
            GameManager.Instance.GetMusMan().RelicCollectSFX();
            Destroy(gameObject);
        }
    }

    public Vector3 GetStartPos(){
        return relicMenuSpawnPos;
    }

    public void DontCollide(){
        canCollide = false;
    }
}
