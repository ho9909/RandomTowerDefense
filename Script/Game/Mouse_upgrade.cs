using UnityEngine;

public class Mouse_upgrade : MonoBehaviour
{
    GameObject[] tower;
    private int upgrade = 1;
    // Start is called before the first frame update
    public void OnClickEvent()
    {
        if (PlayerState.Money > upgrade)
        {
            PlayerState.Money -= upgrade;
            upgrade++;
            tower = GameObject.FindGameObjectsWithTag("mouse");
            for (int i = 0; i < tower.Length; i++)
            {
                tower[i].GetComponent<Turret>().upgrade_check = true;
            }
        }


    }
}
