using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MulitLobbyJoin : MonoBehaviour
{
    public SceneFader fader;
    // Start is called before the first frame update
    public void Select()
    {
        fader.FadeTo("Lobby");
    }
}
