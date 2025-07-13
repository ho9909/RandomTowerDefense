
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private float dist;

    private bool dragging = false;
    public Transform[] card_object;
    private Vector3 offset;
    public Transform spawnOffset;
    private Transform toDrag;
    Touch touch;
    public bool isSell = false;
    //ObjectChanger objectChanger; //= new ObjectChanger();

    void Update()
    {
/*        if (HandCardDragHover.check == true) 
        {
            if (HandCardDragHover.cnt == 1)
            {
                objectChanger.ChangeObject();
            }
            if (HandCardDragHover.cnt == 2)
            {
                objectChanger.ChangeObject();
            }
            if (HandCardDragHover.cnt == 3)
            {
                objectChanger.ChangeObject();
            }
        }*/
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
                if (hit.collider.tag == "dog_card" || hit.collider.tag == "cat_card" || hit.collider.tag == "mouse_card")
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