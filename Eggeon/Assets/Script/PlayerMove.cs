using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] float JumpSpeed;

    [SerializeField] float Gravity = -20f;

    [SerializeField] KeyCode[] MoveKey = new KeyCode[2];
    [SerializeField] KeyCode JumpKey;

    public Rigidbody2D rg_;

    float _xDir = 0f;
    public float _yspeed = 0f;

    public bool bInGround = true;
    public bool JumpPress;

    void Start()
    {
        rg_ = GetComponent<Rigidbody2D>();
    }


    public void SetVelocity(Vector2 v2)
    {
        rg_.velocity = v2;
    }

    void GETKey()
    {
        {
            if (bInGround && Input.GetKeyDown(JumpKey))
            {
                _yspeed = JumpSpeed;
                JumpPress = true;
            }
            if (!bInGround)
            {
                _yspeed += Gravity;
            }
        }
        {
            if (Input.GetKey(MoveKey[0]))
                _xDir = -1;
            else if (Input.GetKey(MoveKey[1]))
                _xDir = 1;
            else
                _xDir = 0;
        }
    }


    void Update()
    {   
        GETKey();

        rg_.velocity = new Vector2(_xDir * Speed * Time.deltaTime, _yspeed * Time.deltaTime);
    }
}
