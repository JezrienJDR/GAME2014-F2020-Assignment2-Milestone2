using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class shortTank : MonoBehaviour
{

    public float xForce;
    public float rampForce;

    Rigidbody2D rb;

    bool onRamp;

    TilemapCollider2D ramps;
    TilemapCollider2D ground;

    BoxCollider2D box;

    public Transform front;
    public Transform back;

    public float SenseDistance;

    Vector2 down;

    GameObject player;

    public GameObject bombTemplate;

    GameObject bomb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();

        foreach (TilemapCollider2D t in FindObjectsOfType<TilemapCollider2D>())
        {
            if (t.gameObject.CompareTag("Ground"))
            {
                ground = t;
            }
            if (t.CompareTag("UpRamp"))
            {
                ramps = t;
            }
        }

        down = new Vector2(0, -1);

        if (xForce > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        player = FindObjectOfType<Player>().gameObject;
        rb.velocity = new Vector2(xForce, 0);

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(front.position.x, front.position.y), down);

        //RaycastHit2D hitFront = Physics2D.Raycast(new Vector2(front.position.x, front.position.y), down);

        //RaycastHit2D hitBack = Physics2D.Raycast(new Vector2(back.position.x, back.position.y), down);


        //if (hit.collider.gameObject.CompareTag("Enemy"))
        //{
        //    Debug.Log("RAYCAST HIT SELF");
        //}

        if (hit.collider.gameObject.CompareTag("UpRamp"))
        {
            if (xForce > 0)
            {
                if (rb.velocity.y > 0)
                {
                    transform.rotation = new Quaternion(0, 0, 1, 4);
                }
                else
                {
                    transform.rotation = new Quaternion(0, 0, 1, -4);
                }
            }
            else if (xForce < 0)
            {
                if (rb.velocity.y > 0)
                {
                    transform.rotation = new Quaternion(0, 0, 1, -4);
                }
                else
                {
                    transform.rotation = new Quaternion(0, 0, 1, 4);
                }
            }
            //rb.AddForce(new Vector2(xForce, rampForce));
        }
        if (hit.transform.gameObject.CompareTag("Ground"))
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        rb.AddForce(new Vector2(xForce, 0));

        if (box.IsTouching(ramps))
        {
            rb.AddForce(new Vector2(xForce * 2, rampForce));
        }

        if (rb.velocity.x == 0)
        {
            //Debug.Log("Dislodging");
            //rb.AddForce(new Vector2(xForce * 2, rampForce * 200));
            rb.AddForce(new Vector2(0, rampForce * 200));
        }


        //if (hitFront.transform.gameObject.GetComponent<TilemapCollider2D>() == ramps && hitBack.transform.gameObject.GetComponent<TilemapCollider2D>() == ground)
        //{
        //    if(hitFront.distance > hitBack.distance)
        //    {
        //        transform.rotation = new Quaternion(0, 0, 1, -4);
        //        rb.velocity = new Vector2(xForce, -0.5f);
        //    }
        //    else
        //    {
        //        transform.rotation = new Quaternion(0, 0, 1, 4);
        //        rb.velocity = new Vector2(xForce, 0.5f);
        //    }

        //}
        //else if(hitFront.transform.gameObject.GetComponent<TilemapCollider2D>() == ground && hitBack.transform.gameObject.GetComponent<TilemapCollider2D>() == ground)
        //{
        //    transform.rotation = new Quaternion(0, 0, 0, 0);
        //    rb.velocity = new Vector2(xForce, 0);
        //}

        if (Vector3.Distance(transform.position, player.transform.position) < SenseDistance)
        {
            if (player.transform.position.x > transform.position.x)
            {
                if (xForce > 0)
                {
                    Attack();
                }
            }
            else if (player.transform.position.x < transform.position.x)
            {
                if (xForce < 0)
                {
                    Attack();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            TurnAround();
        }
    }

    public void Damage()
    {
        rb.AddForce(down * 10);
    }
    void TurnAround()
    {
        xForce = xForce * -1;

        if(xForce > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
           
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        rb.velocity = new Vector2(xForce, 0);
    }

    void Attack()
    {
        if (bomb == null)
        {
            //Debug.Log("Attacking!");

            bomb = Instantiate(bombTemplate, transform.position, new Quaternion(0, 0, 0, 0));

            bomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce * 25, 300));
        }
    }
}
