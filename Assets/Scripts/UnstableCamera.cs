using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableCamera : MonoBehaviour
{

    Vector3 cameraEulerTargert = Vector3.zero;

    public void cameraShake()
    {
        cameraEulerTargert = new Vector3(0, 0, (float)Game.Instance.rand.NextDouble() - 0.5f);
        transform.rotation = Quaternion.Euler(cameraEulerTargert);


    }

    public void deathShake()
    {
        GetComponent<Animator>().Play("CamShake");
    }



    private void Update()
    {



    }

}
