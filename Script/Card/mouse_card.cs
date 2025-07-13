using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_card : MonoBehaviour
{
    private Enemy ememy;

    // Start is called before the first frame update
    void Start()
    {
        // Coroutine을 시작합니다.
        StartCoroutine(DestroyAfterDelay(5f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        // 일정 시간을 기다린 후에 오브젝트를 파괴합니다.
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
