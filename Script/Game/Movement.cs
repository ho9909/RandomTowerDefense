using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(Enemy))]
public class Movement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    public bool cut = false;
    public int count = 0;
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();

        target = WayPoint.points[0];
    }

    private void Update()
    {
        if(!cut)
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
        if (wavepointIndex >= WayPoint.points.Length - 1)
        {
            wavepointIndex = 0;
            target = WayPoint.points[wavepointIndex];
            //wavepointIndex++;
            count++;
        }
        else
        {
            wavepointIndex++;
            target = WayPoint.points[wavepointIndex];
            
        }
    }


}

