using UnityEngine;
using UnityEngine.InputSystem;

public class MouseGrabber : MonoBehaviour
{
    public LayerMask grabMask = ~0;
    public LayerMask interactMask;
    public float jointFrequency = 10f;
    public float jointDamping = 1f;

    [SerializeField] private Vector2 offset;
    private Camera cam;
    private GameObject heldObject;
    private Rigidbody2D heldRigidbody;
    private TargetJoint2D joint;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        var mouse = Mouse.current;
        Vector2 mouseWorld = GetMouseWorldPos();

        if (mouse.leftButton.wasPressedThisFrame)
            TryGrab(mouseWorld);

        if (mouse.leftButton.wasReleasedThisFrame)
            Release();

        if (mouse.rightButton.wasPressedThisFrame)
            TryInteract(mouseWorld);


        if (joint != null)
        {
            joint.target = mouseWorld;
        }
        else if (heldObject)
        {
            heldObject.transform.position = offset + mouseWorld;
        }

    }

    void TryGrab(Vector2 mouseWorld)
    {
        Collider2D hit = Physics2D.OverlapPoint(mouseWorld, grabMask);
        if (hit == null) return;    
        
        GameObject obj = hit.gameObject;
        Grabbable grabbable = hit.GetComponentInParent<Grabbable>();

        if (grabbable)
        {
            if(grabbable.cantGrab) return;
        }

        offset = (Vector2)obj.transform.position - mouseWorld;
        
        heldRigidbody = obj.GetComponentInParent<Rigidbody2D>();
        if (heldRigidbody != null)
        {
            // Add the joint to the object itself
            joint = heldRigidbody.gameObject.AddComponent<TargetJoint2D>();
            joint.dampingRatio = jointDamping;
            joint.frequency = jointFrequency;
            joint.maxForce = 1000000;
            joint.autoConfigureTarget = false;
            joint.target = mouseWorld;

            // Anchor at the exact click point in local space
            joint.anchor = heldRigidbody.transform.InverseTransformPoint(mouseWorld);
        }

        heldObject = obj;
    }

    void Release()
    {
        if (joint != null)
        {
            Destroy(joint);
            joint = null;
        }


        heldObject = null;
        heldRigidbody = null;
    }

    void TryInteract(Vector2 mouseWorld)
    {
        Collider2D hit = Physics2D.OverlapPoint(mouseWorld, interactMask);
        if (hit == null) return;    
        
        Interactable interactable = hit.GetComponentInParent<Interactable>();

        switch (interactable.GetInteractType())
        {
            case InteractableType.Simple:
                interactable.OnInteract();
                break;
            case InteractableType.UI:
                if(Refrences.@r.ui.CanCallUIOverlay)
                    interactable.OnInteract();
                break;
        }
    }

    Vector2 GetMouseWorldPos()
    {
        Vector2 mouseScreen = Mouse.current.position.ReadValue();
        return cam.ScreenToWorldPoint(new Vector3(mouseScreen.x, mouseScreen.y, 0f));
    }
}