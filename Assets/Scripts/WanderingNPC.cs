using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingNPC : NPC
{
    [Range(0,3)]
    public float wanderSpeed;
    [Range(0,20)]
    public float wanderDistance;

    private float lastDelta;
    private Vector3 startingPos;
    float awakeTime;


    protected override void Awake()
    {
        base.Awake();
        startingPos = transform.position;
        awakeTime = Time.time;
    }

    public override void resolveMovement()
    {
        if (grounded)
        {

            float deltaPos = (Mathf.Sin((Time.time-awakeTime) * wanderSpeed)) * wanderDistance;
            playerRigidbody.velocity = new Vector3(deltaPos, playerRigidbody.velocity.y, 0);

            lastDelta = deltaPos;
        }

    }

    public override void ReactToDealth(Vector3 deathPosition)
    {
        
    }

    public override void ReactToJump(Vector3 jumpPosition)
    {
        
    }
}
