using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


[System.Serializable]
public class turretArray
{
    public GameObject[] turret = new GameObject[6];
    
}

public class Gacha : MonoBehaviour
{
    public turretArray[] myTransform = new turretArray[3];
    private Transform set;
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
        if (gameManager.Money < 5)
        {
            Debug.Log("골드가 없습니다.");
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
                        Debug.Log("You can't build it");
                        break;
                    }
                    set = spawnTurret.node_info[location];
                    continue;
                }
                else
                {
                    gameManager.Money -= 5;
                    randomcount = Random.Range(0, 3);
                    Instantiate(myTransform[randomcount].turret[randomNumber], (set.position), Quaternion.Euler(0, 90, 0));
                    break;
                }
            }
        }
       

    }

}
