using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters;
using UnityStandardAssets.CrossPlatformInput;
public class player : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 5f;
    private float forwardSpeed = 1.1f;
    [SerializeField]
    private GameObject prefabLaser;
    [SerializeField]
    private GameObject tripleShotPrefab;
    [SerializeField]
    private float coolDown= 0.2f;
    private float nextFire;
    private int live =3;
    private Spawn_Manager spawn_Manager;
    [SerializeField]
    private bool isTripleShotActive = false;
    private float tripleShootTime =0f;
    private int score = 0;
    private UiManager uiManager;
    private GameManager gameManager;
    private Animator animator;
    [SerializeField]
    private GameObject rightEngine,leftEngine;

    private AudioSource laserAudio;
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        spawn_Manager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        uiManager = GameObject.Find("uiManager").GetComponent<UiManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        laserAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       CalculatePosition();
       CreateLaser();    
    }
    void CalculatePosition(){
        float speedFactor = speed*Time.deltaTime;
        float xDirection = CrossPlatformInputManager.GetAxis("Horizontal"); // Input.GetAxis("Horizontal");
        float yDirection = CrossPlatformInputManager.GetAxis("Vertical"); //Input.GetAxis("Vertical");
        //transform.Translate(Vector3.forward*forwardSpeed*Time.deltaTime);
        transform.Translate(new Vector3(xDirection,yDirection)*speedFactor);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-9,9),Mathf.Clamp(transform.position.y,-4,4),0);
    }
    void CreateLaser(){
        if((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire")) && Time.time > nextFire){
            nextFire = Time.time + coolDown;
            if(isTripleShotActive && (Time.time - tripleShootTime < 10f )){
                Instantiate(tripleShotPrefab,transform.position,Quaternion.identity);
            }  
            else    
                Instantiate(prefabLaser,transform.position + new Vector3(0,0.8f,0),Quaternion.identity);
           laserAudio.Play();     
        }      
    }
    public void Damage(){
        live = live-1;
        if(live == 2){
            new WaitForSeconds(2f);
            this.rightEngine.SetActive(true);
        }
        else if(live == 1){
            new WaitForSeconds(2f);
            this.leftEngine.SetActive(true);
        }    
        else if(live == 0){
            gameManager.setGameOver();
            uiManager.setGameOver();
            spawn_Manager.setIsContinueSpawn(false);
            Destroy(this.gameObject);
        }
        uiManager.updateLivesImage(live);        
    }
    public void setIsTripleShotActive(bool value){
        startTimer();
        this.isTripleShotActive = value;
    }
    private void startTimer(){
        tripleShootTime = Time.time;
    }
    public void increaseScore(int score){
        this.score += score;
    }
    public int getScore(){
        return this.score;
    }
}
