
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerCharacter : MonoBehaviour
{
    Rigidbody2D rigid;

    [SerializeField] LayerMask rayHitLayer;
    [SerializeField] Vector2 charSize;
    [SerializeField] Vector2 boxCastSize;

    [SerializeField] lookme ch_;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float JumpForce = 10f;
    [SerializeField] float gravityForce = -10f;

    bool grounded = false;

    float inputDir_X = 0;
    bool jumpButtonPressed = false;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
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
    }

    void ApplyGravity()
    {
        if (!ch_.bInGround_)
        {
            SetVelocity(y: rigid.velocity.y + (gravityForce * Time.deltaTime));
        }
        else
        {
            SetVelocity(y: 0f);
        }
    }

    void GetInput()
    {
        // Walk Input
        inputDir_X = Input.GetAxis("Horizontal");

        // Jump Input
        if (Input.GetKeyDown(KeyCode.Space)) jumpButtonPressed = true;
    }
    void Walk()
    {
        SetVelocity(x: inputDir_X * moveSpeed);
    }
    void Jump()
    {
        if (jumpButtonPressed)
        {
            if (grounded) SetVelocity(y: JumpForce);

            jumpButtonPressed = false;
        }
    }

    void DetectGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxCastSize, 0, Vector2.down, 5, rayHitLayer);

        grounded = false;
        if (hit.collider == null) return;

        if (hit.distance > (charSize.y / 2) - (boxCastSize.y / 2) &&
            hit.distance < (charSize.y / 2) - (boxCastSize.y / 2) + 0.1f)
        {
            grounded = true;
        }
    }

    void SetVelocity(float x = float.NaN, float y = float.NaN)
    {
        if (float.IsNaN(x)) x = rigid.velocity.x;
        if (float.IsNaN(y)) y = rigid.velocity.y;

        rigid.velocity = new Vector2(x, y);
    }
}