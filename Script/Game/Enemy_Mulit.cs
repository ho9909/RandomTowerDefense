using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Movement_multi))]
public class Enemy_Mulit : MonoBehaviourPun
{
    private Movement_multi m_Movement;
    public float startSpeed = 0f;
    [HideInInspector]
    public float speed;
    public bool posion;
    public float startHealth = 50;
    private float health;
    //public int worth = 50;
    public bool crash = false;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    void Start()
    {
        speed = startSpeed; 
        health = startHealth;
        m_Movement = GetComponent<Movement_multi>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }
    private void Update()
    {
        if(crash == true)
        {
            speed = 0;
        }
        else
        {
            speed = startSpeed;
        }
        if(posion == true)
        {
            TakeDamage(5);
            Slow(0.7f);
        }
        if(m_Movement.count == 3)
        {
            gameManager_Multi.Lives -= 30;
        }
        
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "dog_card") {
            crash = true;
            Invoke("ResumeMovement", 3f);
        }
        if (other.gameObject.tag == "mouse_card")
        {
            posion = true;
            Invoke("RemoveDebuff", 3f);
        }
    }
    void RemoveDebuff()
    {
        posion = false;
    }
    void ResumeMovement()
    {
        crash = false;
    }
    void Die()
    {
        isDead = true;


        GameObject effect = PhotonNetwork.Instantiate(deathEffect.name, transform.position, Quaternion.identity);
        PhotonNetwork.Destroy(effect);

        //WaveSpawner.EnemiesAlive--;

        PhotonNetwork.Destroy(gameObject);
        gameManager_Multi.EnemyCount--;
        gameManager_Multi.EnemyKillCount++;
        gameManager_Multi.Money += 5;
    }

}
