using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Buffer : MonoBehaviour
{
    private Transform target;
    public float range;
    public Vector3 position;
    private int time = 5;
    public float waitingtime;
    private void Awake()
    {
        OffAttackRange();
    }
    private void Update()
    {
        if (Draggable.touch_check)
        {
            InvokeRepeating("SearchingTurret", 0f, 0.25f);
        }
        if (Draggable.touch_check)
        {
            waitingtime += Time.deltaTime;
        }

        if (waitingtime > time)
        {
            Draggable.touch_check = false;
            OffAttackRange();
            waitingtime = 0;
        }
    }
    public void OnAttackRange()
    {
        gameObject.SetActive(true);
        float diameter = range * 2.0f;
        transform.localScale = Vector3.one * diameter;

        transform.position = position;





    }

    void SearchingTurret()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in cols)
        {
            if (collider.tag == "cat" || collider.tag  == "dog" || collider.tag == "mouse")
            {
                buff(collider.transform);
            }
        }
    }

    void buff(Transform turret)
    {
        Turret e = turret.GetComponent<Turret>();
        if (e != null)
        {
            e.buff = true;
        }
    }
    void stop(Transform turret)
    {
        Turret e = turret.GetComponent<Turret>();
        if (e != null)
        {
            e.buff = false;
        }
    }


    public void OffAttackRange()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in cols)
        {
            if (collider.tag == "cat" || collider.tag == "dog" || collider.tag == "mouse")
            {
                stop(collider.transform);
            }
        }
        CancelInvoke("SearchingTurret");
        Draggable.touch_check = false;
        gameObject.SetActive(false);
        gameObject.transform.position = position;
    }
}
