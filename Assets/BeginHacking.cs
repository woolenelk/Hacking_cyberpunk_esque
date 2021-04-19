using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BeginHacking : MonoBehaviour
{
    public void OnHackButtonPressed()
    {
        SceneManager.LoadScene("HackingMiniGame");
    }
}
