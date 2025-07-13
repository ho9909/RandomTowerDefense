using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class DragController_multi : MonoBehaviourPun
{
    private float dist;

    private bool dragging = true;

    private Vector3 offset;

    private Transform toDrag;
    Touch touch;
    public bool isSell = false;

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        Vector3 v3, v4;
/*        if (Input.touchCount != 1)
        {
            dragging = false;
            return;
        }*/


        //Touch touch = Input.touches[0];
        Vector3 pos = Input.mousePosition;

        if (/*(touch.phase == TouchPhase.Began)*/Input.GetMouseButtonDown(0))
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

        if (dragging && /*touch.phase == TouchPhase.Moved*/ Input.GetMouseButton(0))
        {
            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            v4 = Camera.main.ScreenToWorldPoint(v3);
            toDrag.position = v4 + offset;
        }

        if (dragging && (/*(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)*/Input.GetMouseButtonUp(0)))
        {
            dragging = false;
            Turret_multi turret = gameObject.GetComponent<Turret_multi>();
            if (isSell)
            {
                if (turret.tower_count == 1)
                {
                    gameManager_Multi.Money += 3;
                }
                if (turret.tower_count == 2)
                {
                    gameManager_Multi.Money += 5;
                }
                if (turret.tower_count == 3)
                {
                    gameManager_Multi.Money += 7;
                }
                if (turret.tower_count == 4)
                {
                    gameManager_Multi.Money += 9;
                }
                if (turret.tower_count == 5)
                {
                    gameManager_Multi.Money += 15;
                }
                if (turret.tower_count == 6)
                {
                    gameManager_Multi.Money += 30;
                }

                PhotonNetwork.Destroy(gameObject);
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