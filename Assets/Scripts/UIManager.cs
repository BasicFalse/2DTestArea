using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private bool IsInOverlay => currentOverlay != null;
    [SerializeField] private Transform OverlayPivot;
    [SerializeField] private GameObject currentOverlay;
    [SerializeField] private Overlay currentUI;

    public bool CanCallUIOverlay => !IsInOverlay;

    public void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && IsInOverlay)
        {
            currentUI.DestroyUI();
            currentOverlay = null;
            currentUI = null;
        }
    }


    public Overlay CallUIOverlay(GameObject overlay, CallerContext Context)
    {
        currentOverlay = Instantiate(overlay,Vector3.zero,Quaternion.identity,OverlayPivot);
        currentUI = currentOverlay.GetComponent<Overlay>();
        currentUI.HandContext(Context);
        return currentUI;
    }
}
