using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{

    bool useable = false;
    bool used = false;
    float progress = 0;
    float progressNeed = 1.5f;


    public GameObject infographic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!used)
        {
            if (Game.Instance.running)
            {
                if (useable)
                {
                    if (Input.GetKey(KeyCode.F))
                    {
                        progress += Time.deltaTime;
                        if (progress >= progressNeed)
                        {
                            used = true;
                            useable = false;
                            infographic.SetActive(false);
                            Game.Instance.resetJumps();
                        }

                    }
                }
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !used)
        {
            useable = true;
            infographic.SetActive(true);
            Debug.Log("ALTAR");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !used)
        {
            useable = false;
            infographic.SetActive(false);
        }

    }

}
