using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushNPC : NPC
{
    [Range(0, 100)]
    public float runSpeed;
    [Range(0, 20)]
    public float pushDistance;
    private Vector3 lastJump = Vector3.zero;

    public override void ReactToDealth(Vector3 deathPosition)
    {
        
    }

    public override void resolveMovement()
    {
        if (lastJump != Vector3.zero)
        {
            dialogue = "";
            if (Mathf.Abs(transform.position.x - lastJump.x) > pushDistance)
            {
                lastJump = Vector3.zero;
            }
            else
            {
                if (grounded)
                {
                    playerRigidbody.velocity = new Vector2(Mathf.Sign(transform.position.x - lastJump.x) * runSpeed, playerRigidbody.velocity.y);
                }
            }



        }
    }


    public override void ReactToJump(Vector3 jumpPosition)
    {
        lastJump = jumpPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(lastJump, 0.5f);
        Gizmos.DrawWireSphere(transform.position, pushDistance);
    }


}
