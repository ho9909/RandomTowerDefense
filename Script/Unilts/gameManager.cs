using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{

    [Header("Hand")]
    public int handSize = 7;
    //public PlayerHand playerHand;

    [Header("Deck")]
    public int deckSize = 1000; // Maximum deck size
    public int identicalCardCount = 3; // How many identical cards we allow to have in a deck


    private float timer = 0f;
    private float targetTime = 30f;

    public bool next = false;
    // isHovering is only set to true on the Client that called the OnCardHover function.
    // We only want the hovering to appear on the enemy's Client, so we must exclude the OnCardHover caller from the Rpc call.
/*    [HideInInspector] public bool isHovering = false;
    [HideInInspector] public bool isSpawning = false;*/

    //public SyncListPlayerInfo players = new SyncListPlayerInfo(); // Information of all players online. One is player, other is opponent.

    public static int Money;
    public int startMoney = 15;
    public static int EnemyKillCount = 0;
    public static int EnemyCount = 0;
    public Text gold;
    public static int Lives;
    public int startLives = 10000;
    public static int Rounds;


    public void Start()
    {
        Money = startMoney;
        Lives = startLives;
        EnemyKillCount = 0;
        EnemyCount = 0;
        gold.text = Money.ToString();

        Rounds = 0;
    }


    private void Update()
    {
        if (EnemyKillCount == 10)
        {
            Money += 5;
            EnemyKillCount = 0;
        }
        if (EnemyCount == 150)
        {
            Lives -= 10;
        }
        gold.text = Money.ToString();

        timer += Time.deltaTime;
        if(timer > targetTime)
        {
            next = true;
            timer = 0f;
        }
    }

    void check()
    {
        
    }


}
