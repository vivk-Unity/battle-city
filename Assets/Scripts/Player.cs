using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovementController {
    float h, v;

    Rigidbody2D rb2d;
    Animator animator;
    WeaponController wc;

    private static float _h = 0.0F;
    private static float _v = 0.0F;

    private static bool _fire = false;

    private int State
    {
        get { return animator.GetInteger("State"); }
        set { animator.SetInteger("State", value); }
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        wc = GetComponentInChildren<WeaponController>();
    }
    private void FixedUpdate()
    {
        if (h != 0 && !isMoving)
        {
            StartCoroutine(MoveHorizontal(h, rb2d));
        }
        else if (v != 0 && !isMoving)
        {
            StartCoroutine(MoveVertical(v, rb2d));
        }
    }
    void Update()
    {
        if (_h != 0.0F || _v != 0.0F)
        {
            h = _h;
            v = _v;
        }
        else
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            wc.Fire();
        }

        if (_fire)
        {
            wc.Fire();
            _fire = false;
        }

        if (isMoving) State = 1;
        else State = 0;
    }

    // Mobile

    public void Fire()
    {
        _fire = true;
    }

    public void InputMobileHorizontal(int h)
    {
        _h = h;
    }

    public void InputMobileVertical(int v)
    {
        _v = v;
    }

}
