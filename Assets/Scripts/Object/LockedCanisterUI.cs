using TMPro;
using UnityEngine;

public class LockedCanisterUI : Overlay
{
    public TextMeshProUGUI TapeText;
    public TextMeshProUGUI PadText;
    [SerializeField] private bool unlocked;
    [SerializeField] private bool init;
    [SerializeField] private string numCode;

    public bool IsUnlocked => unlocked;

    public void GenerateCode()
    {
        if(!init)
            numCode = Random.Range(100000,999999).ToString();

        PadText.text = "";
        TapeText.text = numCode;
        init = true;
    }

    public void OnPressedButton(char key)
    {
        switch (key)
        {
            case 'c':
                PadText.text = "";
                break;
            case 'e':
                if(PadText.text == numCode)
                    Unlocked();
                break;
            default:
                PadText.text += key;
                break;
        }
    }

    private void Unlocked()
    {
        unlocked = true;
        currentContext.WhosCalling.GetComponent<LockedCanister>().OnUnlocked();
    }
}
