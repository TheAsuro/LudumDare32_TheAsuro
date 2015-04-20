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

    public static bool cursorActive = false;
    public static Vector3 cursorPosition;

    void Update()
    {
        if (draw)
        {
            RaycastHit hitInfo;
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, 1000f, GameInfo.gi.teleportLayers))
            {
                Vector3 newCursorPos = CalculateCursorPosition(hitInfo.point);

                if (pointerInstance != null)
                {
                    pointerInstance.transform.position = newCursorPos + yOffset;
                }
                else
                {
                    pointerInstance = (GameObject)GameObject.Instantiate(pointerPrefab, newCursorPos + yOffset, rotOffset);
                }

                cursorActive = true;
                cursorPosition = newCursorPos;
            }
            else
            {
                if (pointerInstance != null)
                {
                    GameObject.Destroy(pointerInstance);
                    pointerInstance = null;
                }

                cursorActive = false;
            }
        }
    }

    private Vector3 CalculateCursorPosition(Vector3 hitPosition)
    {
        if (Vector3.Distance(hitPosition, GameInfo.gi.player.transform.position) > GameInfo.gi.maxCursorReach)
        {
            Vector3 direction = (hitPosition - GameInfo.gi.player.transform.position).normalized;
            return GameInfo.gi.player.transform.position + (direction * GameInfo.gi.maxCursorReach);
        }
        else
        {
            return hitPosition;
        }
    }
}
