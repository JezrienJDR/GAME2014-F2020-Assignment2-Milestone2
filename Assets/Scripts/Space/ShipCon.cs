using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ShipCon : MonoBehaviour
{
    float health;

    public PlayerProjectileManager Guns;

    [SerializeField]
    float RotSpeed = 180;

    [SerializeField]
    float impulse = 4;

    Rigidbody2D rb;

    [SerializeField]
    public GameObject Blast;

    [SerializeField]
    float fireInterval = 0.2f;

    float fireTimer = 0;

    float gunOffsetHorizontal = 0.4f;
    float gunOffsetVertical = 0.2f;

    bool fullImpulse = false;
    bool turnRight = false;
    bool turnLeft = false;

    public Image healthBar;

    bool missiles = false;

    public GameObject explosion;

    AudioSource gunFire;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        health = 180.0f;

        gunFire = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), RotSpeed * -1 * Time.deltaTime * Input.GetAxis("Horizontal"));

        rb.AddForce(transform.up * impulse * Input.GetAxis("Vertical"));

        if(fullImpulse)
        {
            rb.AddForce(transform.up * impulse);
        }

        if(turnLeft)
        {
            transform.Rotate(new Vector3(0, 0, 1), RotSpeed * 1 * Time.deltaTime);
        }

        if(turnRight)
        {
            transform.Rotate(new Vector3(0, 0, 1), RotSpeed * -1 * Time.deltaTime);
        }

        //if (Input.GetAxis("Fire1") > 0)
        //{
        //    Fire1();
        //}

        //if (Input.GetAxis("Fire2") > 0)
        //{
        //    Fire2();
        //}

        if (fireTimer < fireInterval)
        {
            fireTimer += Time.deltaTime;
        }
    
    }

    public void Damage(float d)
    {
        health -= d;

        //Debug.Log(health);

        healthBar.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, health);

        if(health <= 0)
        {
            gameObject.SetActive(false);
            rb.mass = 1000000;
            rb.drag = 1000000;
            GameObject b = Instantiate(explosion, transform.position, new Quaternion(0, 0, 0, 0));
            b.transform.localScale = new Vector3(3, 3, 3);

            FindObjectOfType<LevelManager>().EndGame();

        }
    }


    public void Fire1()
    {
        if (missiles == false)
        {

            if (fireTimer >= fireInterval)
            {
                Guns.GetShot(transform.position - transform.right * gunOffsetHorizontal + transform.up * gunOffsetVertical, transform.rotation);
                Guns.GetShot(transform.position + transform.right * gunOffsetHorizontal + transform.up * gunOffsetVertical, transform.rotation);

                fireTimer = 0;
            }
        }
        else
        {
            if (fireTimer >= fireInterval)
            {
                Guns.GetShot2(transform.position - transform.right * gunOffsetHorizontal + transform.up * gunOffsetVertical, transform.rotation);
                Guns.GetShot2(transform.position + transform.right * gunOffsetHorizontal + transform.up * gunOffsetVertical, transform.rotation);

                fireTimer = 0;
            }
        }

        //gunFire.PlayOneShot();
        gunFire.Play();
    }

    public void Fire2()
    {
        if (fireTimer >= fireInterval)
        {
            Guns.GetShot2(transform.position - transform.right * gunOffsetHorizontal + transform.up * gunOffsetVertical, transform.rotation);
            Guns.GetShot2(transform.position + transform.right * gunOffsetHorizontal + transform.up * gunOffsetVertical, transform.rotation);

            fireTimer = 0;
        }
    }


    public void FireEngines()
    {
        fullImpulse = true;
    }

    public void KillEngines()
    {
        fullImpulse = false;
    }

    public void TurnRight()
    {
        turnRight = true;
    }
    public void unRight()
    {
        turnRight = false;
    }

    public void TurnLeft()
    {
        turnLeft = true;
    }
    public void unLeft()
    {
        turnLeft = false;
    }


    public void BigShip()
    {
        transform.localScale = new Vector3(2, 2, 2);
        gunOffsetHorizontal *= 2;
        gunOffsetVertical *= 2;


        impulse *= 0.6f;
        //GetComponent<CapsuleCollider2D>()
    }

    public void SwitchMissiles()
    {
        missiles = true;
        fireTimer *= 2;
    }

    public void BigShot()
    {
        Guns.scaleModifier *= 2;
    }
}
