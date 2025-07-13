using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class WaveSpawner_Multi : MonoBehaviourPun
{
    public Transform enemyPrefab;
    public Transform enemyPrefab1;
    public Transform enemyPrefab2;

    public Transform spawnPoint;

    public float timeBetweenWaves = 1.0f;
    private float countdown = 2.0f;
    private float check = 0.0f;
    private int[] arr = new int[3];
    private bool temp = false;
    private bool cur = true;
    private int count = 0;
    private int waveIndex = 10;
    public static int[] turretCounts = new int[3];

    private void Start()
    {
        arr[0] = 1;
        arr[1] = 1;
        arr[2] = 1;
        turretCounts = arr;
    }
    private void Update()
    {
        if (gameManager_Multi.start == true)
        {
            temp = countdown_multi.count;
            if (temp == true)
            {
                photonView.RPC("SetTurretCount", RpcTarget.Others, turretCounts);
            }
            if (temp == true || cur == true)
            {
                    StartCoroutine(SpawnWave());
                    countdown = timeBetweenWaves;
                    countdown_multi.count = false;
                    cur = false;
            }
            countdown -= Time.deltaTime;
        }
    }
    [PunRPC]
    public void SetTurretCount(int[] remoteTurret)
    {
        arr = remoteTurret;
    }

    IEnumerator SpawnWave()
    {
        if (photonView.IsMine)
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
                    gameManager_Multi.EnemyCount++;
                    yield return new WaitForSeconds(1.0f);
                }
                count++;


            }
        }

        //waveIndex++;

    }

    void SpawnEnemy()
    {
        if (count == 0)
        {
            PhotonNetwork.Instantiate(enemyPrefab.name, spawnPoint.position, spawnPoint.rotation);
        }
        else if (count == 1)
        {
            PhotonNetwork.Instantiate(enemyPrefab1.name, spawnPoint.position, spawnPoint.rotation);
        }
        else if (count == 2)
        {
            PhotonNetwork.Instantiate(enemyPrefab2.name, spawnPoint.position, spawnPoint.rotation);
        }


    }
}
