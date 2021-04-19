using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AbortButtonScript : MonoBehaviour
{
    public void OnAbortPressed()
    {
        SceneManager.LoadScene("Start");
    }
}
