using Unity.Mathematics;
using UnityEngine;

public class SpawnPanelAssembler : MonoBehaviour
{
    [SerializeField] private ObjectSpawnContainer Codex;
    [SerializeField] private GameObject PanelTemplate;
    [SerializeField] private Transform GridContentParent;

    void Start()
    {
        for(int i = 0; i < Codex.Objects.Length; i++)
        {
            SpawnPanelButton sp = Instantiate(PanelTemplate, Vector3.zero, quaternion.identity, GridContentParent).GetComponent<SpawnPanelButton>();
            sp.icon.sprite = Codex.Objects[i].obj.GetComponent<SpriteRenderer>().sprite;
            sp.icon.GetComponent<RectTransform>().anchoredPosition = new(50,50);
            sp.icon.transform.localScale = new(2,2,2);
            sp.icon.SetNativeSize();
            sp.objName = Codex.Objects[i].name;
            sp.linkedObject = Codex.Objects[i].obj;
        }
    }
}
