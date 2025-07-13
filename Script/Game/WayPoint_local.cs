using UnityEngine;
using Photon.Pun;

public class WayPoint_local : MonoBehaviourPun
{
    public static Transform[] points;
    public static bool check;

    void Awake()
    {
        if (!photonView.IsMine)
        {
            check = true;
            points = new Transform[transform.childCount];

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = transform.GetChild(i);

            }
        }
    }


}

