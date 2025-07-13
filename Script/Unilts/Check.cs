using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Check : MonoBehaviour
{
    public static Transform[] spawn_position;

    private void Awake()
    {
        spawn_position = new Transform[transform.childCount];

        for (int i = 0; i < spawn_position.Length; i++)
        {
            spawn_position[i] = transform.GetChild(i);
        }
    }

    void Update()
    {
        GameObject[] turret = GameObject.FindGameObjectsWithTag("cat");
        GameObject[] turret1 = GameObject.FindGameObjectsWithTag("dog");
        GameObject[] turret2 = GameObject.FindGameObjectsWithTag("mouse");
        GameObject spawn = GameObject.FindGameObjectWithTag("GameMaster");
        var list = new List<GameObject>();
        list.AddRange(turret);
        list.AddRange(turret1);
        list.AddRange(turret2);
        while(spawn != null)
        {
            for (int i = 0; i < spawn.GetComponent<Gacha>().check_turret.Length; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (spawn_position[i].position.x - 2 <= list[j].gameObject.transform.position.x && spawn_position[i].position.x + 2 >= list[j].gameObject.transform.position.x
                        && spawn_position[i].position.z - 2 <= list[j].gameObject.transform.position.z && spawn_position[i].position.z + 2 >= list[j].gameObject.transform.position.z)
                    {
                        spawn.GetComponent<Gacha>().check_turret[i] = true;
                        break;
                    }
                    else if (!(spawn_position[i].position.x - 2 <= list[j].gameObject.transform.position.x) || !(spawn_position[i].position.x + 2 >= list[j].gameObject.transform.position.x)
                        || !(spawn_position[i].position.z - 2 <= list[j].gameObject.transform.position.z) || !(spawn_position[i].position.z + 2 >= list[j].gameObject.transform.position.z))
                    {
                        spawn.GetComponent<Gacha>().check_turret[i] = false;
                    }
                }

            }
        }


    }
}
