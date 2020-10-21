using UnityEngine;
using UnityEngine.UI;

public class PlayerButton:MonoBehaviour
{
    public Color TeamColor;
    public Image image;
    public Color ConfirmedColor;
    private bool confirmed;
    

    public void Confirm()
    {
        confirmed = true;
    }

    public void Unconfirm()
    {
        confirmed = false;
    }

    private void Update()
    {  
    }
}