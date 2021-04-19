using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DiffultyText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI difficultytext;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        difficultytext.text = StatMover.Instance.difficulty.ToString();
    }
}
