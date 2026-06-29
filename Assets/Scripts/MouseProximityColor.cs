using UnityEngine;

public class MouseProximityColor : MonoBehaviour
{
    public Color farColor = Color.white;
    public Color closeColor = Color.red;
    public float maxDistance = 200f; // Max distance in pixels before color change fades out

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition;

        float distance = Vector2.Distance(new Vector2(screenPos.x, screenPos.y), new Vector2(mousePos.x, mousePos.y));
        float t = Mathf.Clamp01(1f - (distance / maxDistance));

        spriteRenderer.color = Color.Lerp(farColor, closeColor, t);
    }
}
