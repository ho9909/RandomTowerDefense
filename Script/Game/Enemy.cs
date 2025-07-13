using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Movement))]
public class Enemy : MonoBehaviour
{
    private Movement m_Movement;
    public float startSpeed = 0f;
    [HideInInspector]
    public float speed;
    public bool posion;
    public float startHealth = 100;
    private float health;

    public int worth = 50;
    public bool crash = false;

    //public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    void Start()
    {
        speed = startSpeed; 
        health = startHealth;
        m_Movement = GetComponent<Movement>();
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
            gameManager.Lives -= 30;
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


        //GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);

        //WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
        gameManager.EnemyCount--;
        gameManager.EnemyKillCount++;
        gameManager.Money += 1;
    }

}
