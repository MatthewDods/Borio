using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.name == "Rock" && collision.gameObject.name == "PC")
        {
            GameObject.Find("UICanvas").GetComponent<UI>().changeDialogue("This damn rock is in the way... maybe I should try jumping? HINT: Jump with W or Spacebar!");
            Invoke("clear", 5);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    { 
        if (gameObject.name == "StoryPoint1" && collision.gameObject.name == "PC")
        {
            Destroy(gameObject);
            GameObject.Find("UICanvas").GetComponent<UI>().changeDialogue("What the? Where am I? I swear I was in the middle of fighting those guys from that other plumbing company... what happened?!");
            Invoke("clear", 3);
        }

        if (gameObject.name == "StoryPoint2" && collision.gameObject.name == "PC")
        {
            Destroy(gameObject);
            GameObject.Find("UICanvas").GetComponent<UI>().changeDialogue("Well at least I still know how to jump. As for why I'm wearing all this armor... no clue there. Lets just get going and see if I can't find a way back!");
            Invoke("clear", 5);
        }

        if (gameObject.name == "StartPad" && collision.gameObject.name == "PC")
        {
            Destroy(gameObject);
            GameObject.Find("UICanvas").GetComponent<UI>().changeDialogue("If I want to escape I probably have to beat that guy...");
            Invoke("clear", 5);
        }
    }

    void clear()
    {
        GameObject.Find("UICanvas").GetComponent<UI>().changeDialogue("");
    }
}
