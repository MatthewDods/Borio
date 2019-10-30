using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    //adjust this to change speed
    float speed = 5f;
    //adjust this to change how high it goes
    float height = 0.01f;
    private Player Player1;

    private void Start()
    {
        Player1 = GameObject.Find("PC").GetComponent<Player>();
    }

    void Update()
    {
        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.position;
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
        //set the object's Y to the new calculated Y

        transform.position = new Vector3(pos.x, newY, pos.z);

        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.name == "Heal" && collision.gameObject.name == "PC")
        {
            Destroy(gameObject);
            Player1.health += 25;
            Player1.score += 2;
        }
        else if (gameObject.name == "ExpOrb" && collision.gameObject.name == "PC")
        {
            Destroy(gameObject);
            Player1.exp += 15;
            Player1.score += 5;
        }
        else if (gameObject.name == "Power" && collision.gameObject.name == "PC")
        {
            Destroy(gameObject);
            Player1.damage += 30;
            Player1.score += 10;
        }
        else if (gameObject.name == "legday" && collision.gameObject.name == "PC")
        {
            Destroy(gameObject);
            Player1.JumpForce = 500;
            Player1.score += 30;
        }
    }
}
