using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningNPC : NPC
{
    [Range(0, 100)]
    public float runSpeed;
    private Vector3 lastDeath = Vector3.zero;

    public override void ReactToDealth(Vector3 deathPosition)
    {
        lastDeath = deathPosition;
    }

    public override void ReactToJump(Vector3 jumpPosition)
    {
        
    }

    public override void resolveMovement()
    {
        if (lastDeath != Vector3.zero)
        {
            dialogue = "";
            if (grounded)
            {
                playerRigidbody.velocity = new Vector2(Mathf.Sign(transform.position.x - lastDeath.x) * runSpeed, playerRigidbody.velocity.y);
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (lastDeath != Vector3.zero)
        {

            Gizmos.DrawSphere(lastDeath, 0.2f);

        }


    }



}
