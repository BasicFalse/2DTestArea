using UnityEngine;

public class LockedCanisterButton : Interactable
{
    [SerializeField] private LockedCanisterUI ui;
    [SerializeField] private char num;
    
    public override InteractableType GetInteractType() => InteractableType.Simple;

    public override void OnInteract() => ui.OnPressedButton(num);

    public override void OnUIDestroyed(){}
}
