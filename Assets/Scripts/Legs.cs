using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    private Player Player1;

    void Start()
    {
        Player1 = GameObject.Find("PC").GetComponent<Player>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            Player1.health = 0;
        }
        Player1.grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Player1.grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "bedrock")
        {
            Player1.health = 0;
        }
    }
}
