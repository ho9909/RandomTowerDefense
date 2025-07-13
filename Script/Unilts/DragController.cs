using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragController : MonoBehaviour
{
    private float dist;

    private bool dragging = false;

    private Vector3 offset;

    private Transform toDrag;
    Touch touch;
    public bool isSell = false;

    void Update()
    {
        Vector3 v3, v4;
        if (Input.touchCount != 1)
        {
            dragging = false;
            return;
        }


        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "dog" || hit.collider.tag == "cat" || hit.collider.tag == "mouse")
                {
                    toDrag = hit.transform;
                    dist = hit.transform.position.z - Camera.main.transform.position.z;
                    v3 = new Vector3(pos.x, pos.y, dist);
                    v4 = Camera.main.ScreenToWorldPoint(v3);
                    offset = toDrag.position - v4;
                    dragging = true;
                }
            }
        }

        if (dragging && touch.phase == TouchPhase.Moved)
        {
            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            v4 = Camera.main.ScreenToWorldPoint(v3);
            toDrag.position = v4 + offset;
        }

        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            dragging = false;
            Turret turret = gameObject.GetComponent<Turret>();
            if (isSell)
            {
/*                if (turret.range.towerGrade == TowerGrade.E)
                {
                    PlayerState.Money += 3;
                }
                if (turret.range.towerGrade == TowerGrade.D)
                {
                    PlayerState.Money += 5;
                }
                if (turret.range.towerGrade == TowerGrade.C)
                {
                    PlayerState.Money += 7;
                }
                if (turret.range.towerGrade == TowerGrade.B)
                {
                    PlayerState.Money += 9;
                }
                if (turret.range.towerGrade == TowerGrade.A)
                {
                    PlayerState.Money += 15;
                }
                if (turret.range.towerGrade == TowerGrade.SSS)
                {
                    PlayerState.Money += 30;
                }
*/
                Destroy(gameObject);
            }
        }



    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "sell")
        {
            isSell = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "sell")
        {
            isSell = false;
        }
    }

}