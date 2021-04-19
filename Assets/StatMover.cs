using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatMover : MonoBehaviour
{
    public static StatMover Instance;

    public int skill = 0;
    public int difficulty = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
                Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
