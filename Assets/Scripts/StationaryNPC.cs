using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryNPC : NPC
{
    public override void ReactToDealth(Vector3 deathPosition)
    {
        //stationaries do nothing
    }

    public override void ReactToJump(Vector3 jumpPosition)
    {
        
    }

    public override void resolveMovement()
    {
        //stationaries do nothing
    }
}
