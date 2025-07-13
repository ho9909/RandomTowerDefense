using UnityEngine;


public class PlayerHand : MonoBehaviour
{
    public GameObject panel;
    public HandCard cardPrefab;
    public Transform handContent;

    //private int cardCount = 0; // Amount of cards in hand

    void Update()
    {
    }

    public void RemoveCard(int index)
    {
        for (int i = index; i < handContent.childCount; ++i)
        {
            HandCard slot = handContent.GetChild(i).GetComponent<HandCard>();
            int count = i;
            if (count == index) slot.RemoveCard();
            else if (slot.handIndex > index) slot.handIndex--;
        }
    }

}