using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    TextAsset textFile;
    string[] names = { "Shinichi","Ayame","Ayami" };
    string[] allScripts;
    Text textName, textRel;
    string scriptName, scripts;
    int currentLine = 1;
    TW_Regular tw;
    FadeCanvas fadeCanvas;
    int state = 0;
    int score = 0;
    public GameObject choicesPanel;
    public Text textC1, textC2;

    void Awake()
    {
        string sceneName = SceneManager.GetActiveScene().name.ToString();
        textFile = Resources.Load<TextAsset>(sceneName);
        scriptName = sceneName;
        scripts = textFile.text;
        textName = GameObject.Find("TextName").GetComponent<Text>();
        textRel = GameObject.Find("TextRel").GetComponent<Text>();
        tw = GameObject.Find("Text").GetComponent<TW_Regular>();
        fadeCanvas = GameObject.Find("Canvas").GetComponent<FadeCanvas>();
        allScripts = scripts.Split("\n");
    }

    void Start()
    {
        fadeCanvas.ShowUI();
        display();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && state < 2)
        {
            currentLine++;
            display();
        }
    }

    void display()
    {
        string[] tmp = new string[10];
        tmp = allScripts[currentLine - 1].Split(":");
        state = int.Parse(tmp[0]);
        int cNumber = int.Parse(tmp[1]);
        textName.text = names[cNumber];
        score += int.Parse(tmp[2]);
        RelationshipRank(score);

        switch (state)
        {
            case 0:
                tw.ORIGINAL_TEXT = tmp[3];
                tw.StartTypewriter();
                break;
            case 1:
                fadeCanvas.HideUI();
                break;
            case 2:
                textC1.text = tmp[3];
                textC2.text = tmp[4];
                choicesPanel.SetActive(true);
                break;
            case 3:
                break;
            default:
                break;
        }

    }
    public void SelectChoices1()
    {
        scriptName = scriptName + "-1";
        LoadNewScript();
    }

    public void SelectChoices2()
    {
        scriptName = scriptName + "-2";
        LoadNewScript();
    }

    void LoadNewScript()
    {
        textFile = Resources.Load<TextAsset>(scriptName);
        scripts = textFile.text;
        allScripts = scripts.Split("\n");
        currentLine = 1;
        choicesPanel.SetActive(false);
        display();
    }

    void RelationshipRank(int s)
    {
        if (s > 1) textRel.text = "Rank: Close Friend (" + score + ")";
        else if (s == 1 || s >= 0) textRel.text = "Rank: Friend (" + score + ")";
        else if (s < 0) textRel.text = "Rank: Hate (" + score + ")";
    }
}