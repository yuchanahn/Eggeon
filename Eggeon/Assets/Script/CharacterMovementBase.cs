using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementBase : MonoBehaviour
{
    #region VAR - REFERANCES

    protected Rigidbody2D rigid;

    #endregion

    #region VAR - INSPECTOR

    [Header("Ground Detection")]
    [SerializeField] LayerMask rayHitLayer;
    [SerializeField] Vector2 charSize;
    [SerializeField] Vector2 boxCastSize;
    [SerializeField] float groundSnapLength_Inner = 0.15f;
    [SerializeField] float groundSnapLength_Outter = 0.15f;

    [Space]
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float jumpTime = 0.25f;
    [SerializeField] float fallGravity = -10f;

    #endregion

    #region VAR - MOVEMENT

    float jumpGravity = 0f;
    float jumpVelocity = 0f;

    #endregion

    #region VAR - INPUTS

    float inputDir_X = 0;
    bool jumpButtonPressed = false;

    #endregion

    #region VAR - STATES

    protected bool useGravity = true;
    protected bool grounded { private set; get; }



    protected bool jumping { private set; get; }

#endregion


#region METHOD - UNITY

private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.centerOfMass = Vector2.zero;
    }
    private void Update()
    {
        GetInput();
        Walk();
    }
    private void FixedUpdate()
    {
        DetectGround();
        Jump();
        ApplyGravity();
        ExternalForce();
    }

    #endregion

    #region METHOD - INPUT

    protected virtual void GetInput()
    {
        // Walk Input
        inputDir_X = Input.GetAxis("Horizontal");

        // Jump Input
        if (Input.GetKeyDown(KeyCode.Space)) jumpButtonPressed = true;
    }

    #endregion

    #region METHOD - MOVEMENT

    protected virtual void WhenGrounded()
    {
    }

    void DetectGround()
    {
        // Reset Grounded State
        grounded = false;

        // Box Cast
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxCastSize, 0, Vector2.down, 5, rayHitLayer);
        if (hit.collider == null) return;

        // Calculate hitMinDistance
        float hitMinDistance = (charSize.y / 2) - (boxCastSize.y / 2);

        // Check Grounded
        if (hit.distance > hitMinDistance - groundSnapLength_Inner &&
            hit.distance < hitMinDistance + groundSnapLength_Outter)
        {
            grounded = true;
            WhenGrounded();

            // Snap To the Ground
            if (hit.distance > hitMinDistance && !jumping)
            {
                SetPosition(y: hit.point.y + (charSize.y / 2));
                SetVelocity(y: 0);
            }
        }
    }
    protected virtual void Walk()
    {
        SetVelocity(x: inputDir_X * moveSpeed);
    }
    protected virtual void Jump()
    {
        if (jumpButtonPressed)
        {
            //if (grounded)
            {
                // Character is Jumping
                jumping = true;

                JumpProcess(jumpHeight, jumpTime);
            }

            // Reset Jump Input
            jumpButtonPressed = false;
        }

        // Character Is Not Jumping
        if (jumping && rigid.velocity.y <= 0)
        {
            jumping = false;
        }
    }

    protected virtual void JumpProcess(float height, float time)
    {
        // Set Jump Gravity
        jumpGravity = -(2 * height) / Mathf.Pow(time, 2);

        // Set Jump Velocity
        jumpVelocity = Mathf.Abs(jumpGravity * time);

        // Apply Jump Velocity
        SetVelocity(y: jumpVelocity);
    }
    protected virtual void ApplyGravity()
    {
        if (useGravity)
        {
            if (!jumping)
            {
                if (!grounded)
                {
                    SetVelocity(y: rigid.velocity.y + (fallGravity * Time.deltaTime));
                }
                else
                {
                    SetVelocity(y: 0);
                }
            }
            else
            {
                SetVelocity(y: rigid.velocity.y + (jumpGravity * Time.deltaTime));
            }
        }
        else
        {
            SetVelocity(y: 0);
        }
    }
    protected virtual void ExternalForce()
    {

    }

    #endregion

    #region METHOD - HELPER

    protected void SetVelocity(float x = float.NaN, float y = float.NaN)
    {
        if (float.IsNaN(x)) x = rigid.velocity.x;
        if (float.IsNaN(y)) y = rigid.velocity.y;

        rigid.velocity = new Vector2(x, y);
    }
    protected void SetPosition(float x = float.NaN, float y = float.NaN, float z = float.NaN)
    {
        if (float.IsNaN(x)) x = transform.position.x;
        if (float.IsNaN(y)) y = transform.position.y;
        if (float.IsNaN(z)) z = transform.position.z;

        transform.position = new Vector3(x, y, z);
    }

    #endregion
}
