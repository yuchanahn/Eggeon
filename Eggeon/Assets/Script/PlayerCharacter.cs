using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : CharacterMovementBase
{
    [SerializeField] float dashTime = 0.15f;
    [SerializeField] float dashSpeed = 10f;

    bool dashing = false;

    Coroutine coroutine_Dash = null;

    protected override void GetInput()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            coroutine_Dash = StartCoroutine(Dash());
        }

        base.GetInput();
    }

    protected override void Walk()
    {
        if (!dashing)
        {
            base.Walk();
        }
    }

    IEnumerator Dash()
    {
        float time = dashTime;

        dashing = true;
        useGravity = false;
        SetVelocity(x: Input.GetAxisRaw("Horizontal") * 20f);

        while (time > 0)
        {
            yield return new WaitForEndOfFrame();
            time -= Time.deltaTime;
        }

        dashing = false;
        useGravity = true;

        yield return null;
    }
}
