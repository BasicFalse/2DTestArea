using UnityEngine;

public class LockedCanister : Interactable
{
    [SerializeField] private GameObject lockOverlay;
    [SerializeField] private GameObject UnlockedLid;
    [SerializeField] private Sprite UnlockedSprite;
    [SerializeField] private Transform LidStartPos;
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private LockedCanisterUI ui;
    [SerializeField] private bool opened;

    public override InteractableType GetInteractType() => InteractableType.UI;

    public override void OnInteract()
    {
        if (Refrences.@r.ui.CanCallUIOverlay && !opened){
            CallerContext cc = new(this, "interactable");
            ui = Refrences.@r.ui.CallUIOverlay(lockOverlay, cc).GetComponent<LockedCanisterUI>();
            ui.GenerateCode();
        }
    }

    public override void OnUIDestroyed()
    {
        ui = null;
    }

    public void OnUnlocked()
    {
        if (!ui.IsUnlocked || opened) return;
        opened = true;
        Renderer.sprite = UnlockedSprite;
        UnlockedLid = Instantiate(UnlockedLid, LidStartPos.position, LidStartPos.rotation, transform);
        collider.size = new(0.27f,0.16f);
        collider.offset = new(-0.005f,0f);
    }

}
