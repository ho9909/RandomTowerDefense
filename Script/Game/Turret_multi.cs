using Photon.Pun;
using UnityEngine;
using System.Collections;

public enum TowerGrade_multi { E = 1, D = 2, C = 3, B = 4, A = 5, SSS = 6 };

[System.Serializable]
public class Datas
{
    public TowerGrade_multi towerGrade;
    public float range;
    public int weight;

    public Datas(Datas dataa)
    {
        this.weight = dataa.weight;
        this.range = dataa.range;
        this.towerGrade = dataa.towerGrade;
    }

}

public class Turret_multi : MonoBehaviour
{
    public TowerGrade_multi towerGrade;
    public float range;
    public static int weight;
    public static Datas rangee;
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
        //InvokeRepeating("UpdateTarget", 0f, 0.5f);
        switch (towerGrade)
        {
            case TowerGrade_multi.E:
                tower_count = 1;
                range = 15f;
                towerGrade = TowerGrade_multi.E;
                weight = 5;
                break;
            case TowerGrade_multi.D:
                tower_count = 2;
                range = 25f;
                towerGrade = TowerGrade_multi.D;
                weight = 10;
                break;
            case TowerGrade_multi.C:
                tower_count = 3;
                range = 35f;
                towerGrade = TowerGrade_multi.C;
                weight = 15;
                break;
            case TowerGrade_multi.B:
                tower_count = 4;
                range = 45f;
                towerGrade = TowerGrade_multi.B;
                weight = 20;
                break;
            case TowerGrade_multi.A:
                tower_count = 5;
                range = 55f;
                towerGrade = TowerGrade_multi.A;
                weight = 25;
                break;
            case TowerGrade_multi.SSS:
                tower_count = 6;
                range = 65f;
                towerGrade = TowerGrade_multi.SSS;
                weight = 30;
                break;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "cat_card")
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

        if (nearestEnemy != null && shortestDistance <= range)
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
        UpdateTarget();
        if (target == null)
        {
            return;
        }
        if(upgrade_check)
        {
            weight += 5;
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
    void LockOnTarget()
    {

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);


    }

    void Shoot()
    {

        GameObject bulletGO = PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position, firePoint.rotation);
        Bullet_multi bullet = bulletGO.GetComponent<Bullet_multi>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }


    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

    }


}
