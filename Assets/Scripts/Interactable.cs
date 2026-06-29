using UnityEngine;

public enum InteractableType
{
    Simple,
    UI
}

public abstract class Interactable : MonoBehaviour
{
    private InteractableType type;

    public abstract InteractableType GetInteractType();

    public abstract void OnInteract();

    public abstract void OnUIDestroyed();
}
