using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> currentObjects = new();

    public bool AddObject(GameObject objToAdd)
    {
        if (!currentObjects.Contains(objToAdd))
        {
            currentObjects.Add(objToAdd);
            Log($"Added object {objToAdd.name} to currentObjects.");
            return true;
        }

        
        Log("Failed to add object to currentObjects as it is already present.", true, objToAdd);
        return false;
    }

    private void Log(string log, bool troublsome = false, object refrence = null)
    {
        Refrences.@r.l.AddLog(log, 4f);

        if(troublsome)
        Debug.LogAssertion(log);
    }
}
