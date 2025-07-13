using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandCardDragHover : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Card Properties")]
    public HandCard card;
    public CanvasGroup canvasGroup;

    [Header("Card Hover")]
    public bool canHover = false; // Hover and Drag are false by default and set to true when the card is instantiated.

    [Header("Card Drag")]
    public bool canDrag = false;
    Transform parentReturnTo = null; // Return to hand canvas
    public GameObject EmptyCard; // Used for creating an empty placeholder card where our current card used to be.
    private GameObject temp;
    Touch touch;
    private Vector3 lastTouchPosition;
    GameObject obj;
    private float dist;

    private bool dragging = false;

    private Vector3 offset;

    private Transform toDrag;
    Vector3 v3, v4;

    void Update()
    {
        card = GetComponent<HandCard>();

        if (Input.touchCount != 1)
        {
            dragging = false;
            return;
        }


        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;
        if (touch.phase == TouchPhase.Began)
        {
            dragging = true;
            Vector2 touchEndPos = Camera.main.ScreenToWorldPoint(touch.position);
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
            Vector2 touchEndPos = Camera.main.ScreenToWorldPoint(touch.position);
            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            v4 = Camera.main.ScreenToWorldPoint(v3);
            //toDrag.position = v4 + offset;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // If we can't hover, return.
        if (!canHover || card == null) return;
        // Move card locally
        card.transform.localScale = new Vector2(0.5f, 0.5f);
        card.transform.localPosition = new Vector2(card.transform.localPosition.x, 190);
        int index = card.transform.GetSiblingIndex();
/*
        // Move corresponding card on opponent's screen
        Player.gameManager.isHovering = true;
        Player.gameManager.CmdOnCardHover(-25, index);*/
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!canHover) return;

        // Return to normal
        card.transform.localScale = new Vector2(0.1f, 0.1f);
        card.transform.localPosition = new Vector2(card.transform.localPosition.x, 0);
        int index = card.handIndex;
/*
        // Move corresponding card back to normal on opponent's screen
        Player.gameManager.CmdOnCardHover(0, index);
        Player.gameManager.isHovering = false;*/
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // If we can't drag, return.
        if (!canDrag) return;
        temp = Instantiate(EmptyCard);
        temp.transform.SetParent(this.transform.parent, false);
        temp.transform.SetSiblingIndex(transform.GetSiblingIndex());

        parentReturnTo = this.transform.parent;
        transform.SetParent(this.transform.parent.parent, false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // If we can't drag, return.
        if (!canDrag) return;
        Vector3 screenPoint = eventData.position;
        screenPoint.z = 10.0f; //distance of the plane from the camera
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canDrag) return;

        transform.SetParent(parentReturnTo, false);
        transform.SetSiblingIndex(temp.transform.GetSiblingIndex());
        canvasGroup.blocksRaycasts = false;
        obj = GameObject.Find("GameMaster");
        v4 = new Vector3(v4.x / 9, 0.63f, v4.z / 16);
        //obj.GetComponent<spawn>().spawnarea(v4);

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            v4.x = hit.point.x;
            v4.y = 0.63f;
            v4.z = hit.point.z;
            obj.GetComponent<spawn>().spawnarea(v4);
        }

        Destroy(temp);
        dragging = false;

        //Destroy(EmptyCard);

    }
}

