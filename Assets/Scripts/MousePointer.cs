using UnityEngine;
using System.Collections;

public class MousePointer : MonoBehaviour
{
    private const float verticalCorrection = 4f;

    public GameObject pointerPrefab;
    private bool draw = true;

    private Vector3 yOffset = new Vector3(0f, 0.01f, 0f);
    private Quaternion rotOffset = Quaternion.Euler(90f, 0f, 0f);

    private GameObject pointerInstance;

    void Update()
    {
        UpdateVirtualCursor();

        if (draw)
        {
            RaycastHit hitInfo;
            Ray ray = GetComponent<Camera>().ScreenPointToRay(GameInfo.gi.virtualCursorPos);
            if (Physics.Raycast(ray, out hitInfo, 1000f, GameInfo.gi.teleportLayers))
            {
                if (pointerInstance != null)
                {
                    pointerInstance.transform.position = hitInfo.point + yOffset;
                }
                else
                {
                    pointerInstance = (GameObject)GameObject.Instantiate(pointerPrefab, hitInfo.point + yOffset, rotOffset);
                }
            }
            else
            {
                if (pointerInstance != null)
                {
                    GameObject.Destroy(pointerInstance);
                    pointerInstance = null;
                }
            }
        }
    }

    private void UpdateVirtualCursor()
    {
        GameInfo.gi.virtualCursorPos += new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * GameInfo.gi.sensitivity;
        if (GameInfo.gi.virtualCursorPos.y > Screen.height)
            GameInfo.gi.virtualCursorPos.y = Screen.height;
        if (GameInfo.gi.virtualCursorPos.y < 0f)
            GameInfo.gi.virtualCursorPos.y = 0f;
    }
}
