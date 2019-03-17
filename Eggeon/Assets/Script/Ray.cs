using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    private GameObject target;
    [SerializeField] LayerMask mask_;


    Vector2 _BoxCastSize;
    PlayerMove PlayerMove_;



    private void Start()
    {
        _BoxCastSize = GetComponent<CapsuleCollider2D>().size;
        _BoxCastSize.y = 0.2f;

        PlayerMove_ = GetComponent<PlayerMove>();
    }

    void FixedUpdate()
    {
        GetClickedObject();
    }
    private void GetClickedObject()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, _BoxCastSize, 0, Vector2.down, 5, mask_);

        if (hit.collider != null)
        {
            if (hit.distance > 0.3f && hit.distance < 0.5f)
            {
                PlayerMove_.bInGround = true;
            }
            else
            {
                PlayerMove_.bInGround = false;
            }
        }

        if (PlayerMove_.JumpPress)
        {
            PlayerMove_.SetVelocity(new Vector2(PlayerMove_.rg_.velocity.x, PlayerMove_._yspeed * Time.deltaTime));
        }
        PlayerMove_.JumpPress = false;
    }
}