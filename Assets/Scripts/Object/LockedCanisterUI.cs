using UnityEngine;

public class LockedCanisterUI : Overlay
{
    [SerializeField] private bool unlocked;

    public bool IsUnlocked => unlocked;
}
