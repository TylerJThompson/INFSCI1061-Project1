using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float lastSpawn = 0;
    public GameManager gameManager;
    public Pooler hugePool;
    public Pooler bigPool;
    public Pooler mediumPool;
    public Pooler smallPool;
    public Pooler tokenPool;
    public GameObject boss;
    public float spawnInterval;
    private bool spawnBool = false;

    // Update is called once per frame
    void Update()
    {
        if (gameManager.getScore() < gameManager.maxScore || spawnBool) objectSpawn();
        else bossSpawn();
    }

    void objectSpawn()
    {
        GameObject huge = hugePool.getPooledObject();
        GameObject big = bigPool.getPooledObject();
        GameObject medium = mediumPool.getPooledObject();
        GameObject small = smallPool.getPooledObject();
        GameObject token = tokenPool.getPooledObject();
        if((Time.fixedTime - lastSpawn) > spawnInterval)
        {
            int rand = Random.Range(0, 100);
            if (huge != null && rand < 15)
            {
                huge.transform.position = new Vector2(15, Random.Range(-5.5f, 5.5f));
                huge.SetActive(true);
                lastSpawn = Time.fixedTime;
            }
            else if (big != null && rand < 35)
            {
                big.transform.position = new Vector2(15, Random.Range(-5.5f, 5.5f));
                big.SetActive(true);
                lastSpawn = Time.fixedTime;
            }
            else if (medium != null && rand < 60)
            {
                medium.transform.position = new Vector2(15, Random.Range(-5.5f, 5.5f));
                medium.SetActive(true);
                lastSpawn = Time.fixedTime;
            }
            else if (small != null && rand < 90)
            {
                small.transform.position = new Vector2(15, Random.Range(-5.5f, 5.5f));
                small.SetActive(true);
                lastSpawn = Time.fixedTime;
            }
            else if (token != null)
            {
                token.transform.position = new Vector2(15, Random.Range(-5.5f, 5.5f));
                token.SetActive(true);
                lastSpawn = Time.fixedTime;
            }
        }
    }

    void bossSpawn()
    {
        boss.gameObject.SetActive(true);
    }

    public void setSpawnInterval(float interval)
    {
        spawnInterval = interval;
    }

    public void toggleSpawn()
    {
        spawnBool = !spawnBool;
    }
}
