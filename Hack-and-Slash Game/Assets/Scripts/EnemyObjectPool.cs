using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : ObjectPool
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 spawnArea;

    public float secondsToSpawn;
    private float timer;
    private WaitForSeconds spawnTime;
    public WaitForSeconds respawnTime = new WaitForSeconds(1f);

    [SerializeField] private GameObject enemyHolder;

    private bool canSpawn = false;

    private int amountOfEnemiesToSpawn;

    // Start is called before the first frame update
     public override void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool, enemyHolder.transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
        //Debug.Log(pooledObjects[0].GetComponent<EnemyBase>().name);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        if (canSpawn && amountOfEnemiesToSpawn > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                SpawnEnemy();

                timer = secondsToSpawn;
            }
        }
    }

    private void SpawnEnemy()
    {
        
        GameObject tmp;
        Vector3 position = getRandomPosition();
        tmp = GetPooledObject();
        position += player.transform.position;
        if (tmp == null)
        {
            //Debug.Log("all enemies are being used");
            return;
        }
        tmp.GetComponent<EnemyBase>().ResetHealth();
        tmp.transform.position = position;
        amountOfEnemiesToSpawn -= 1;
        tmp.SetActive(true);
    }

    public Vector3 getRandomPosition()
    {
        Vector3 position = new Vector3();
        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;
        if (UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.z = spawnArea.z * f;
        }
        else
        {
            position.z = UnityEngine.Random.Range(-spawnArea.z, spawnArea.z);
            position.x = spawnArea.x * f;
        }
        position.y = 0f;
        //Debug.Log(position);
        return position;
    }

    public void Spawning(GameObject enemy)
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        //Debug.Log("waiting for 3 seconds");
        yield return respawnTime;
        //Debug.Log("3 seconds have passed");

    }

    public void SetCanSpawn(bool ans)
    {
        canSpawn = ans;
    }

    public void SetAmountOfEnemiesToSpawn(int amount)
    {
        amountOfEnemiesToSpawn = amount;
    }
}
