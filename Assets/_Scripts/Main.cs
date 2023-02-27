using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S;
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemySpawnPadding = 1.5f;
    public float gameRestartDelay = 2f;
    public GameObject prefabPowerUp;
    public int scoreBoard = 0;
    private BoundsCheck boundsCheck;
    private void Awake() {
        S = this;
        boundsCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f/enemySpawnPerSecond);
    }

    public void SpawnEnemy(){
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);
        float enemyPadding = enemySpawnPadding;
        if(go.GetComponent<BoundsCheck>() != null){
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }
        Vector3 pos = Vector3.zero;
        float xMin = -boundsCheck.camWidth + enemyPadding;
        float xMax = boundsCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = boundsCheck.camHeight + enemyPadding;
        go.transform.position = pos;
        Invoke("SpawnEnemy", 1f/enemySpawnPerSecond);
    }

    public void ShipDestroyed(Enemy e){
        if(Random.value <= e.dropPowerUp){
            GameObject go = Instantiate(prefabPowerUp);
            PowerUp up = go.GetComponent<PowerUp>();
            up.transform.position = e.transform.position;
        }
    }

    public void DelayedRestart(float delay){
        Invoke("Restart", delay);
    }
    public void Restart(){
        SceneManager.LoadScene("SampleScene");
    }
}
