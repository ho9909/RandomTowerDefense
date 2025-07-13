using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;

public class countdown_multi : MonoBehaviourPun
{
    public static bool count = false;

    public Text timerText; // Unity UI Text ��Ҹ� ������ ����

    private float timer = 30.0f;
    private bool countingDown = true;

    void Update()
    {
        if (countingDown)
        {
            timer -= Time.deltaTime;

            if (timer <= 0.0f)
            {
                count = true;
                timer = 30.0f; // 0�ʰ� �Ǹ� Ÿ�̸Ӹ� �ٽ� 30�ʷ� ����
            }
        }

        UpdateTimerText(); // Ÿ�̸� �ؽ�Ʈ ������Ʈ
    }

    void UpdateTimerText()
    {
        // UI Text�� Ÿ�̸� ������ ǥ��
        if (timerText != null)
        {
            timerText.text = "Time: " + timer.ToString("F2") + "s";
        }
    }
}
