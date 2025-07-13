using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public class WaveSpawner : MonoBehaviourPun
{
    public Transform enemyPrefab;
    public Transform enemyPrefab1;
    public Transform enemyPrefab2;

    public Transform spawnPoint;

    public float timeBetweenWaves = 1.0f;
    private float countdown = 2.0f;
    private float check = 0.0f;
    private int[] arr = new int[3];

    private int count = 0;
    private int waveIndex = 10;

    private void Start()
    {
        arr[0] = 10;
        arr[1] = 10;
        arr[2] = 10;
    }
    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }


    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveIndex - 1; i++)
        {
            if (count == 3)
            {
                break;
            }
            check += 1.0f;
            for (int j = 0; j < arr[count]; j++)
            {
                SpawnEnemy();
                PlayerState.EnemyCount++;
                yield return new WaitForSeconds(0.5f);
            }
            count++;


        }
        //waveIndex++;

    }

    void SpawnEnemy()
    {
        if (count == 0)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else if (count == 1)
        {
            Instantiate(enemyPrefab1, spawnPoint.position, spawnPoint.rotation);
        }
        else if (count == 2)
        {
            Instantiate(enemyPrefab2, spawnPoint.position, spawnPoint.rotation);
        }


    }
}
