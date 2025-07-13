using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;

public class countdown_multi : MonoBehaviourPun
{
    public static bool count = false;

    public Text timerText; // Unity UI Text 요소를 저장할 변수

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
                timer = 30.0f; // 0초가 되면 타이머를 다시 30초로 설정
            }
        }

        UpdateTimerText(); // 타이머 텍스트 업데이트
    }

    void UpdateTimerText()
    {
        // UI Text에 타이머 정보를 표시
        if (timerText != null)
        {
            timerText.text = "Time: " + timer.ToString("F2") + "s";
        }
    }
}
