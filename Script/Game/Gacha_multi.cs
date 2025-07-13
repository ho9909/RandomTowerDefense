using Photon.Pun;
using UnityEngine;



[System.Serializable]
public class turretArray_mulit
{
    public GameObject[] turret = new GameObject[6];
    
}

public class Gacha_multi : MonoBehaviourPunCallbacks
{
    public turretArray_mulit[] myTransform = new turretArray_mulit[3];
    private Transform set;
    private GameObject temp;
    public bool[] check_turret = new bool[9] {true, true, true, true, true, true, true, true, true};
    private float[] rate = new float[6] {0.5002f, 0.331f,  0.102f, 0.061f, 0.005f, 0.0008f};
    private float total;
    private int randomNumber;
    private int randomcount;

    int Choose(float[] probs)
    {

        total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }
    private void Start()
    {
        foreach(var item in rate)
        {
            total += (float)item;
        }

    }

    public void OnClickButtonEvenet()
    {
        if(gameManager_Multi.start == true)
        {
            if (gameManager_Multi.Money < 5)
            {
                ToastMsg.Instrance.showMessage("골드가 없습니다!", 1.0f);
                return;
            }
            else
            {

                randomNumber = Choose(rate);
                int location = 0;
                set = spawnTurret.node_info[location];
                for (int i = 0; i < spawnTurret.node_info.Length; i++)
                {
                    if (check_turret[i] == true)
                    {
                        location++;
                        if (location == 9)
                        {
                            ToastMsg.Instrance.showMessage("대기석을 비워주세요", 1.0f);
                            break;
                        }
                        set = spawnTurret.node_info[location];
                        continue;
                    }
                    else
                    {
                        gameManager_Multi.Money -= 5;
                        randomcount = Random.Range(0, 3);
                        temp = myTransform[randomcount].turret[randomNumber];
                        turretspawn(myTransform[randomcount].turret[randomNumber]);


                        break;
                    }
                }
            }
        } 
    }

    public void turretspawn(GameObject cur)
    {
        PhotonNetwork.Instantiate(cur.name, set.position, Quaternion.identity);
    }

}
