using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public enum direction
{
    horizonal,
    vertical,
}

public class HackManager : MonoBehaviour
{

    //placement
    [SerializeField]
    Image win, lose;
    public static HackManager Instance;
    [SerializeField]
    GameObject hackingcodeParent;
    [SerializeField]
    GameObject buffercodeParent;
    public GameObject gridprefab;
    [SerializeField]
    TextMeshProUGUI timerText;

    int sizex, sizey;
    [SerializeField]
    float offsetx, offsety;
    [SerializeField]
    float padding;

    // need to know to initialize
    int skill = 0;
    int diffculty = 3;
    int buffer = 5;

    // game info
    [SerializeField]
    List<hexcode> hackingcode = new List<hexcode>();
    [SerializeField]
    direction hackdir = direction.horizonal; 
    public int currentAnchor = 0;
    [SerializeField]
    int currentBufferIndex = 0;
    [SerializeField]
    float timer = 10f;
    [SerializeField]
    bool timerStart = false;
    [SerializeField]
    bool won;
    //list 
    [SerializeField]
    public List<List<ButtonMatrixScript>> HexGrid = new List<List<ButtonMatrixScript>>();
    [SerializeField]
    List<ButtonMatrixScript> bufferList = new List<ButtonMatrixScript>();

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
        timerText.text = timer.ToString() + ".0";
        skill = StatMover.Instance.skill;
        diffculty += StatMover.Instance.difficulty;
        sizex = 1 + diffculty;
        sizey = 0 + diffculty;

        offsetx = (sizex - 1) * (50 + padding) / 2.0f;
        offsety = (sizey - 1) * (50 + padding) / 2.0f;

        for (int i = 0; i < sizex; i++)
        {
            HexGrid.Add(new List<ButtonMatrixScript>());
            for (int j = 0; j < sizey; j++)
            {
                GameObject temp = Instantiate(gridprefab, gameObject.transform);
                temp.transform.Translate(new Vector3(-offsetx + (i * (50 + padding)), offsety - (j * (50 + padding)), 0));
                temp.GetComponent<ButtonMatrixScript>().RandomizeHex();
                temp.GetComponent<ButtonMatrixScript>().x = i;
                temp.GetComponent<ButtonMatrixScript>().y = j;
                HexGrid[i].Add(temp.GetComponent<ButtonMatrixScript>());
            }
        }

        //pick unlockcode
        List<ButtonMatrixScript> path = new List<ButtonMatrixScript>();
        int anchor = 0;
        direction pathdir = direction.horizonal;
        int length = Mathf.Max(diffculty, (buffer + skill));
        for (int i = 0; i < length; i++)
        {
            bool added = false;
            while (!added)
            {
                if (pathdir == direction.horizonal)
                {
                    int x = Random.Range(0, sizex);
                    ButtonMatrixScript temp = HexGrid[x][anchor];
                    if (!path.Contains(temp))
                    {
                        //add to path
                        path.Add(temp);
                        anchor = x; 
                        pathdir = direction.vertical;
                        added = true;
                    }
                }
                else
                {
                    int y = Random.Range(0, sizey);
                    ButtonMatrixScript temp = HexGrid[anchor][y];
                    if (!path.Contains(temp))
                    {
                        //add to path
                        path.Add(temp);
                        anchor = y;
                        pathdir = direction.horizonal;
                        added = true;
                    }
                }
            }
        }
        // see if its in the middle of the path or at the start
        int delay = Mathf.Max(0, Random.Range(0, buffer + skill - diffculty));
        
        for (int i = 0; i < path.Count; i ++)
        {
            if (i >= delay && hackingcode.Count < diffculty)
            {
                hackingcode.Add(path[i].hex);
            }
        }

        for (int i = 0; i < diffculty; i ++)
        {
            GameObject temp = Instantiate(gridprefab, hackingcodeParent.transform);
            temp.transform.Translate(new Vector3(i * (50 + padding), 0, 0));
            temp.GetComponent<ButtonMatrixScript>().SetHexCode(hackingcode[i]);
        }

        for (int i = 0; i < (buffer + skill); i++)
        {
            GameObject temp = Instantiate(gridprefab, buffercodeParent.transform);
            temp.transform.Translate(new Vector3(i * (50 + padding), 0, 0));
            temp.GetComponent<ButtonMatrixScript>().SetHexCode(hexcode.Null);
            bufferList.Add(temp.GetComponent<ButtonMatrixScript>());
        }
    }

    public void Clicked(int x, int y, hexcode _hex)
    {
        if (won)
            return;
        if (currentBufferIndex >= bufferList.Count)
            return;

        if (hackdir == direction.horizonal)
        {
            if (y == currentAnchor)
            {
                bufferList[currentBufferIndex].SetHexCode(_hex);
                currentBufferIndex++;
                HexGrid[x][y].SetHexCode(hexcode.Null);
                currentAnchor = x;
                hackdir = direction.vertical;
                
                timerStart = true;
            }
        }
        else
        {
            if (x == currentAnchor)
            {
                bufferList[currentBufferIndex].SetHexCode(_hex);
                currentBufferIndex++;
                HexGrid[x][y].SetHexCode(hexcode.Null);
                currentAnchor = y;
                hackdir = direction.horizonal;
                
                timerStart = true;
            }
        }
        CheckUnlock();
    }


    public void CheckUnlock()
    {
        if (currentBufferIndex < diffculty)
            return;

        int unlockindex = 0;
        for (int i = 0; i < currentBufferIndex; i ++)
        {
            if (bufferList[i].hex == hackingcode[unlockindex])
            {
                unlockindex++;
            }
            else
            {
                unlockindex = 0;
            }
        }
        if (unlockindex >= hackingcode.Count)
        {
            won = true;
            Debug.Log("win!!!");
            win.gameObject.SetActive(true);
        }
        if (currentBufferIndex == bufferList.Count)
        {
            lose.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (won)
            return;
        if (currentBufferIndex == bufferList.Count)
            return;
        if (timer <= 0)
        {
            timer = 0;
            lose.gameObject.SetActive(true);
            //display lose
        }
        else if (timerStart)
        {
            timer -= Time.deltaTime;
            float temp = (int)(timer * 10);
            temp /= 10.0f;
            if (temp % 1 == 0)
            {
                timerText.text = temp.ToString() + ".0";
            }
            else
            {
                timerText.text = temp.ToString();
            }
        }
    }
}
