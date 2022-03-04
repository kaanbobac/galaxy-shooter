using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float forwardSpeed = 4f;
    private player Player;
    private  Animator animator;
    private AudioSource explodeSound;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("Player") != null)
            Player = GameObject.Find("Player").GetComponent<player>();
        animator = gameObject.GetComponent<Animator>();
        explodeSound = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down*forwardSpeed*Time.deltaTime);
        if(transform.position.y <= -6.7f){
            transform.position = new Vector3(Random.Range(-10, 10),-1 * transform.position.y,0);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
            Player.Damage();
        if(other.tag == "Laser"){
            Player.increaseScore(10);
            Destroy(other.gameObject);
        }
        destroyEnemy();
    }
    private void destroyEnemy(){
        animator.SetTrigger("onEnemyDestroy");
        explodeSound.Play();
        Destroy(this.gameObject, 2.4f);
    }
}
