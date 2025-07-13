using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;

public class Bullet_multi : MonoBehaviourPun
{
    private Transform target;

    public float speed = 70f;

    public int damage = 100;

    public float explosionRadius = 0f;
    public GameObject impactEffect;

    public void Start()
    {
        damage += Turret_multi.weight;
    }
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy_Mulit e = enemy.GetComponent<Enemy_Mulit>();
        GameObject[] turret = GameObject.FindGameObjectsWithTag("cat");
        GameObject[] turret1 = GameObject.FindGameObjectsWithTag("dog");
        GameObject[] turret2 = GameObject.FindGameObjectsWithTag("mouse");
        //cat
        var list = new List<GameObject>();
        list.AddRange(turret);
        list.AddRange(turret1);
        list.AddRange(turret2);
        for (int i= 0; i<list.Count; i++)
        {
            if (e != null && list[i].GetComponent<Turret_multi>().position_check == true)
            {
                if (e != null && list[i].GetComponent<Turret_multi>().upgrade_check == true)
                {
                    damage += 1;
                    list[i].GetComponent<Turret_multi>().upgrade_check = false;
                }
                damage += (int)(damage * 0.25);
                e.TakeDamage(damage);

            }
            else if((e != null && list[i].GetComponent<Turret_multi>().position_check == false)){
                if(list[i].GetComponent<Turret_multi>().upgrade_check == true)
                {
                    damage += 1;
                    list[i].GetComponent<Turret_multi>().upgrade_check = false;

                }
                e.TakeDamage(damage);
            }
            else if ((e != null && list[i].GetComponent<Turret_multi>().position_check == false))
            {
                if (list[i].GetComponent<Turret_multi>().upgrade_check == false)
                {
                    e.TakeDamage(damage);

                }
                
            }

        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

