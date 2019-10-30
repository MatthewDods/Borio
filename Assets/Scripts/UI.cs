using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    UI Instance;
    private Text dmgOb;
    private Text hpOb;
    private Text expOb;
    private Text dialogueOb;
    private Text timerOb;
    private string dialogue;

    // Use this for initialization
    void Start () {
        dmgOb = GameObject.Find("Damage").GetComponent<Text>();
        hpOb = GameObject.Find("Health").GetComponent<Text>();
        expOb = GameObject.Find("Experience").GetComponent<Text>();
        dialogueOb = GameObject.Find("Dialogue").GetComponent<Text>();
        timerOb = GameObject.Find("Timer").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        dmgOb.text = "Damage: " + GameObject.Find("PC").GetComponent<Player>().damage;
        hpOb.text = "Health: " + GameObject.Find("PC").GetComponent<Player>().health;
        expOb.text = "Experience: " + GameObject.Find("PC").GetComponent<Player>().exp;
        timerOb.text = "Timer: " + FormatTime(GameObject.Find("PC").GetComponent<Player>().timer);
        dialogueOb.text = dialogue;
    }

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void changeDialogue(string newDialogue)
    {
        dialogue = "" + newDialogue;
    }

    private string FormatTime(float timeInSeconds)
    {
        return string.Format("{0}:{1:00}", Mathf.FloorToInt(timeInSeconds / 60), Mathf.FloorToInt(timeInSeconds % 60));
    }
}
