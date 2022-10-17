using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeCanvas : MonoBehaviour
{
    public CanvasGroup myUIGroup;
    bool fadeIn = false;
    bool fadeOut = false;

    public void ShowUI()
    {
        fadeIn = true;
    }

    public void HideUI()
    {
        fadeOut = true;
    }

    void Update()
    {
        if (fadeIn)
        {
            if (myUIGroup.alpha < 1)
            {
                myUIGroup.alpha += Time.deltaTime / 0f;
                if (myUIGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            } 
        }

        if (fadeOut)
        {
            if (myUIGroup.alpha > 0)
            {
                myUIGroup.alpha -= Time.deltaTime / 0f;
                if (myUIGroup.alpha == 0)
                {
                    fadeOut = false;
                    int sceneIndex = SceneManager.GetActiveScene().buildIndex;
                    SceneManager.LoadScene(sceneIndex + 1);
                }
            }
        }

    }
}
