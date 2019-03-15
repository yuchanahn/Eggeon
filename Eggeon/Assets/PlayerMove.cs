using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float Speed = 1.0f;
    [SerializeField] float JumpSpeed = 5.0f;

    [SerializeField] float Gravity = -20f;

    [SerializeField] KeyCode[] MoveKey = new KeyCode[2];
    [SerializeField] KeyCode JumpKey;

    Rigidbody2D rg;

    float _xspeed = 0f;
    float _yspeed = 0f;
    public float _gravity = 0;

    [SerializeField] bool bInGround = true;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
         
    }


    void GETKey()
    {
        {
            _gravity = 0;
            if (Input.GetKeyDown(JumpKey))
            {
                if (bInGround)
                {
                    _gravity = 10;
                }
            }
            else
            {
                if (!bInGround)
                {
                    _gravity = Gravity * Time.deltaTime;
                }
            }

            _yspeed = _gravity;
        }
        {
            if (Input.GetKey(MoveKey[0]))
                _xspeed = -1;
            else if (Input.GetKey(MoveKey[1]))
                _xspeed = 1;
            else
                _xspeed = 0;
        }
    }


    void Update()
    {
        GETKey();
        float MovePosX = transform.position.x + _xspeed * Speed * Time.deltaTime;
        float MovePosY = transform.position.y + _yspeed * Time.deltaTime;
        Vector2 MovePos = new Vector2(MovePosX , MovePosY);

        rg.MovePosition(MovePos);
    }
}
