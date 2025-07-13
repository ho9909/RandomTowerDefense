using UnityEngine;
using Photon.Pun;
using System.Collections;

[RequireComponent(typeof(Enemy_Mulit))]
public class Movement_multi : MonoBehaviourPun
{
    private Transform target;
    private int wavepointIndex = 0;
    public bool cut = false;
    public int count = 0;
    private Enemy_Mulit enemy;


    void Start()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        enemy = GetComponent<Enemy_Mulit>();

        target = WayPoint.points[0].transform;

    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        if (!cut)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
            //transform.position = Vector3.MoveTowards(transform.position, target.position, (speed * Time.deltaTime));

            if (Vector3.Distance(transform.position, target.position) <= 0.2f)
            {
                GetNextWayPoint();
            }
        }

    }

    void GetNextWayPoint()
    {
        if(count >= 3)
        {
            EndPath();
            return;
        }
        if (wavepointIndex >= WayPoint.points.Length - 1)
        {
            wavepointIndex = 0;
            target = WayPoint.points[wavepointIndex];
            //wavepointIndex++;
            count++;
            return;
        }
        else
        {
            wavepointIndex++;
            target = WayPoint.points[wavepointIndex];

        }
    }
    void EndPath()
    {
        gameManager_Multi.TakeDamage(50);
        PhotonNetwork.Destroy(gameObject);
        count = 0;
        wavepointIndex = 0;
    }


}

