using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    SpriteRenderer sr;

    public Animator animr;

    public char direction = 'N';

    Rigidbody2D rb;

    CapsuleCollider2D cap;

    TilemapCollider2D ground;
    TilemapCollider2D ramps;

    public float JumpForce;
    public float RunForce;
    public float rampForce;

    bool onGround = false;

    bool onRamp = false;

    public Image healthBar;

    float health;
    float maxHealth = 180;

    Vector3 recallPoint;

    bool attackEnabled = true;

    public GameObject blast;

    public float blastSpeed;

    public Transform muzzle;

    public AudioClip boom;
    public AudioClip fire;

    Joystick joystick;

    float xDead = 0.2f;

    bool onPlatform = false;
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();

        sr = GetComponent<SpriteRenderer>();
        animr = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cap = GetComponent<CapsuleCollider2D>();

        health = maxHealth;

        recallPoint = transform.position;

        //muzzle = GetComponentInChildren<Transform>();

        foreach(TilemapCollider2D t in FindObjectsOfType<TilemapCollider2D>())
        {
            if(t.gameObject.CompareTag("Ground"))
            {
                ground = t;
            }
            if(t.CompareTag("UpRamp"))
            {
                ramps = t;
            }
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || joystick.Horizontal < -xDead)
        {
            direction = 'L';
            transform.localScale = new Vector3(-1, 1, 1);
           
            rb.AddForce(new Vector2(-RunForce, 0));
           
            animr.SetInteger("animState", 1);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || joystick.Horizontal > xDead)
        {
            direction = 'R';
            transform.localScale = new Vector3(1, 1, 1);
            rb.AddForce(new Vector2(RunForce, 0));
            animr.SetInteger("animState", 1);
        }
        else
        {
            //direction = 'N';
            animr.SetInteger("animState", 0);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            onGround = false;
        }

        if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            FireWeapon();
        }

        if(onRamp)
        {
            rb.AddForce(new Vector2(0, rampForce));
        }

        if(cap.IsTouching(ground))
        {
            onGround = true;
        }
        else
        {
            if (onPlatform == false)
            { 
                onGround = false;
            }
        }

        if(onGround == false)
        {
            if (onRamp == false)
            {
                animr.SetInteger("animState", 0);
            }
            rb.velocity *= new Vector2(0.995f, 1.0f);
        }
        else
        {
            
        }
    }

    public void FireWeapon()
    {
        if(attackEnabled)
        {
            GameObject b = Instantiate(blast, muzzle.position, Quaternion.identity);

            Vector2 force;

            if(direction == 'R')
            {
                //force = new Vector2(RunForce * 3, 0);
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(blastSpeed, 0);
            }
            else if(direction == 'L')
            {
                force = new Vector2(-RunForce * 3, 0);
                b.transform.rotation = Quaternion.Euler(0, 180, 0);
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(-blastSpeed, 0);
            }

            GetComponent<AudioSource>().PlayOneShot(fire);
            //b.GetComponent<Rigidbody2D>().AddForce(new Vector2(RunForce * 3, 0));
        
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Grounded!");

            onGround = true;
        }
        if (collision.gameObject.CompareTag("UpRamp"))
        {
            onRamp = true;
            //Debug.Log("RAMP!");
        }
        if (collision.gameObject.CompareTag("platform"))
        {
            onPlatform = true;
            onGround = true;
            //Debug.Log("RAMP!");
        }
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            Death();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("UpRamp"))
        {
            onRamp = false;
        }
        if (collision.gameObject.CompareTag("platform"))
        {
            onPlatform = false;
            //Debug.Log("RAMP!");
        }
    }

    public void Jump()
    {
        rb.AddForce(new Vector2(0, JumpForce));
    }

    public void EnableAttack()
    {
        attackEnabled = true;
    }

    public void boomSound()
    {
        GetComponent<AudioSource>().PlayOneShot(boom);
    }

    public void Damage(float d)
    {
        health -= d;

        //Debug.Log(health);

        healthBar.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, health);

        Debug.Log(health);

        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        transform.position = recallPoint;
        health = 180;
        healthBar.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, health);
    }
}
