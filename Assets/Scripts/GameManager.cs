using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]public enum gameState {menu, falling, loot_room};

    [SerializeField][Tooltip("a list of the names of all of the relics")]private List<string> relicNames;
    [SerializeField][Tooltip("a map of all relics and if they've been found. 0 = not found, 1 = found")]private Dictionary<string,int> relics;
    

    [SerializeField][Tooltip("current falling speed")]public float fallSpeed;
    [SerializeField][Tooltip("the fall speed at the start of the game")]public float startingFallSpeed;
    [SerializeField][Tooltip("the rate at which the character fall speed increases per second")]public float fallAcceleration;

    [SerializeField]public gameState currentState;
    [SerializeField]private bool gameStarted;
    /**if the game is currently paused*/
    [SerializeField] private bool paused;
    private bool canPause = true;
    private int score = 0;
    [Tooltip("Current Game Manager")]
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    void Awake(){
        if (Instance != this)
        {
            Destroy(this.gameObject);
            Destroy(this);
            return;
        }
        _instance = this;
        
    }
    
    void InitializeScene(){
        ChangeState(gameState.menu);
        fallSpeed = 0f;
        gameStarted = false;
        //Respawn player
        //do the camera reset stuff
        //reset level
    }

    void ResetScene(){
        InitializeScene();
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeScene();
    }

    void StartFalling(){

        //play game start scene
        //rotate camera

        fallSpeed = startingFallSpeed;
        ChangeState(gameState.falling);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentState == gameState.falling){
            fallSpeed += fallAcceleration * Time.fixedDeltaTime;
        }
    }

    void ChangeState(gameState newState){
        //call audio manager with the new state
        currentState = newState;
    }

    void CheckStoreData(){
        if(relics.Count > 0 && !PlayerPrefs.HasKey(relicNames[0])){
            ClearPrefs();
        }
        else if(relics.Count > 0){
            foreach (string item in relicNames)
            {
                relics[item] = PlayerPrefs.GetInt(item);
            }
        }
    }

    void ClearPrefs(){
        if(relics.Count < 1){
            return;
        }
        foreach (string item in relicNames)
        {
            PlayerPrefs.SetInt(item, 0);
            relics[item] = 0;
        }
    }

    void FindRelic(string name){
        if(!relics.ContainsKey(name)){
            Debug.LogError("TRIED TO REMOVE NOTEXISTENT RELIC " + name);
            return;
        }
        relics[name] = 1;

    }

    void SavePrefs(){
        foreach (string item in relicNames)
        {
            PlayerPrefs.SetInt(item,relics[item]);
        }
    }
}
