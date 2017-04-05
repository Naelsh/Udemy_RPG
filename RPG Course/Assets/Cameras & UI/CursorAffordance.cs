using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {
    
    [SerializeField]
    [Tooltip("Need to be a sprite set to Texture type: Cursor")]
    Texture2D walkCursor = null;
    [SerializeField]
    [Tooltip("Need to be a sprite set to Texture type: Cursor")]
    Texture2D attackCursor = null;
    [SerializeField]
    [Tooltip("Need to be a sprite set to Texture type: Cursor")]
    Texture2D questionCursor = null;

    private CameraRaycaster cameraRaycaster;
    private Vector2 cursorHotSpot;

    // Use this for initialization
    void Start () {
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        cursorHotSpot = new Vector2(96,96);
    }
	
	// Update is called once per frame
	void LateUpdate (){
        switch (cameraRaycaster.layerHit)
        {
            case Layer.Default:
                break;
            case Layer.TransparentFX:
                break;
            case Layer.IgnoreRaycast:
                break;
            case Layer.Water:
                break;
            case Layer.UI:
                break;
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, cursorHotSpot, CursorMode.Auto);
                break;
            case Layer.Enemy:
                Cursor.SetCursor(attackCursor, cursorHotSpot, CursorMode.Auto);
                break;
            case Layer.RaycastEndStop:
                Cursor.SetCursor(questionCursor, cursorHotSpot, CursorMode.Auto);
                break;
            default:
                print("Don't know what cursor to show");
                break;
        }
        
	}
}