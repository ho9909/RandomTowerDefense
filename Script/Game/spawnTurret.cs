using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTurret : MonoBehaviour
{
    public static Transform[] node_info;
    private GameObject[] turret;
    private GameObject[] turret1;
    private GameObject[] turret2;
    Gacha spawn;
    private void Start()
    {
        node_info = new Transform[transform.childCount];
        for (int i = 0; i < node_info.Length; i++)
        {
            node_info[i] = transform.GetChild(i);

        }
        spawn = GameObject.FindWithTag("fuck").GetComponent<Gacha>();

    }
    void create()
    {
        int cnt = 0;
        turret = GameObject.FindGameObjectsWithTag("cat");
        turret1 = GameObject.FindGameObjectsWithTag("dog");
        turret2 = GameObject.FindGameObjectsWithTag("mouse");
        var lista = new List<GameObject>();
        lista.AddRange(turret);
        lista.AddRange(turret1);
        lista.AddRange(turret2);
        while (spawn != null && cnt  == 0)
        {
            cnt = 0;
            for (int i = 0; i < spawn.check_turret.Length; i++)
            {
                for (int j = 0; j < lista.Count; j++)
                {
                    if (node_info[i].position.x - 2 <= lista[j].gameObject.transform.position.x && node_info[i].position.x + 2 >= lista[j].gameObject.transform.position.x
                        && node_info[i].position.z - 2 <= lista[j].gameObject.transform.position.z && node_info[i].position.z + 2 >= lista[j].gameObject.transform.position.z)
                    {
                        spawn.check_turret[i] = true;
                        break;
                    }
                    else if (!(node_info[i].position.x - 2 <= lista[j].gameObject.transform.position.x) || !(node_info[i].position.x + 2 >= lista[j].gameObject.transform.position.x)
                        || !(node_info[i].position.z - 2 <= lista[j].gameObject.transform.position.z) || !(node_info[i].position.z + 2 >= lista[j].gameObject.transform.position.z))
                    {
                        spawn.check_turret[i] = false;
                    }
                }
            }
            cnt += 1;

            
        }
    }
    private void Update()
    {
        create();

    }
}
