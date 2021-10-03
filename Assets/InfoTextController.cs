using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTextController : MonoBehaviour
{

    public GameObject textDisplay;
    bool used = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.tag == "Player")
        {
            if (!used)
            {
                textDisplay.GetComponent<Animator>().Play("TextFadeIn");
                used = true;
                Invoke("cleanUpText", 15);
            }
        }

    }

    public void cleanUpText()
    {
        Destroy(textDisplay);
    }







}
