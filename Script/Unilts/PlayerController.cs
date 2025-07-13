using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    void Start()
    {
        if (photonView.IsMine)
        {
            // 로컬 플레이어의 처리 (아래에 위치)
            Camera.main.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        else
        {
            // 원격 플레이어의 처리 (위에 위치)
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, -180);
        }
    }
}
