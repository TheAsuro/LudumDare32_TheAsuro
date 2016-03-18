using UnityEngine;

public class MousePointer : MonoBehaviour
{
    [SerializeField]
    private GameObject pointerPrefab;
    private GameObject pointerInstance;
    [SerializeField]
    private bool draw = true;

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
                    pointerInstance.transform.position = newCursorPos;
                }
                else
                {
                    pointerInstance = (GameObject)Instantiate(pointerPrefab, newCursorPos, Quaternion.identity);
                    pointerInstance.transform.SetParent(GameInfo.gi.transform.FindChild("Canvas"), true);
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
        Vector3 val;

        if (Vector3.Distance(hitPosition, GameInfo.gi.player.transform.position) > GameInfo.gi.maxCursorReach)
        {
            Vector3 direction = (hitPosition - GameInfo.gi.player.transform.position).normalized;
            val = GameInfo.gi.player.transform.position + (direction * GameInfo.gi.maxCursorReach);
        }
        else
        {
            val = hitPosition;
        }

        return Camera.main.WorldToScreenPoint(val);
    }
}
