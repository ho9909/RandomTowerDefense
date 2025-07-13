using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

[System.Serializable]
public class circleTransform_mulit
{
    public GameObject[] card = new GameObject[1];

}
public class spawn_multi : MonoBehaviour
{
    public circleTransform_mulit[] myTransform = new circleTransform_mulit[3];

    public void spawnarea(Vector3 v4)
    {
        GameObject cur = GameObject.Find("PlayerHand");
        GameObject circleobj = PhotonNetwork.Instantiate(myTransform[cur.GetComponent<drawcard>().count].card[0].name, v4, Quaternion.Euler(90, 0, 0));
        myTransform[cur.GetComponent<drawcard>().count].card[0].SetActive(true);
    }
}
