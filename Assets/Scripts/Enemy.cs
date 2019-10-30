using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour {
    public string Name;
    public int Health;
    public int Damage;
    public int NMExp;
    public Collider2D collider1;
    public bool _attackAllow, _NMEattackAllow;
    public float min = 2f;
    public float max = 3f;

    private Player Player1;
    private Collider2D weaponCollider, playerCollider;

    public Enemy(string name, int health, int damage, int exp, Collider2D newCollider)
    {
        Name = name;
        Health = health;
        Damage = damage;
        NMExp = exp;
        collider1 = newCollider;
    }

    public void takeDamage(int damageAmount)
    {
        Health = Health - damageAmount;
        Debug.Log("Player dealt " + damageAmount + "to enemy.");
    }

    // Use this for initialization
    void Start () {
        Player1 = GameObject.Find("PC").GetComponent<Player>();
        weaponCollider = Player1.weaponCollider;
        playerCollider = Player1.GetComponent<Collider2D>();
        collider1 = GetComponent<Collider2D>();
        min = transform.position.x;
        max = transform.position.x + 5;
    }

    // Update is called once per frame
    void Update () {
        transform.position = new Vector3(Mathf.PingPong(Time.time * 5, max - min) + min, transform.position.y, transform.position.z);

        if (Health <= 0)
        { 
            if (gameObject.name == "chad")
            {
                Player1.exp += NMExp;
                Player1.score += NMExp;
                Tube.NextScene();
            }
            Destroy(collider1.gameObject);
            Player1.exp += NMExp;
            Player1.score += NMExp;
        }

        if (_attackAllow)
        {
            if (weaponCollider.IsTouching(collider1))
            {
                _attackAllow = false;
                takeDamage(Player1.damage);
            }
        }
        else
        {
            if (!weaponCollider.IsTouching(collider1))
            {
                _attackAllow = true;
            }
        }

        if (_NMEattackAllow)
        {
            if (playerCollider.IsTouching(collider1))
            {
                Player1.takeDamage(Damage);
                _NMEattackAllow = false;
            }
        }
        else
        {
            if (!playerCollider.IsTouching(collider1))
            {
                _NMEattackAllow = true;
            }
        }
    }
}
