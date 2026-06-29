using UnityEngine;

public class Overlay : MonoBehaviour
{
    private CallerContext currentContext = null;
    public void HandContext(CallerContext handedContxt)
    {
        if(currentContext == null)
            currentContext = handedContxt;
    }

    public void DestroyUI()
    {
        currentContext.WhosCalling.OnUIDestroyed();
        Destroy(gameObject);
    }
}
