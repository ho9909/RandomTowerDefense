using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class circleTransform
{
    public GameObject[] card = new GameObject[1];

}
public class spawn : MonoBehaviour
{
    public circleTransform[] myTransform = new circleTransform[3];

    public void spawnarea(Vector3 v4)
    {
        GameObject cur = GameObject.Find("PlayerHand");
        GameObject circleobj = Instantiate(myTransform[cur.GetComponent<drawcard>().count].card[0], v4, Quaternion.Euler(90, 0, 0));
        myTransform[cur.GetComponent<drawcard>().count].card[0].SetActive(true);
    }
}
