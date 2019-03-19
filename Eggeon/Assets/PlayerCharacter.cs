using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : CharacterMovementBase
{
    [SerializeField] float dashTime = 0.15f;
    [SerializeField] float swordDashTime = 0.15f;

    [SerializeField, Range(0, 100)]
    float swordDashSpeed = 10f;

    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float airJumpSpeed = 10f;

    [SerializeField] int airJumpCount = 0;
    int currentAirJumpCount = 0;
    Vector2 swordDashVelocity;
    float dashVelocity;

    bool dashing = false;
    public bool swordDashing = false;


    protected override void GetInput()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetKeyDown(KeyCode.LeftShift) && !dashing)
        {
            StartCoroutine(Dash());
        }

        base.GetInput();
    }


    IEnumerator Dash()
    {
        float time = dashTime;

        dashing = true;
        useGravity = false;
        dashVelocity = Input.GetAxisRaw("Horizontal") * 20f;

        while (time > 0)
        {
            yield return new WaitForEndOfFrame();
            time -= Time.deltaTime;
        }

        dashing = false;
        useGravity = true;

        yield return null;
    }

    protected override void ExternalForce()
    {
        if (dashing)
            SetVelocity(x: dashVelocity);

        if (swordDashing)
        {
            SetVelocity(swordDashVelocity.x, swordDashVelocity.y);
        }
    }

    IEnumerator CSwordDash(Vector2 vel)
    {
        float time = swordDashTime;

        useGravity = false;
        swordDashing = true;
        Vector2 v2 = vel * swordDashSpeed;

        while (time > 0)
        {
            swordDashVelocity = v2 * Mathf.Clamp01(time);
            yield return new WaitForEndOfFrame();
            time -= Time.deltaTime;
        }

        
        useGravity = true;
        swordDashing = false;

        yield return null;
    }

    protected override void JumpProcess(float height, float time)
    {
        if (currentAirJumpCount < airJumpCount)
        {
            base.JumpProcess(height, time);
            currentAirJumpCount++;
        }
    }

    protected override void WhenGrounded()
    {
        currentAirJumpCount = 0;
    }

    public void SwordDash(Vector2 velocity)
    {
        StartCoroutine(CSwordDash(velocity));
    }
}
