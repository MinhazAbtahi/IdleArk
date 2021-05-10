using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public static BotSpawner instance;
    public Transform[] enemySpawnPoint;
    public GameObject[] enemy;

    public float spawnTime;
    public float spawnTimeMax;

    public GameObject enemy_cloner;
    public Material clonerMat;


    private void Awake()
    {
        if (instance == null) instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.startGame) return;
        if (GameManager.instance.playerCon.isDead) return;
        if (GameManager.instance.victory) return;
        //if (!GameManager.instance.gameStart) return;
        spawnTime += Time.deltaTime;
        if (spawnTime >= spawnTimeMax)
        {
            GameObject botIns = Instantiate(enemy[Random.Range(0, enemy.Length)], enemySpawnPoint[Random.Range(0, enemySpawnPoint.Length)].position, Quaternion.identity, transform);
            int randIndex = Random.Range(0, 2);
            spawnTime = 0;
        }
    }
}
