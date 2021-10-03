using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//DEPRECATED
public class SideWanderer : MonoBehaviour
{
    Vector3 startingPos;
    [Range(0,5)]
    public float wanderDistance;
    [Range(0,2)]
    public float wanderSpeed;
    private float lastDelta = 0;

    private void Awake()
    {
        startingPos = transform.position;
    }

    private void Update()
    {
        float deltaPos = (Mathf.Sin(Time.time * wanderSpeed)) * wanderDistance;
        transform.position = startingPos + Vector3.right * deltaPos;

        if (deltaPos - lastDelta < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        lastDelta = deltaPos;

    }


}
