using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DogCard : MonoBehaviour
{
    private Enemy enemy;
    public bool check = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "dog_card")
        {
            enemy.crash = true;
        }
        else
        {
            enemy.crash = false;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Enemy"))
        {
            return;
        }
        else
        {
            enemy = other.GetComponent<Enemy>();
            enemy.speed = 0f;
        }
        
    }
    public void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Enemy"))
        {
            return;
        }
        else
        {
            enemy = other.GetComponent<Enemy>();
            enemy.speed = enemy.startSpeed;
        }

    }


    void Start()
    {
        // Coroutine�� �����մϴ�.
        StartCoroutine(DestroyAfterDelay(5f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        
        // ���� �ð��� ��ٸ� �Ŀ� ������Ʈ�� �ı��մϴ�.
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        check = true;
        //enemy.speed = enemy.startSpeed;
    }
}
