using System;
using UnityEngine;

public class CallerContext
{
    private Interactable callingScript;
    private string context;
    private string arguments;

    public CallerContext(Interactable mb, string contxt, string args = null)
    {
        callingScript = mb;
        context = contxt;
        arguments = args;
    }

    public Interactable WhosCalling => callingScript;

    public string GetContext => context;
    public string GetArguments => arguments;
}
