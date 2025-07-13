using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private float dist;
    public GameObject sprite;
    private bool dragging = false;

    private Vector3 offset;

    private Transform toDrag;
    public static bool touch_check = false;
    private float touchTime;

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
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "dog_card" || hit.collider.tag == "cat_card" || hit.collider.tag == "mouse_card")
                {
                    PanZoom.drag_check = true;
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
            touch_check = true;
            PanZoom.drag_check = false;
            dragging = false;
        }

    }




}
