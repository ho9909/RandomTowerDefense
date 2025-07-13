// Put all our items in the Resources folder. We use Resources.LoadAll down
// below to load our items into a cache so we can easily reference them.
using UnityEngine;
using System.Collections.Generic;

public enum CreatureType : byte { BEAST, DRAGON, ALL }

// Struct for cards in your deck. Card + amount (ex : Sinister Strike x3). Used for Deck Building. Probably won't use it, just add amount to Card struct instead.
[CreateAssetMenu(menuName = "Card/Creature Card", order = 111)]
public partial class CreatureCard : ScriptableCard
{
    [Header("Stats")]
    public int strength;
    public int health;

    [Header("Targets")]

    [Header("Type")]
    public List<CreatureType> creatureType;

    [Header("Specialities")]
    public bool hasCharge = false;
    public bool hasTaunt = false;

    [Header("Death Abilities")]
    public List<CardAbility> deathcrys = new List<CardAbility>();
    [HideInInspector] public bool hasDeathCry = false; // If our card has a DEATHCRY ability


    private void OnValidate()
    {
        if (deathcrys.Count > 0) hasDeathCry = true;

    }
}