using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField][Tooltip("small healing item")]private GameObject smallHeal;
    [SerializeField][Tooltip("big healing item")]private GameObject bigHeal;
     [SerializeField][Tooltip("the likelihood of a bit Heal spawning")] private int bigHealChance;
      [SerializeField][Tooltip("the likelihood of a relic spawning")] private int relicChance;

      [SerializeField][Tooltip("the ghost enemies")]private GameObject ghost;
    
    GameObject spawnedObjectHolder;
    
    [Header("spawn stats")]
    [SerializeField][Tooltip("left spawn bound")]private GameObject leftSpawnBound;
    [SerializeField][Tooltip("right spawn bound")]private GameObject rightSpawnBound;
    [SerializeField][Tooltip("the average amount of time between objects spawning")]private float avgSpawnTime = 10;
    [SerializeField][Tooltip("the variance on the amount of time between objects spawning")]private float spawnTimeVar = 1;
    [SerializeField]private float spawnTimer = 0;
    [SerializeField][Tooltip("if the ghosts can spawn yet")]private bool canGhostsSpawn = false;
    [SerializeField][Tooltip("the average amount of time between ghosts spawning")]private float avgGhostSpawnTime = 10;
    [SerializeField][Tooltip("the variance on the amount of time between ghosts spawning")]private float spawnGhostTimeVar = 1;
    [SerializeField]private float ghostSpawnTimer = 0;
    [SerializeField]private float ghostSpawnScore = 500;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManager.Instance.CanMove()){
            if(!canGhostsSpawn && GameManager.Instance.GetScore() > ghostSpawnScore){
                LetGhostsSpawn();
            }
            if(spawnTimer > 0){
                spawnTimer -= Time.fixedDeltaTime;
            }
            else{
                SpawnObject();
                ResetTimer();
            }
            if(canGhostsSpawn){
                if(ghostSpawnTimer > 0){
                    ghostSpawnTimer -= Time.fixedDeltaTime;
                }
                else{
                    SpawnGhost();
                    ResetGhostTimer();
                }
            }
        }
    }

    public void StartFalling(){
        spawnedObjectHolder = new GameObject("spawned object holder");
        //spawnedObjectHolder.transform.parent = Camera.main.transform;
        ResetTimer();
    }

    public void StopFalling(){
        if(spawnedObjectHolder){
            Destroy(spawnedObjectHolder);
        }
    }

    void SpawnObject(){
        Vector3 spawnPos = Vector3.Lerp(leftSpawnBound.transform.position, rightSpawnBound.transform.position, Random.Range(0f,1f));
        GameObject spawnObj = PickObjToSpawn();
        GameObject newObj = (GameObject)Instantiate(spawnObj, spawnedObjectHolder.transform);
        newObj.transform.position = spawnPos;
    }

    void ResetTimer(){
        spawnTimer = Random.Range(avgSpawnTime - spawnTimeVar, avgSpawnTime + spawnTimeVar);
    }

    GameObject PickObjToSpawn(){
        float chance = Random.Range(0f,100f);
        if(chance < relicChance){
            GameObject result = GameManager.Instance.GetRelicToSpawn();
            if(!result){
                return bigHeal;
            }
            return result;
        }
        else if(chance < bigHealChance){
            return bigHeal;
        }
        return smallHeal;
    }

    void SpawnGhost(){
        Vector3 spawnPos = Vector3.Lerp(leftSpawnBound.transform.position, rightSpawnBound.transform.position, Random.Range(0f,1f));
        GameObject newObj = (GameObject)Instantiate(ghost, spawnedObjectHolder.transform);
        newObj.transform.position = spawnPos;
    }

    void ResetGhostTimer(){
        ghostSpawnTimer = Random.Range(avgGhostSpawnTime - spawnGhostTimeVar, avgGhostSpawnTime + spawnGhostTimeVar);
    }

    void LetGhostsSpawn(){
        canGhostsSpawn = true;
        SpawnGhost();
        ResetGhostTimer();
    }


}
