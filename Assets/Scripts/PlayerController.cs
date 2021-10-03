using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    BoxCollider2D playerBoxCollider;

    public float movementSpeed;
    public float jumpHeight;
    public float bounceHeight;

    private bool grounded = true;
    private bool jumped = false;

    private Animator anim;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerBoxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Game.Instance.running)
        {
            HandleInput();
            CheckGrounded();
        }

        if (Game.Instance.levelCompleted)
        {
            playerBoxCollider.enabled = false;
            playerRigidbody.velocity = Vector3.right*movementSpeed*0.5f;
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
            anim.SetBool("Walking", true);
        }

    }

    private void Start()
    {
        enteredLevel();
    }






    private void CheckGrounded()
    {
        //TODO Refactor with Overlap Box Test
        if (playerRigidbody.velocity.y > 0.01f)
        {
            grounded = false;
            return;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + (Vector3.right*0.2f), Vector2.down, 0.51f);
        foreach (var hit in hits)
        {
            if (hit.transform.tag == "Ground")
            {
                if (grounded == false)
                {
                    Game.Instance.landedOnGround();
                    Game.Instance.alertPlayerJump(transform.position);
                    jumped = false;
                }
                grounded = true;

                return;
            }
            else if (hit.transform.tag == "Bouncable")
            {
                if (grounded == false)
                {
                    Debug.Log("Bounce");
                    playerRigidbody.AddForce(Vector2.up * bounceHeight);
                    AudioController.Instance.PlaySound(SoundFX.JUMP2);
                    NPC hitNPC = hit.transform.GetComponent<NPC>();
                    hitNPC.kill();
                    Game.Instance.alertNPCDeath(hit.transform.position);
                    return;
                }
            }
        }
        hits = Physics2D.RaycastAll(transform.position + (Vector3.left * 0.2f), Vector2.down, 0.51f);
        foreach (var hit in hits)
        {
            if (hit.transform.tag == "Ground")
            {
                if (grounded == false)
                {
                    Game.Instance.landedOnGround();
                    Game.Instance.alertPlayerJump(transform.position);
                    jumped = false;
                }
                grounded = true;

                return;
            }
            else if (hit.transform.tag == "Bouncable")
            {
                if (grounded == false)
                {
                    Debug.Log("Bounce");
                    playerRigidbody.AddForce(Vector2.up * bounceHeight);
                    AudioController.Instance.PlaySound(SoundFX.JUMP2);
                    NPC hitNPC = hit.transform.GetComponent<NPC>();
                    hitNPC.kill();
                    Game.Instance.alertNPCDeath(hit.transform.position);
                    return;
                }
            }
        }

        grounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Goal")
        {
            
            Game.Instance.CompleteLevel();
        }
        else if (collision.transform.tag == "Death")
        {

            Debug.Log("Died");
            AudioController.Instance.PlaySound(SoundFX.HURT1);
            StartCoroutine(Game.Instance.DeathWaitReset(2));
        }
    }

    
    public void ShuffleNoise()
    {
        //called by animation event
        if (grounded)
        {
            AudioController.Instance.PlaySound(SoundFX.SHUFFLE);
        }

    }

    private void HandleInput()
    {
        bool animateFrame = false;
        Vector2 movementForce = Vector2.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movementForce.x += -movementSpeed;
            animateFrame = true;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movementForce.x += movementSpeed;
            animateFrame = true;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded && !jumped)
            {
                animateFrame = false;
                //JUMP
                jumped = true;
                playerRigidbody.AddForce(Vector2.up * jumpHeight);
                AudioController.Instance.PlaySound(SoundFX.JUMP1);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            animateFrame = false;
            StartCoroutine(Game.Instance.DeathWaitReset(0.25f));
        }



        //Resolve
        if (movementForce != Vector2.zero)
        {
            playerRigidbody.AddForce(movementForce * Time.deltaTime*100);

            if (playerRigidbody.velocity.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                
            }

        }


        if (!grounded || jumped || !Game.Instance.running)
        {
            animateFrame = false;
        }

        anim.SetBool("Walking", animateFrame);


    }

    public void enteredLevel()
    {
        Game.Instance.startLevel();
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + (Vector3.right * 0.2f), 0.1f);
        Gizmos.DrawSphere(transform.position - (Vector3.right * 0.2f), 0.1f);
    }


}
