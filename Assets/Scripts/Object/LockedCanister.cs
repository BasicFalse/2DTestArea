using UnityEngine;

public class LockedCanister : Interactable
{
    [SerializeField] private GameObject lockOverlay;
    [SerializeField] private LockedCanisterUI ui;
    [SerializeField] private bool isUnlocked;

    public override void OnInteract()
    {
        if (Refrences.@r.ui.CanCallUIOverlay){
            CallerContext cc = new(this, "interactable");
            ui = Refrences.@r.ui.CallUIOverlay(lockOverlay, cc).GetComponent<LockedCanisterUI>();
        }
    }

    public override void OnUIDestroyed()
    {
        if (isUnlocked)
        {
            
        }
        ui = null;
    }

    public override InteractableType GetInteractType() => InteractableType.UI;

    public void OnUnlocked()
    {
        if (ui.IsUnlocked)
        {
            isUnlocked = true;
        }
    }
}
