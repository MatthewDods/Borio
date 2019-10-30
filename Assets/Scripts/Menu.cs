using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button Play;
    public Button Exit;
    public Button Restart;
    public float timer;
    public float score;
    private Text scoreOb;

    // Start is called before the first frame update
    void Start()
    {
        int Scene = Tube.GetScene();
        if (Scene == 0)
        {
            Play.onClick.AddListener(PlayOnClick);
        }else
        {
            timer = GameObject.Find("PC").GetComponent<Player>().timer;
            score = GameObject.Find("PC").GetComponent<Player>().score;
            Restart.onClick.AddListener(RestartOnClick);
            float timerScore = (180 / timer) * 1000;
            score += timerScore;
            scoreOb = GameObject.Find("Score").GetComponent<Text>();
            scoreOb.text = "" + score + ";";
        }
        Exit.onClick.AddListener(ExitOnClick);
    }

    void PlayOnClick()
    {
        Tube.NextScene();
        timer = 0;
    }

    void ExitOnClick()
    {
        Application.Quit();
    }

    void RestartOnClick()
    {
        Tube.Restart();
    }
}
