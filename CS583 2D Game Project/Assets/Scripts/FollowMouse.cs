using UnityEngine;

// this script runs the functionality of the "spotlight"
public class FollowMouse : MonoBehaviour
{

    // has mask/spotlight follow the position of the mouse
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;
    }
}
