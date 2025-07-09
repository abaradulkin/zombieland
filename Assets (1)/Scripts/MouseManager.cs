using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    [Header("Layer mask with clickable objects")]
    public LayerMask clickableLayer;

    [Header("Cursors types")]
    public Texture2D attack;
    public Texture2D interact;
    public Texture2D move;
    public Texture2D pointer;

    void SkipUpdate()
    {
        Texture2D cursorType;
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 150, clickableLayer.value))
        {
            GameObject hitObject = hit.collider.gameObject;

            switch (hitObject.tag)
            {
                case "Zone":
                    cursorType = move;
                    break;
                case "Enemy":
                    cursorType = attack;
                    break;
                default:
                    cursorType = interact;
                    break;
            }
        }
        else
        {
            cursorType = pointer;
        }

        Cursor.SetCursor(cursorType, new Vector3(16, 16), CursorMode.Auto);
    }
}