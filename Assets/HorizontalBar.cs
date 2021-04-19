using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalBar : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        float y = HackManager.Instance.HexGrid[0][HackManager.Instance.currentAnchor].gameObject.GetComponent<RectTransform>().rect.y;
        Debug.Log("horizontal bar: " + y.ToString());
        rectTransform.rect.Set(15, y+15, 350, 30);
    }
}
