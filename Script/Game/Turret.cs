using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public enum TowerGrade { E = 1, D = 2, C = 3, B = 4, A = 5, SSS = 6 }

[System.Serializable]
public class Data
{
    public TowerGrade towerGrade;
    public float range;
    public double weight;

    public Data(Data data)
    {
        this.weight = data.weight;
        this.range = data.range;
        this.towerGrade = data.towerGrade;
    }

}

public class Turret : MonoBehaviour
{
    public static Data range;
    private Transform target;
    private Enemy targetEnemy;
    public int tower_damage = 0;
    public int tower_count = 0;
    public bool upgrade_check = false;
    public bool position_check = false;
    [Header("General")]

    //public float range = 15f;

    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate;
    public float firstRate;
    private float fireCountdown = 0f;

    public bool buff = false;
    public Transform firePoint;

    // Use this for initialization
    void Start()
    {
        fireRate = firstRate;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        switch (range.towerGrade)
        {
            case TowerGrade.E:
                tower_count = 1;
                break;
            case TowerGrade.D:
                tower_count = 2;
                break;
            case TowerGrade.C:
                tower_count = 3;
                break;
            case TowerGrade.B:
                tower_count = 4;
                break;
            case TowerGrade.A:
                tower_count = 5;
                break;
            case TowerGrade.SSS:
                tower_count = 6;
                break;
            default:
                tower_count = 0; // You can set a default value or handle it according to your requirements
                break;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "dog_card")
        {
            buff = true;
            Invoke("Removebuff", 3f);
        }
    }
    void Removebuff()
    {
        buff = false;
    }


    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range.range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        LockOnTarget();

        if (buff)
        {
            fireRate = firstRate * 1.4f;

        }
        else
        {
            fireRate = firstRate;
        }
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;



    }

    void Shoot()
    {

        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet_multi bullet = bulletGO.GetComponent<Bullet_multi>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }


    }

    void LockOnTarget()
    {

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);


    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range.range);

    }


}
