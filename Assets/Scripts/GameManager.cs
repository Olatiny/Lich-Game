using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [Header("relic fields")]
    

    [SerializeField][Tooltip("a list of the names of all of the relics")]private List<string> relicNames;
    [SerializeField][Tooltip("a list of the names of all of the relics")]private List<GameObject> relicObjects;
    [SerializeField][Tooltip("a map of all relics and if they've been found. 0 = not found, 1 = found")]private Dictionary<string,int> relics;
    [SerializeField][Tooltip("a map of all relics and if they've been found. 0 = not found, 1 = found")]private Dictionary<string,GameObject> relicObjectsMap;
    
    [Header("falling fields")]
    [SerializeField][Tooltip("current falling speed")]public float fallSpeed;
    [SerializeField][Tooltip("the fall speed at the start of the game")]public float startingFallSpeed;
    [SerializeField][Tooltip("the rate at which the character fall speed increases per second")]public float fallAcceleration;


    [Header("game state fields")]
    [SerializeField]private bool inDialog;
    /**if the game is currently paused*/
    [SerializeField] private bool paused;
    private bool canPause = true;
    [SerializeField]private bool rotating = false;
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
    [SerializeField][Tooltip("the music manager script")]private MusicScript musScript;
    [SerializeField][Tooltip("the player particles script")]private CharacterParticles charParts;
    [SerializeField][Tooltip("the item spawner")]private ItemSpawner itemSpawn;
    [SerializeField][Tooltip("the level resetter")]private ResetLevelScript resLevScript;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject killZone;

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
        killZone.SetActive(false);
        ChangeState(gameState.menu);
        rotating = false;
        fallSpeed = 0f;
        score = 0;
        inDialog = false;
        UnPause();
        menuMan.OpenStartMenu();
        gameCamera.GetComponent<CameraScript>().SetRotation(startingRotation);
        gameCamera.transform.position = new Vector3(0, 0, gameCamera.transform.position.z);
        player.transform.position = playerMenuStartPos;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        if(charParts){
            charParts.Reset();
        }
        if(itemSpawn){
            itemSpawn.StopFalling();
        }
        SpawnInObjects();
        if(resLevScript){
            resLevScript.ResetLevel();
        }
        //Respawn player
        //do the camera reset stuff
        //reset level
    }

    public void SpawnInObjects(){
        foreach(string s in relicNames){
            if(relics[s] == 1){
                GameObject obj = (GameObject)Instantiate(relicObjectsMap[s], relicObjectsMap[s].GetComponent<Relic>().GetStartPos(), Quaternion.identity);
                obj.GetComponent<FallingObject>().enabled = false;
                obj.GetComponent<Relic>().DontCollide();
            }
        }
    }

    public void ResetScene(){
        InitializeScene();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        relics = new Dictionary<string, int>();
        relicObjectsMap = new Dictionary<string, GameObject>();
        for(int i = 0; i < relicNames.Count; i++){
            relics.Add(relicNames[i],0);
            relicObjectsMap.Add(relicNames[i],relicObjects[i]);
        }
        InitializeScene();
    }

    public void StartGame(){

        //play game start scene
        //rotate camera
        Debug.Log("start falling");
         StartCoroutine(Camera.main.GetComponent<CameraScript>().StartMove(playerStartPos, playerMoveTime));
         if(charParts){
            charParts.StartFloating();
        }
        Invoke("StartRotate", (playerMoveTime));
        rotating = true;
        musScript.FadeMenuMusic(); //-----UNCOMMENT
        
    }

    private void StartRotate(){
        StartCoroutine(Camera.main.GetComponent<CameraScript>().StartRotate(finalRotation, rotateTime));
        if(charParts){
            charParts.StartRotating();
        }
        if(musScript){
            musScript.FallRumble();
        }
        Invoke("StartFalling", (rotateTime));
        
    }

    private void StartFalling(){
        fallSpeed = startingFallSpeed;
        ChangeState(gameState.falling);
        if(charParts){
            charParts.StartFalling();
        }
        if(itemSpawn){
            itemSpawn.StartFalling();
        }
        if(musScript){
            musScript.FallScream();
        }
        rotating = false;
        killZone.SetActive(true);
    }


    public void TogglePause(){
        if(!canPause){
            return;
        }
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
            if(musScript){
                musScript.PauseAdjust();
            }
            Time.timeScale = 0f;
    }
    public void UnPause(){
        paused = false;
            menuMan.ClosePauseMenu();
            if(musScript){
                musScript.UnpauseAdjust();
            }
            Time.timeScale = 1f;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(!paused && !inDialog){
            if(currentState == gameState.falling){
                fallSpeed += fallAcceleration * Time.fixedDeltaTime;
                score += 1;
                menuMan.UpdateScore(score);
            }
        }
        if(inDialog){
            if(Input.GetKey(KeyCode.Space)){
                EndDialog();
            }
        }
    }

    public void ChangeState(gameState newState){
        musScript.UpdateMusic(newState);
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

    public void FindRelic(Relic relic){
        if(!relics.ContainsKey(relic.GetName())){
            Debug.LogError("TRIED TO FIND NOTEXISTENT RELIC " + relic.GetName());
            return;
        }
        relics[relic.GetName()] = 1;
        if(charParts){
            charParts.GainedRelic();
        }
        inDialog = true;
        if(musScript){
            musScript.PauseAdjust();
        }
        canPause = false;
        menuMan.GainedRelic(relic);

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
        if(!CanMove()){
            return 0;
        }
        return fallSpeed;
    }

    public void Die(){
        if(musScript){
            ChangeState(gameState.die);
        }
        menuMan.OpenGameOverMenu();
    }

    public int GetScore(){
        return score;
    }

    public void TookDamage(int currentHealth, int damageTaken){
        menuMan.TookDamage(currentHealth,damageTaken);
        if(charParts){
            charParts.Hurt();
        }
        if(musScript){
            musScript.DamageSFX();
        }
    }

    public void GainedHealth(int currentHealth, int healthGained){
        Debug.Log("gained health GM");
        menuMan.GainedHealth(currentHealth,healthGained);
        if(charParts){
            charParts.GainedHealth();
        }
        if(musScript){
            musScript.HealthSFX();
        }
    }

    public bool CanMove(){
        return !paused && !inDialog && !rotating && (currentState == gameState.falling || currentState == gameState.loot_room);
    }

    public void EnterLootRoom(){
        ChangeState(gameState.loot_room);
        //make the character start using gravity instead of falling room.
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
    public void ExitLootRoom(){
        ChangeState(gameState.falling);
        //make the character stop using gravity
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void EndDialog(){
        inDialog = false;
        if(musScript){
            musScript.UnpauseAdjust();
        }
        canPause = true;
        menuMan.EndDialog();
    }

    public GameObject GetRelicToSpawn(){
        foreach(string i in relicNames){
            if(relics[i] == 0){
                return relicObjectsMap[i];
            }
        }
        return null;
    }

    public MusicScript GetMusMan(){
        return musScript;
    }

    public GameObject GetPlayer(){
        return player;
    }
}
