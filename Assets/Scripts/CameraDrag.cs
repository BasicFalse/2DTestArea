using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDrag : MonoBehaviour
{
    [Header("Drag Settings")]
    public float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;
    private Vector3 lastMousePosition;

    [Header("Zoom Settings")]
    public float zoomSpeed = 5f;
    public float minZoom = 2f;
    public float maxZoom = 20f;
    public float zoomSmoothTime = 0.15f;
    private float targetZoom;
    private float zoomVelocity;
    private float currentZoom;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        targetPosition = transform.position;
        targetZoom = cam.orthographicSize;
        currentZoom = cam.orthographicSize;
    }

    void Update()
    {
        HandleDrag();
        HandleZoom();

        float previousZoom = currentZoom;
        currentZoom = Mathf.SmoothDamp(currentZoom, targetZoom, ref zoomVelocity, zoomSmoothTime);

        float zoomDelta = currentZoom - previousZoom;
        if (Mathf.Abs(zoomDelta) > 0.0001f)
        {
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector3 mouseScreen3D = new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0f);

            cam.orthographicSize = previousZoom;
            Vector3 mouseWorldAtPrevZoom = cam.ScreenToWorldPoint(mouseScreen3D);

            cam.orthographicSize = currentZoom;
            Vector3 mouseWorldAtCurrentZoom = cam.ScreenToWorldPoint(mouseScreen3D);

            targetPosition += mouseWorldAtPrevZoom - mouseWorldAtCurrentZoom;
        }

        cam.orthographicSize = currentZoom;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    void HandleDrag()
    {
        var mouse = Mouse.current;

        if (mouse.rightButton.wasPressedThisFrame)
        {
            lastMousePosition = mouse.position.ReadValue();
        }

        if (mouse.rightButton.isPressed)
        {
            Vector3 currentMousePosition = mouse.position.ReadValue();
            Vector3 delta = currentMousePosition - lastMousePosition;

            float pixelsPerUnit = Screen.height / (currentZoom * 2f);
            targetPosition += new Vector3(-delta.x, -delta.y, 0f) / pixelsPerUnit;

            lastMousePosition = currentMousePosition;
        }
    }

    void HandleZoom()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (Mathf.Abs(scroll) > 0.01f)
        {
            // scroll.y comes in as large pixel values (~120 per notch), normalize to match old GetAxis scale
            targetZoom -= (scroll / 12f) * targetZoom;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        }
    }
}