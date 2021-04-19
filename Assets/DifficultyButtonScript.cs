using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyButtonScript : MonoBehaviour
{
    public void DifficultyIncrease()
    {
        StatMover.Instance.difficulty = Mathf.Min(3, StatMover.Instance.difficulty + 1);
    }

    public void DifficultyDecrease()
    {
        StatMover.Instance.difficulty = Mathf.Max(0, StatMover.Instance.difficulty - 1);
    }
}
