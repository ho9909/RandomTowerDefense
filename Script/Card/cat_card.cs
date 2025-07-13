using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat_card : MonoBehaviour
{
    private Turret turret;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("cat") || !other.CompareTag("dog") || !other.CompareTag("mouse"))
        {
            return;
        }
        turret = other.GetComponent<Turret>();
        turret.fireRate += 5f;
        turret.tower_damage += 10;
    }
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
