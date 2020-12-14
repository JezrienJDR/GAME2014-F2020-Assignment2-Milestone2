using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    Vector3 recallPoint;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animr = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cap = GetComponent<CapsuleCollider2D>();

        recallPoint = transform.position;

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
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction = 'L';
            transform.localScale = new Vector3(-1, 1, 1);
            rb.AddForce(new Vector2(-RunForce, 0));
           
            animr.SetInteger("animState", 1);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction = 'R';
            transform.localScale = new Vector3(1, 1, 1);
            rb.AddForce(new Vector2(RunForce, 0));
            animr.SetInteger("animState", 1);
        }
        else
        {
            direction = 'N';
            animr.SetInteger("animState", 0);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            onGround = false;
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
            onGround = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Grounded!");

            onGround = true;
        }
        if (collision.gameObject.CompareTag("UpRamp"))
        {
            onRamp = true;
            Debug.Log("RAMP!");
        }
        if(collision.gameObject.CompareTag("DeadZone"))
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
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, JumpForce));
    }

    void Death()
    {
        transform.position = recallPoint;
    }
}
