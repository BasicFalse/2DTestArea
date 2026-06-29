using UnityEngine;
using UnityEngine.UI;

public class SpawnPanelButton : Interactable
{
    public string objName;
    public Image icon;
    public GameObject linkedObject;

    public override InteractableType GetInteractType() => InteractableType.Simple;

    public override void OnInteract()
    {
        Refrences.@r.s.objectToSpawn = linkedObject;
        Refrences.@r.l.AddLog("Set next spawned object to: " + linkedObject.name);
    }

    public override void OnUIDestroyed()
    {
        throw new System.NotImplementedException();
    }

}
