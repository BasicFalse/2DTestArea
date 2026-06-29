using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DotGrid : MonoBehaviour
{
    public Mesh dotMesh;
    public Material dotMaterial;
    public float dotSize;
    public float spacing = 1f;
    public int radius = 10;
    public float maxMouseDist = 5f;
    public float glowRadius;
    public float mouseRenderMod = 1;
    public float XOffset;
    public float YOffset;
    public float ZOffest;
    public Transform cameraTransform;

    private List<Matrix4x4> dotMatrices = new();
    private HashSet<Vector2Int> visibleDots = new();

    void Update()
    {
        Vector2 cameraPos = cameraTransform.position;
        Vector2Int camGridPos = new(
            Mathf.RoundToInt(cameraPos.x / spacing),
            Mathf.RoundToInt(cameraPos.y / spacing)
        );

        dotMatrices.Clear();
        visibleDots.Clear();

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0f));
        mouseWorld.z = 0f;

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                Vector2Int gridPos = camGridPos + new Vector2Int(x, y);
                Vector3 worldPos = new Vector3(gridPos.x * spacing, gridPos.y * spacing, 0f);

                if (Vector3.Distance(worldPos, mouseWorld) <= maxMouseDist * mouseRenderMod)
                {
                    worldPos.x += XOffset;
                    worldPos.y += YOffset;
                    worldPos.z += ZOffest;

                    Matrix4x4 matrix = Matrix4x4.TRS(worldPos, Quaternion.identity, Vector3.one * dotSize);
                    dotMatrices.Add(matrix);
                    visibleDots.Add(gridPos);
                }
            }
        }

        dotMaterial.SetVector("_MouseWorldPos", mouseWorld);
        dotMaterial.SetFloat("_GlowRadius", glowRadius);

        for (int i = 0; i < dotMatrices.Count; i += 1023)
        {
            int count = Mathf.Min(1023, dotMatrices.Count - i);
            Graphics.DrawMeshInstanced(dotMesh, 0, dotMaterial, dotMatrices.GetRange(i, count));
        }
    }
}