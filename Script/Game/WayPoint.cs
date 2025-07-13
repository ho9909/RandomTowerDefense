using UnityEngine;
using Photon.Pun;

public class WayPoint : MonoBehaviourPun
{
    public static Transform[] points;
    public static Transform[] local_points;


    void Awake()
    {
        if(photonView.IsMine) {
            points = new Transform[transform.childCount];

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = transform.GetChild(i);

            }
        }

    }


}

