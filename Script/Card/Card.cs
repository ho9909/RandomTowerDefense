using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;


public class Card : MonoBehaviour
{

    [Header("Sprite")]
    public Image image;

    [Header("Front & Back")]
    public Image cardfront;

    [Header("Properties")]
    public Text cardName;
    public Text cost;
    public Text strength;
    public Text health;
    public Text description;
    public Text creatureType;

    [Header("Card Drag & Hover")]
    public HandCardDragHover cardDragHover;

    [Header("Outline")]
    public Image cardOutline;
    public Color readyColor;
    [HideInInspector] public int handIndex;

    // Called from PlayerHand to instantiate the cards in the player's hand
    public void AddCard(CardInfo newCard, int index)
    {
        handIndex = index;

        // Enable hover on player cards. We disable it for enemy cards.
        cardDragHover.canHover = true;
        cardOutline.gameObject.SetActive(true);

        // Reveal card FRONT, hide card BACK
        cardfront.color = Color.white;

        // Set card image
        image.sprite = newCard.image;

        // Assign description, name and remaining stats
        description.text = newCard.description; // Description
        cost.text = newCard.cost; // Cost
        cardName.text = newCard.name;

        // Only set Health & Strength if CreatureCard
        if (newCard.data is CreatureCard)
        {
            health.text = ((CreatureCard)newCard.data).health.ToString();
            strength.text = ((CreatureCard)newCard.data).strength.ToString();
        }
    }

    public void AddCardBack()
    {
        cardfront.color = Color.clear;
    }

    // Clears the card. Called when we Play/remove a card.
    public void RemoveCard()
    {
        Destroy(gameObject);
    }

    public void UpdateFieldCardInfo(CardInfo card)
    {
        // Reveal card FRONT, hide card BACK
        cardfront.color = Color.white;

        // Set card image
        image.sprite = card.image;

        // Assign description, name and remaining stats
        description.text = card.description; // Description
        cost.text = card.cost; // Cost
        cardName.text = card.name;

        // Stats
        health.text = ((CreatureCard)card.data).health.ToString();
        strength.text = ((CreatureCard)card.data).strength.ToString();
    }
}

