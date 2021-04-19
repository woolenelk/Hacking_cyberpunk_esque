using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum hexcode
{
    E9, 
    F2,
    B0,
    C4,
    A7,
    count,
    Null
}

public class ButtonMatrixScript : MonoBehaviour
{
    public hexcode hex;
    public int x = -1, y = -1;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        SetHexCode(hex);
    }

    public void RandomizeHex()
    {
        SetHexCode((hexcode)Random.Range(0, (int)hexcode.count));
    }

    public void SetHexCode(hexcode _hex)
    {
        hex = _hex;
        switch (hex)
        {
            case hexcode.E9:
                text.text = "E9";
                break;
            case hexcode.F2:
                text.text = "F2";
                break;
            case hexcode.B0:
                text.text = "B0";
                break;
            case hexcode.C4:
                text.text = "C4";
                break;
            case hexcode.A7:
                text.text = "A7";
                break;
            case hexcode.Null:
                text.text = "--";
                break;
            default:
                text.text = "";
                break;
        }
    }
    public void OnButtonPressed()
    {
        Debug.Log("x:" + x + "  y:" + y + "  hex:" + hex);
        if (hex == hexcode.Null)
            return;
        if (x < 0 || y < 0)
            return;
        HackManager.Instance.Clicked(x, y, hex);
    }
}
