using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public GameObject player;
    public float glideSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Game.Instance.running)
        {
            Vector2 deltaXY =  player.transform.position - transform.position;
            glideSpeed = Mathf.Lerp(0, 1, deltaXY.sqrMagnitude);
            transform.Translate((deltaXY * glideSpeed * Time.deltaTime));
        }
    }
}
