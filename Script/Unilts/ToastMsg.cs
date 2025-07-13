using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastMsg : MonoBehaviour
{
    private Text no_money;
    private float fadeInOutTime = 1f;
    private static ToastMsg instance = null;

    public static ToastMsg Instrance
    {
        get
        {
            if (null == instance) instance =  FindObjectOfType<ToastMsg>();
            return instance;
        }
    }
    private void Awake()
    {
        if(null == instance) instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        no_money = this.gameObject.GetComponent<Text>();
        no_money.enabled = false;
    }
    private IEnumerator fadeInOut(Text target, float durationTime, bool inOut)
    {
        float start, end;
        if (inOut)
        {
            start = 0.0f;
            end = 1.0f;
        }
        else
        {
            start = 1.0f;
            end = 0f;
        }

        Color current = Color.clear; /* (0, 0, 0, 0) = 검은색 글자, 투명도 100% */
        float elapsedTime = 0.0f;

        while (elapsedTime < durationTime)
        {
            float alpha = Mathf.Lerp(start, end, elapsedTime / durationTime);

            target.color = new Color(current.r, current.g, current.b, alpha);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator showMessageCoroutine(string msg, float durationTime)
    {
        Color originalColor = no_money.color;
        no_money.text = msg;
        no_money.enabled = true;

        yield return fadeInOut(no_money, fadeInOutTime, true);

        float elapsedTime = 0.0f;
        while (elapsedTime < durationTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return fadeInOut(no_money, fadeInOutTime, false);

        no_money.enabled = false;
        no_money.color = originalColor;
    }

    public void showMessage(string msg, float durationTime)
    {
        StartCoroutine(showMessageCoroutine(msg, durationTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
