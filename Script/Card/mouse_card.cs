using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_card : MonoBehaviour
{
    private Enemy ememy;

    // Start is called before the first frame update
    void Start()
    {
        // Coroutine�� �����մϴ�.
        StartCoroutine(DestroyAfterDelay(5f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        // ���� �ð��� ��ٸ� �Ŀ� ������Ʈ�� �ı��մϴ�.
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
