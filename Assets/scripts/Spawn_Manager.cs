using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyContainer;
     [SerializeField]
    private GameObject tripleShoot;
    private bool isContinueSpawn = true;
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnRoutineTriple());           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnRoutine(){
        while(isContinueSpawn){
            yield return new WaitForSeconds(5.0f);
            GameObject newEnemy = Instantiate(enemyPrefab,new Vector3(Random.Range(-7.0f,7.0f),7,0),Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
        }
    }
     IEnumerator SpawnRoutineTriple(){
        while(true){
            yield return new WaitForSeconds(60.0f);
            Instantiate(tripleShoot,new Vector3(Random.Range(-7.0f,7.0f),7,0),Quaternion.identity);
        }
    }
    public void setIsContinueSpawn(bool value)
    {
        this.isContinueSpawn = value;
    }
}
