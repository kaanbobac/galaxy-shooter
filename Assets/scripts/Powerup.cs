using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Start is called before the first frame update
    private float forwardSpeed = 4f;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
         transform.Translate(Vector3.down*forwardSpeed*Time.deltaTime);
        if(transform.position.y <= -6.7f){
             Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            audioSource.Play();
            player player = other.transform.GetComponent<player>();
            player.setIsTripleShotActive(true);
            Destroy(this.gameObject);
        }
    }
}
