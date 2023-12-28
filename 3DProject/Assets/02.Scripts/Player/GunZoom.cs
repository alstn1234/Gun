using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunZoom : MonoBehaviour
{
    public GameObject zoomUI;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnZoomInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && PlayerController.instance.isCursorLock)
        {
            if (zoomUI.gameObject.activeSelf)
            {
                zoomUI.SetActive(false);
                CameraUnZoom();
            }
            else
            {
                zoomUI.SetActive(true);
                CameraZoom();
            }
        }
    }

    private void CameraZoom()
    {
        _mainCamera.fieldOfView = 20f;
    }

    private void CameraUnZoom()
    {
        _mainCamera.fieldOfView = 60f;
    }
}
