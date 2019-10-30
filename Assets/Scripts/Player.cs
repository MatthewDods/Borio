using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour

{
    public int damage;
    public int health;
    public int exp;
    public float timer;
    public float score;

    public float WalkSpeed;
    public float JumpForce;
    public AnimationClip _walk, _jump;
    public Animation _Legs;
    public Transform _Blade;
    public Camera cam;
    public bool mirror;
    public Vector3 startPos;
    public Collider2D collider1, weaponCollider;
    public bool grounded;

    private bool _canJump, _canWalk;
    private bool alive;
    private bool _isAttack;
    private bool _isWalk, _isJump;
    private float rot, _startScale;
    private Rigidbody2D rig;
    private Vector2 _inputAxis;

    void Start()
    {
        alive = true;
        timer = 0;
        collider1 = gameObject.GetComponent<Collider2D>();
        weaponCollider = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Collider2D>();

        startPos = transform.position;
        rig = gameObject.GetComponent<Rigidbody2D>();
        _startScale = transform.localScale.x;

        GameObject[] grassColliders = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject collider in grassColliders)
        {
            Physics2D.IgnoreCollision(weaponCollider, collider.GetComponent<Collider2D>());
        }

        health = GlobalControl.Instance.health;
        damage = GlobalControl.Instance.damage;
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }

    public void SavePlayer()
    {
        GlobalControl.Instance.health = health;
        GlobalControl.Instance.damage = damage;
        GlobalControl.Instance.exp = exp;
    }

    public void LevelUp()
    {
        health += 45;
        damage += 20;
        exp = exp - 200;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (alive)
        {
            if (grounded)
            {
                _canJump = true;
                _canWalk = true;
            }
            else _canJump = false;

            _inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (_inputAxis.y > 0 && _canJump)
            {
                _canWalk = false;
                _isJump = true;
            }
        }
    }

    public int getDamage()
    {
        return damage;
    }

    void FixedUpdate()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        if (health <= 0)
        {
            GameObject.Find("UICanvas").GetComponent<UI>().changeDialogue("You have died. Restarting in 3...");
            Tube.Restart();
            GameObject.Find("PC").transform.position = startPos;
        }

        if (exp >= 200)
        {
            LevelUp();
        }

        Vector3 dir = cam.ScreenToWorldPoint(Input.mousePosition) - _Blade.transform.position;
        dir.Normalize();

        if (cam.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x + 0.2f)
            mirror = false;
        if (cam.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x - 0.2f)
            mirror = true;

        if (!mirror)
        {
            rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.localScale = new Vector3(_startScale, _startScale, 1);
            _Blade.transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
        }
        if (mirror)
        {
            rot = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
            transform.localScale = new Vector3(-_startScale, _startScale, 1);
            _Blade.transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
        }

        if (_inputAxis.x != 0)
        {
            rig.velocity = new Vector2(_inputAxis.x * WalkSpeed * Time.deltaTime, rig.velocity.y);

            if (_canWalk)
            {
                _Legs.clip = _walk;
                _Legs.Play();
            }
        }
        else
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
        }

        if (_isJump)
        {
            rig.AddForce(new Vector2(0, JumpForce));
            _Legs.clip = _jump;
            _Legs.Play();
            _canJump = false;
            _isJump = false;
        }

    }

    public void Reset()
    {
        alive = true;
        health = 100;
        damage = 25;
        timer = 0;
        score = 0;
    }

    public bool IsMirror()
    {
        return mirror;
    }

}
