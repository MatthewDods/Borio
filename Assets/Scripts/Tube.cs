using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tube : MonoBehaviour {
	
	void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PC")
        {
            GameObject.Find("PC").GetComponent<Player>().SavePlayer();
            NextScene(); //changes the scene to the next one
        }
    }

    public static void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (GetScene() == 2)
        {
            Vector3 tp = GameObject.Find("StartPad").transform.position;
            GameObject.Find("PC").transform.position = tp;
        }
    }

    public static int GetScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public static void Restart()
    {
        SceneManager.LoadScene(1);
        Destroy(GameObject.Find("PC"));
        Destroy(GameObject.Find("Main Camera"));
        Destroy(GameObject.Find("UICanvas"));
        GameObject.Find("PC").GetComponent<Player>().Reset();
    }
}
