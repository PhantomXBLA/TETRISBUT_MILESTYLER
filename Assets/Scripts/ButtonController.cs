using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playButtonPressed()
    {
        SceneManager.LoadScene(1);
    }


    public void instructionButtonPressed()
    {
        SceneManager.LoadScene(2);
    }

    public void quitButtonPressed()
    {
        Application.Quit();
    }

    public void backButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
}
