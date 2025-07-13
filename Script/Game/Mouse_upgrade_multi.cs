using UnityEngine;

public class Mouse_upgrade_multi : MonoBehaviour
{
    GameObject[] tower;
    private int upgrade = 1;
    // Start is called before the first frame update
    public void OnClickEvent()
    {
        if (gameManager_Multi.Money > upgrade)
        {
            gameManager_Multi.Money -= upgrade;
            upgrade++;
            tower = GameObject.FindGameObjectsWithTag("mouse");
            for (int i = 0; i < tower.Length; i++)
            {
                tower[i].GetComponent<Turret_multi>().upgrade_check = true;
            }
        }
        else
        {
            ToastMsg.Instrance.showMessage("골드가 없습니다!", 1.0f);
        }

    }
}
