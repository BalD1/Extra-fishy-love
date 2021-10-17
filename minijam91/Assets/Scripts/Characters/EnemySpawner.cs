using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private bool spawnEnemies = true;

    [Header("Stats")]
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private float timeBeforeReduce;
    [SerializeField] private float amountToReduce;
    [SerializeField] private int timesToReduceSpawnTime;
    private float spawnTimer;

    [SerializeField] private float speed;

    [SerializeField] private Transform basePos;
    [SerializeField] private Transform targetPos;
    private Transform direction;

    private void Start()
    {
        if(basePos == null)
            basePos = this.transform;

        direction = targetPos;

        spawnTimer = maxSpawnTime * 2;
        StartCoroutine(ReduceSpawnTime(timeBeforeReduce, --timesToReduceSpawnTime));
    }

    private void Update()
    {
        if(GameManager.Instance.GameState == GameManager.GameStates.InGame)
        {
            Movements();
            spawnTimer = spawnTimer - Time.deltaTime;
            if(spawnTimer <= 0)
                SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, enemies.Count);
        if(spawnEnemies)
            Instantiate(enemies[randomEnemy], this.transform.position, Quaternion.identity);

        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void Movements()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, direction.position, speed * Time.deltaTime);

        if(this.transform.position.y <= targetPos.position.y)
            direction = basePos;
        else if (this.transform.position.y >= basePos.position.y)
            direction = targetPos;
    }

    private IEnumerator ReduceSpawnTime(float time, int timesToReduce)
    {
        yield return new WaitForSeconds(time);
        minSpawnTime -= amountToReduce;
        maxSpawnTime -= amountToReduce;
        if(timesToReduce > 0)
            StartCoroutine(ReduceSpawnTime(timeBeforeReduce, --timesToReduceSpawnTime));
    }
}
