using UnityEngine;

[System.Serializable]
public struct ObjectContainer
{
    public string name;
    public GameObject obj;
    public string catagory;
    public bool show;

    public ObjectContainer(string iname, GameObject iobj, string icatagory = "unassigned", bool ishow = true)
    {
        name = iname;
        obj = iobj;
        catagory = icatagory;
        show = ishow;
    }
}

[CreateAssetMenu(fileName = "ObjectCodex", menuName = "Containers/ObjectSpawnContainer")]
public class ObjectSpawnContainer : ScriptableObject
{
    public ObjectContainer[] Objects;
}
