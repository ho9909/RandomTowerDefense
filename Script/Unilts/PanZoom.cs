using UnityEngine;

public class PanZoom : MonoBehaviour
{
    public float moveSpped;
    public Transform cam;
    public static bool drag_check = false;

    Vector2 prevPos = Vector2.zero;
    float prevDistance = 0f;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    public void OnDrag()
    {
        int touchCount = Input.touchCount;

        if (touchCount == 1 && drag_check == false)
        {
            if (prevPos == Vector2.zero)
            {
                prevPos = Input.GetTouch(0).position;
                return;
            }
            Vector2 dir = (Input.GetTouch(0).position - prevPos).normalized;
            Vector3 vec = new Vector3(dir.x, 0, dir.y);

            cam.position -= vec * moveSpped * Time.deltaTime;
            prevPos = Input.GetTouch(0).position;
        }

        else if (touchCount == 2)
        {
            if (prevDistance == 0)
            {
                prevDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                return;
            }
            float curDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            float move = prevDistance - curDistance;

            Vector3 pos = cam.position;

            if (move < 0)
            {
                pos.y -= moveSpped * Time.deltaTime;
            }
            else if (move > 0)
            {
                pos.y += moveSpped * Time.deltaTime;
            }

            cam.position = pos;
            prevDistance = curDistance;
        }
    }
    public void ExitDrag()
    {
        prevPos = Vector2.zero;
        prevDistance = 0;
    }
}
