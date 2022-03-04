using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    private player Player;
    [SerializeField]
    private Image lives;
    [SerializeField]
    private Sprite[] currentlives;
    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private Text restartText;
    // Start is called before the first frame update
    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        scoreText.text = "Score: 0"; 
        Player = GameObject.Find("Player").GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
     scoreText.text = "Score: " + Player.getScore();  
    }
    public void updateLivesImage(int liveAmount){
        lives.sprite = currentlives[liveAmount];
    }
    public void setGameOver(){
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        StartCoroutine(gameoverFlickerRoutine());
    }
    IEnumerator gameoverFlickerRoutine(){
        while(true){
            yield return new WaitForSeconds(0.5f);
            gameOverText.gameObject.SetActive(true);
            restartText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            gameOverText.gameObject.SetActive(false);
            restartText.gameObject.SetActive(false);
        }
    }
}
