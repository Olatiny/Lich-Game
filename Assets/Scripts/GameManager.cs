using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [Header("relic fields")]
    

    [SerializeField][Tooltip("a list of the names of all of the relics")]private List<string> relicNames;
    [SerializeField][Tooltip("a map of all relics and if they've been found. 0 = not found, 1 = found")]private Dictionary<string,int> relics;
    
    [Header("falling fields")]
    [SerializeField][Tooltip("current falling speed")]public float fallSpeed;
    [SerializeField][Tooltip("the fall speed at the start of the game")]public float startingFallSpeed;
    [SerializeField][Tooltip("the rate at which the character fall speed increases per second")]public float fallAcceleration;

    [Header("game state fields")]
    [SerializeField]private bool gameStarted;
    /**if the game is currently paused*/
    [SerializeField] private bool paused;
    private bool canPause = true;
    [SerializeField]public enum gameState {menu, falling, loot_room, die};
    [SerializeField]public gameState currentState;
    
    private int score = 0;
    [Header("camera fields")]
    [SerializeField][Tooltip("The main game camera")]private GameObject gameCamera;
    [SerializeField][Tooltip("the starting rotation of the camera")]private Quaternion startingRotation;
    [SerializeField][Tooltip("the primary rotation for the camera to reach")]private Quaternion finalRotation;
    [SerializeField][Tooltip("the position for the player on the main menu")]private Vector3 playerMenuStartPos;
    [SerializeField][Tooltip("the positions where the player starts after the game begins")]private Vector3 playerStartPos;
    [SerializeField][Tooltip("the total time it should take the camera to rotate")]private float rotateTime = 1000f;
    [SerializeField][Tooltip("the total time it should take the player to move to their start position")]private float playerMoveTime = 1000f;

    [Header("manager fields")]
    [SerializeField][Tooltip("the menu manager object")]private MenuManager menuMan;
    [SerializeField] private GameObject player;

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
        score = 0;
        gameStarted = false;
        UnPause();
        menuMan.OpenStartMenu();
        gameCamera.GetComponent<CameraScript>().SetRotation(startingRotation);
        player.transform.position = playerMenuStartPos;

        //Respawn player
        //do the camera reset stuff
        //reset level
    }

    public void ResetScene(){
        InitializeScene();
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeScene();
    }

    public void StartGame(){

        //play game start scene
        //rotate camera
        Debug.Log("start falling");
         StartCoroutine(Camera.main.GetComponent<CameraScript>().StartMove(playerStartPos, playerMoveTime));
        Invoke("StartRotate", (playerMoveTime));
        
    }

    private void StartRotate(){
        StartCoroutine(Camera.main.GetComponent<CameraScript>().StartRotate(finalRotation, rotateTime));
        Invoke("StartFalling", (rotateTime));
    }

    private void StartFalling(){
        fallSpeed = startingFallSpeed;
        ChangeState(gameState.falling);
    }


    public void TogglePause(){
        if(paused){
            UnPause();
        }
        else{
            Pause();
        }
    }

    public void Pause(){
        paused = true;
            menuMan.OpenPauseMenu();
            //maybe change
            Time.timeScale = 0f;
    }
    public void UnPause(){
        paused = false;
            menuMan.ClosePauseMenu();
            //maybe change
            Time.timeScale = 1f;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(!paused){
            if(currentState == gameState.falling){
                fallSpeed += fallAcceleration * Time.fixedDeltaTime;
                score += 1;
                menuMan.UpdateScore(score);
            }
        }
    }

    public void ChangeState(gameState newState){
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

    public void FindRelic(string name){
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

    void OnAppicationQuit(){
        SavePrefs();
    }

    public float GetFallSpeed(){
        if(paused){
            return 0;
        }
        return fallSpeed;
    }

    public void Die(){
        ChangeState(gameState.die);
    }

    public int GetScore(){
        return score;
    }
}
