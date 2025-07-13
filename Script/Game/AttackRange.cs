using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackRange : MonoBehaviour
{
    private Transform target;
    public float range;
    public Vector3 position;
    public string enemyTag = "Enemy";
    private int time = 5;
    public float waitingtime;
    private void Start()
    {
        OnAttackRange();
    }
    private void Update()
    {
        if (Draggable.touch_check == false)
        {
            InvokeRepeating("UpdatingTarget", 0f, 0.25f);
        }
        if (Draggable.touch_check == false)
        {
            waitingtime += Time.deltaTime;
        }
        
        if(waitingtime >time)
        {
            Draggable.touch_check= false;
            OffAttackRange();
            waitingtime=0;
        }
    }
    public void OnAttackRange()
    {
            gameObject.SetActive(true);
            float diameter = range * 2.0f;
            transform.localScale = Vector3.one * diameter;

            transform.position = position;

    }

    void UpdatingTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in cols)
        {
            if (collider.tag == "Enemy")
            {
                stop(collider.transform);
            }
        }
    }

    void stop(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.crash = true;
        }
    }
    void again(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.crash = false;
        }
    }


    public void OffAttackRange()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in cols)
        {
            if (collider.tag == "Enemy")
            {
                again(collider.transform);
            } 
        }
        CancelInvoke("UpdatingTarget");
        Draggable.touch_check = false;
        gameObject.SetActive(false);
        gameObject.transform.position = position;
    }
}
