using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;

    [SerializeField] private Transform ParentObject;
    private InputAction spawnAction;

    void Awake()
    {
        spawnAction = new InputAction("Spawn", binding: "<Keyboard>/e");
    }

    void OnEnable() => spawnAction.Enable();
    void OnDisable() => spawnAction.Disable();

    void Update()
    {
        if (spawnAction.WasPressedThisFrame() && objectToSpawn)
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        GameObject newSpawn = Instantiate(objectToSpawn, new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0), Quaternion.identity, ParentObject);
        Refrences.@r.o.AddObject(newSpawn);
    }
}
