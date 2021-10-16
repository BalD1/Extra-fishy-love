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

    [SerializeField] private float speed;

    [SerializeField] private Transform basePos;
    [SerializeField] private Transform targetPos;
    private Transform direction;

    private void Start()
    {
        if(basePos == null)
            basePos = this.transform;

        direction = targetPos;

        SpawnEnemy();
    }

    private void Update()
    {
        Movements();
    }

    private void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, enemies.Count - 1);
        if(spawnEnemies)
            Instantiate(enemies[randomEnemy], this.transform.position, Quaternion.identity);

        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        StartCoroutine(SpawnTimer(spawnTime));
    }

    private IEnumerator SpawnTimer(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnEnemy();
    }

    private void Movements()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, direction.position, speed * Time.deltaTime);

        if(this.transform.position.y <= targetPos.position.y)
            direction = basePos;
        else if (this.transform.position.y >= basePos.position.y)
            direction = targetPos;
    }
}
