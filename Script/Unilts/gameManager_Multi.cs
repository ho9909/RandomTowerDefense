using Photon.Pun;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class gameManager_Multi : MonoBehaviourPunCallbacks
{
    
    private float timer = 0f;
    private float targetTime = 30f;

    public static bool next = false;

    private float time = 0f;
    private float executionInterval = 2f;

    public static int Money;
    public int startMoney = 15;
    public static int EnemyKillCount = 0;
    public static int EnemyCount = 0;
    public Text gold;
    public static int Lives;
    public Transform[] spawnPositions;
    public int startLives = 500;
    public static int Rounds;
    public static bool start = false;
    public static bool stop = true;
    Vector3 player2;
    public Image healthBar;
    public GameObject playerPrefab;

    public void Start()
    {   
        Money = startMoney;
        Lives = startLives;
        EnemyKillCount = 0;
        EnemyCount = 0;
        gold.text = Money.ToString();

        Rounds = 0;
    }

    private void SpawnPlayer()
    {
        if (stop)
        {
            var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
            spawnPositions[0].position = spawnPositions[0].localPosition;
            spawnPositions[1].position = spawnPositions[1].localPosition;
            var spawnPosition = spawnPositions[localPlayerIndex % spawnPositions.Length];

            PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition.position, spawnPosition.rotation);
            stop = false;
        }
        else
        {
            return;
        }


    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }

    void CheckOtherPlayer()
    {
        // 현재 룸에 있는 모든 플레이어 가져오기

        if(PhotonNetwork.PlayerList.Length == 2)
        {
            SpawnPlayer();
            start = true;
        }

    }
    public static void TakeDamage(int amount)
    {
        Lives -= amount;
    }
    private void Update()
    {
        healthBar.fillAmount = Lives / startLives;
        CheckOtherPlayer();
        if (start)
        {
            time += Time.deltaTime;
            if (EnemyKillCount == 10)
            {
                Money += 5;
                EnemyKillCount = 0;
            }
            if (time > executionInterval)
            {
                if (EnemyCount == 150)
                {
                    Lives -= 10;
                }
                time = 0f;
            }
            if (EnemyCount == 0 && timer == 0f)
            {
                //check();
            }

            gold.text = Money.ToString();

            timer += Time.deltaTime;
            if (timer > targetTime)
            {
                next = true;
                check();
                timer = 0f;
            }
            next = false;
        }

    }

    public void check()
    {
        int Grade_cat = 0;
        int Grade_dog = 0;
        int Grade_mouse = 0;
        var turret = new List<GameObject>();
        GameObject[] cat = GameObject.FindGameObjectsWithTag("cat");
        GameObject[] dog = GameObject.FindGameObjectsWithTag("dog");
        GameObject[] mouse = GameObject.FindGameObjectsWithTag("mouse");
        turret.AddRange(cat);
        turret.AddRange(dog);
        turret.AddRange(mouse);
        foreach (GameObject t in turret)
        {
            if (t == null) break;

            if (t.tag == "cat")
            {
                Grade_cat += t.GetComponent<Turret_multi>().tower_count;
            }
            else if (t.tag == "dog")
            {
                Grade_dog += t.GetComponent<Turret_multi>().tower_count;
            }
            else if (t.tag == "mouse")
            {
                Grade_mouse += t.GetComponent<Turret_multi>().tower_count;
            }
        }
        if (turret.Count != 0)
        {
            WaveSpawner_Multi.turretCounts[0] = Grade_cat;
            WaveSpawner_Multi.turretCounts[1] = Grade_dog;
            WaveSpawner_Multi.turretCounts[2] = Grade_mouse;
        }

    }

}
