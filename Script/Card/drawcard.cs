using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class cardTransform
{
    public GameObject[] card = new GameObject[1];

}
public class drawcard : MonoBehaviour
{
    public GameObject panel;
    public cardTransform[] myTransform = new cardTransform[3];
    // Start is called before the first frame update
    public HandCard cardPrefab;
    public Transform handContent;
    /*private Player player;
    private PlayerInfo enemyInfo;
    private int cardCount = 0; // Amount of cards in hand*/
    private float[] rate = new float[3] { 0.34f, 0.33f, 0.33f };
    public int count = 0;
    private float total;
    private int randomNumber;
    private int randomcount;
    int CardChoose(float[] probs)
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
        foreach (var item in rate)
        {
            total += (float)item;
        }

    }
    public void OnClickButtonEvenet()
    {
        if (gameManager.Money < 5)
        {
            ToastMsg.Instrance.showMessage("골드가 없습니다!", 1.0f);
            return;
        }
        else
        {
            randomNumber = CardChoose(rate);
            gameManager_Multi.Money -= 5;
            count = randomNumber;
            GameObject cardObj = Instantiate(myTransform[randomNumber].card[0]);
            cardObj.transform.SetParent(handContent, false);
        }


    }
}
