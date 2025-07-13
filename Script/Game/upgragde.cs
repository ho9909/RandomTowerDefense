using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgragde : MonoBehaviour
{
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {

        pos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] turret = GameObject.FindGameObjectsWithTag("cat");
        GameObject[] turret1 = GameObject.FindGameObjectsWithTag("dog");
        GameObject[] turret2 = GameObject.FindGameObjectsWithTag("mouse");

        for (int i = 0; i < turret.Length; i++)
        {
            if (pos.x - 2 <= turret[i].transform.position.x && pos.x + 2 >= turret[i].transform.position.x && pos.z - 2 <= turret[i].transform.position.z && pos.z + 2 >= turret[i].transform.position.z)
            {
                turret[i].GetComponent<Turret>().position_check = true;
            }

        }
        for (int i = 0; i < turret1.Length; i++)
        {
            if (pos.x - 2 <= turret1[i].transform.position.x && pos.x + 2 >= turret1[i].transform.position.x && pos.z - 2 <= turret1[i].transform.position.z && pos.z + 2 >= turret1[i].transform.position.z)
            {
                turret1[i].GetComponent<Turret>().position_check = true;
            }

        }
        for (int i = 0; i < turret2.Length; i++)
        {
            if (pos.x - 2 <= turret2[i].transform.position.x && pos.x + 2 >= turret2[i].transform.position.x && pos.z - 2 <= turret2[i].transform.position.z && pos.z + 2 >= turret2[i].transform.position.z)
            {
                turret2[i].GetComponent<Turret>().position_check = true;
            }

        }
    }
}
