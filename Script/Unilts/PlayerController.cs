using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    void Start()
    {
        if (photonView.IsMine)
        {
            // ���� �÷��̾��� ó�� (�Ʒ��� ��ġ)
            Camera.main.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        else
        {
            // ���� �÷��̾��� ó�� (���� ��ġ)
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, -180);
        }
    }
}
