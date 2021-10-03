using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum behaviourType
{
    STATIONARY,
    WANDERER,
    RUNNER
}



public abstract class NPC : MonoBehaviour
{
    public string dialogue;
    public float dialogeDelay;

    protected Rigidbody2D playerRigidbody;
    protected BoxCollider2D playerBoxCollider;

    protected bool grounded = true;
    protected bool alive = true;

    public abstract void resolveMovement();
    public abstract void ReactToDealth(Vector3 deathPosition);
    public abstract void ReactToJump(Vector3 jumpPosition);

    [Range(0,2000)]
    public float jumpHeight;



    protected virtual void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerBoxCollider = GetComponent<BoxCollider2D>();
        Invoke("sayDialogue", dialogeDelay);
        
    }

    public void sayDialogue()
    {
        if (dialogue != "" || dialogue == null)
        {

            GameObject newDialogue = Instantiate(Game.Instance.dialogePrefab, transform.position + Vector3.up, Quaternion.identity, null);
            newDialogue.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = dialogue;
            dialogue = "";
            AudioController.Instance.PlaySound(SoundFX.TALKING);
        }
    }




    private void Update()
    {
        if (Game.Instance.running && alive)
        {
            resolveMovement();
            CheckGrounded();

            if (playerRigidbody.velocity.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (playerRigidbody.velocity.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        
        

    }

    public void kill()
    {
        alive = false;
        playerBoxCollider.enabled = false;

        playerRigidbody.constraints = RigidbodyConstraints2D.None;
        playerRigidbody.AddForce(new Vector2(((float)Game.Instance.rand.NextDouble() + 1f) * Mathf.Sign((float)Game.Instance.rand.NextDouble() - 0.5f), (float)Game.Instance.rand.NextDouble() + 1f) * 50);
        playerRigidbody.angularVelocity = (Mathf.Sign((float)Game.Instance.rand.NextDouble() - 0.5f) * ((float)Game.Instance.rand.NextDouble() + 0.5f)) * 25;


        Invoke("cleanUpObj",2);

    }


    public void cleanUpObj()
    {
        Destroy(this.gameObject);
    }


    private void CheckGrounded()
    {
        //TODO Refactor with Overlap Box Test
        if (playerRigidbody.velocity.y > 0.01f)
        {
            grounded = false;
            return;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + (Vector3.right * 0.2f), Vector2.down, transform.localScale.y * 0.6f);
        foreach (var hit in hits)
        {
            if (!GameObject.ReferenceEquals(transform.gameObject, hit.transform.gameObject))
            {
                if (hit.transform.tag == "Ground")
                {
                    if (grounded == false)
                    {
                        Debug.Log(hit.transform.name);
                        Game.Instance.landedOnGround();
                    }
                    grounded = true;
                    return;
                }
                else if (hit.transform.tag == "Bouncable")
                {
                    if (grounded == false)
                    {
                        Debug.Log("Bounce");
                        playerRigidbody.AddForce(Vector2.up * jumpHeight * 1.75f);
                        AudioController.Instance.PlaySound(SoundFX.JUMP2);
                        hit.transform.GetComponent<NPC>().kill();
                        return;
                    }
                }
            }
        }
        hits = Physics2D.RaycastAll(transform.position + (Vector3.left * 0.2f), Vector2.down, transform.localScale.y * 0.6f);
        foreach (var hit in hits)
        {
            if (!GameObject.ReferenceEquals(transform.gameObject, hit.transform.gameObject))
            {
                if (hit.transform.tag == "Ground")
                {
                    if (grounded == false)
                    {
                        Debug.Log(hit.transform.name);
                        Game.Instance.landedOnGround();
                    }
                    grounded = true;
                    return;
                }
                else if (hit.transform.tag == "Bouncable")
                {
                    if (grounded == false)
                    {
                        Debug.Log("Bounce");
                        playerRigidbody.AddForce(Vector2.up * jumpHeight * 1.75f);
                        AudioController.Instance.PlaySound(SoundFX.JUMP2);
                        hit.transform.GetComponent<NPC>().kill();
                        return;
                    }
                }
            }
        }

        grounded = false;
    }












}
