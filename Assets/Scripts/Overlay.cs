using UnityEngine;

public class Overlay : MonoBehaviour
{
    protected CallerContext currentContext = null;
    public virtual void HandContext(CallerContext handedContxt)
    {
        currentContext ??= handedContxt;
    }

    public void DestroyUI()
    {
        currentContext.WhosCalling.OnUIDestroyed();
        Destroy(gameObject);
    }
}
