using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;

    [SerializeField]
    float walkMoveStopRadius = 0.2f;
    [SerializeField] bool isInDirectMode = false; // TODO consider making it static later

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // TODO allow player to map later or add to menu
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            
            isInDirectMode = !isInDirectMode;
        }
        if (isInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            ProcessMouseMovement();
        }
        

    }

    private void ProcessDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative to movement
        Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;

        m_Character.Move(m_Move, false, false);
    }

    private void ProcessMouseMovement()
    {

        print("Cursor raycast hit " + cameraRaycaster.layerHit);

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
                currentClickTarget = cameraRaycaster.hit.point;  // So not set in default case
                break;
            case Layer.Enemy:
                print("An enemy, not yet implemented");
                break;
            case Layer.RaycastEndStop:
                break;
            default:
                break;

        }

        var playerToClickPoint = currentClickTarget - transform.position; // TODO sort out this click to bugg
        if (Input.GetMouseButton(0))
        {
            if (playerToClickPoint.magnitude >= walkMoveStopRadius)
            {
                m_Character.Move(playerToClickPoint, false, false);
            }
        }
        if (playerToClickPoint.magnitude <= walkMoveStopRadius)
        {
            m_Character.Move(Vector3.zero, false, false);
        }
    }
}

