using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButtonScript : MonoBehaviour
{
    public void SkillIncrease()
    {
        StatMover.Instance.skill = Mathf.Min(3, StatMover.Instance.skill + 1);
    }

    public void SkillDecrease()
    {
        StatMover.Instance.skill = Mathf.Max(0, StatMover.Instance.skill - 1);
    }
}
