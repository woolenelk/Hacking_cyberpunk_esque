using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SkillText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI skilltext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        skilltext.text = StatMover.Instance.skill.ToString();
    }
}
