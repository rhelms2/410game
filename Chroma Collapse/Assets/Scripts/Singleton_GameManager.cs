using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.UI;
using Unity.VisualScripting;

/*
    This class contains logic for any kind of transitions right now. It does scene transitions and manages
    where the player object is spawned. The game object also contains all sound effects associated with
    the player, basically everything that would make sense not to leave in a specific scene.
*/

public class GameManager : MonoBehaviour 
{
    // Putting the control listener in here for now for all classes to access. All inputs will eventaully be taken in one class...
    public static PlayerControls controls;
    public static GameManager instance;
    private GameObject player;

    // These are all variables used for spawning/respawning the player
    private GameObject spawn_positions;
    private int most_recent_spawn_index = 0;
    private string respawn_scene_name = null;
    private int respawn_index = 0;

    // this is temporary while I figure out a better way to do this
    [SerializeField] UnityEngine.UI.Image display;
    [SerializeField] GameObject gameoverUI;

    void Awake() 
    {
        if (instance != null) {
            // Debug.Log("Destroying GameManager instance");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = Player_Singleton.player_instance.gameObject;
        controls = new PlayerControls();
        controls.Enable();
    }

    // FOR TESTING
    void Update() {

        // Respawn player

        if (Input.GetKeyDown("g")) {
            Restart();
        }

        // Set the most recent spawn point for respawning

        if (Input.GetKeyDown("0")) {
            most_recent_spawn_index = 0;
        }
        if (Input.GetKeyDown("1")) {
            most_recent_spawn_index = 1;
        }
        if (Input.GetKeyDown("2")) {
            most_recent_spawn_index = 2;
        }
        if (Input.GetKeyDown("3")) {
            most_recent_spawn_index = 3;
        }
        if (Input.GetKeyDown("4")) {
            most_recent_spawn_index = 4;
        }

        // Get color switcher

        if (Input.GetKeyDown("o")) {
            for (int i = 0; i < 4; i++) {
                Player_Inventory.instance.ActivateHUDItem(i);
            }
        }
    }

    public void GameOver()
    {
        // Tint display
        display.color = new Color(255f, 0f, 0f, 0.15f);

        // Activate the game object which shows options
        gameoverUI.SetActive(true);

        // Freeze the player
        Player_Singleton.player_instance.SetPlayerState((int) Player_Singleton.state.freeze);

        // Give player access to the mouse to choose game over options
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    // This function is called by the "Restart" button on the GameOverUI in canvas
    public void Restart() {
        if (respawn_scene_name != null) {
            Debug.Log("Loading a respawn point for restart");
            Load_New_Scene(respawn_scene_name, respawn_index);
        }
        else {
            Debug.Log("respawn null. Loading the most recent spawn");
            Load_New_Scene(SceneManager.GetActiveScene().name, most_recent_spawn_index);
        }

        gameoverUI.SetActive(false);
        
        // Untint display
        display.color = new Color(0f, 0f, 0f, 0f);

        // Replenish health

        Player_Singleton.player_instance.ChangeCurrentHealth(Player_Singleton.MAX_HEALTH);

        // Return normal mouse functionality to player
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // This function is called by the "Quit" button on the GameOverUI in canvas
    public void Quit() {
        Application.Quit();
    }

    // Spawns are all just empty transforms with an empty parent that is tagged "Spawn". Used for scene transitions and respawning
    void GetSpawns() {
        spawn_positions = GameObject.FindWithTag("Spawn");
    }

    // Respawn points are set based on player contact with them. These take priority if the player dies. If no respawn is set, GameOver will choose the last spawn point loaded
    public void SetRespawn(string new_respawn, int index) {
        Debug.Log("Setting a new respawn point");
        respawn_scene_name = new_respawn;
        respawn_index = index;
    }

    // COROUTINE VERSION FOR SCENE TRANSITIONER

    /* 
    This method is called from the object that triggers a scene transition, and then 
    triggers the scene transition coroutine. If the coroutine is triggered by an object
    in a scene that will be destroyed in the transition, any yield statements will return
    to a destroyed stack(?) and the operation will not continue past that point. Since
    this method exists outside both the old and new scene, the coroutine will continue
    execution on the next frame. This is currently the only way I know how to deal with
    this issue...
    */
    public void Load_New_Scene(string scene_name, int spawn_point_index) {
        StartCoroutine(Scene_Transition(scene_name, spawn_point_index));
    }

    IEnumerator Scene_Transition(string scene_name = null, int spawn_point_index = 0) {

        // This method will be reused for respawning the player. A respawn object contains a scene_name and transform location...
        Debug.Log("|| Starting Scene transition ||");

        // PLACEHOLDER
        /*
        Animator animation = display.GetComponent<Animator>();
        Debug.Log("Playing fade out animation");
        animation.SetBool("Fade_in", false);
        animation.enabled = true; 
        player.GetComponent<AudioListener>().enabled = false;

        /*
        var old_scene = SceneManager.GetActiveScene();
        Debug.Log("Currently active scene: " + old_scene.name);
        */

        Debug.Log("Calling screen fade to black");

        FadeManager.instance.StartFadeOut();

        while (FadeManager.instance.IsFadingOut) {
            yield return null;
        }

        Debug.Log("Scene to load: " + scene_name);
            
        Debug.Log("Setting player state to freeze, input should be locked");
        Player_Singleton.player_instance.SetPlayerState((int) Player_Singleton.state.freeze);        

        /* 
        SceneManager.LoadScene(scene_name, LoadSceneMode.Additive);
        Debug.Log("New scene loaded additively");
        */

        Debug.Log($"New scene, {scene_name} loading");
        AsyncOperation loadingOp = SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Single);

        while (!loadingOp.isDone) {
            Debug.Log("Loading progress: " + loadingOp.progress * 100 + "%");
            yield return null;
        }

        Debug.Log("|| Loading complete! ||");

        Debug.Log("Fetching spawn location...");

        Transform spawn_point;

        GetSpawns();
        spawn_point = spawn_positions.transform.GetChild(spawn_point_index);

        // Setting this for case where respawn has not been loaded
        most_recent_spawn_index = spawn_point_index;
        
        Debug.Log("The position should be: " + spawn_point);
        Player_Singleton.player_instance.SetPlayerPosition(spawn_point.position);
        Player_Singleton.player_instance.SetPlayerRotation(spawn_point.rotation);

        /*
        Debug.Log("Playing fade in animation");
        animation.SetBool("Fade_in", true);
        */

        Debug.Log("Starting FadeIn");

        FadeManager.instance.StartFadeIn();

        while (FadeManager.instance.IsFadingIn) {
            yield return null;
        }

        player.GetComponent<AudioListener>().enabled = true;

        Debug.Log("Setting player state to normal, inputs should register as normal");
        Player_Singleton.player_instance.SetPlayerState((int) Player_Singleton.state.normal); 
        Debug.Log("|| Scene transition completed ||");
    }
}
