using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCon1 : MonoBehaviour
{

    State currentState;

    public GameObject player;

    public GameObject projectile;

    Rigidbody2D rb;

    public float RotSpeed;
    public float impulse;

    public float attackTimer;
    float timeElapsed;

    public float visionAngle;
    public float visionRange;

    bool hasSeenPlayer = false;


    float gunOffsetVertical = 1.7f;

    EnemyProjectileManager guns;

    public float health;

    AudioSource gunFire;

    public GameObject explosion;
    enum State
    {
        IDLE,
        KILL
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.IDLE;
        player = FindObjectOfType<ShipCon>().gameObject;
        rb = GetComponent<Rigidbody2D>();

        guns = FindObjectOfType<EnemyProjectileManager>();

        gunFire = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if(currentState == State.IDLE)
        {
            FireEngines();
            CW();

            if(CheckPlayerSeen())
            {
                currentState = State.KILL;
            }
            
        }

        if (currentState == State.KILL)
        {
            timeElapsed += Time.deltaTime;

            float angle = GetAngleToPlayer();


            if(angle < -1)
            {
                CCW();
            }
            else if(angle > 1)
            {
                CW();
            }

            if (Mathf.Abs(GetAngleToPlayer()) < 90)
            {
                if (GetVectorToPlayer().magnitude > 5)
                {
                    FireEngines();
                }
            }

            if (timeElapsed >= attackTimer)
            {
                FireWeapon();


                timeElapsed = 0;
            }
        }

    }

    public void Damage(float d)
    {
        health -= d;

        if(health <= 0)
        {
            FindObjectOfType<LevelManager>().ScratchOne();
            GameObject b = Instantiate(explosion, transform.position, new Quaternion(0, 0, 0, 0));
            b.transform.localScale = new Vector3(3, 3, 3);
            Destroy(gameObject);
        }
    }

    Vector3 GetVectorToPlayer()
    {
        Vector3 playerVec = player.transform.position - transform.position;

        return playerVec;
    }

    float GetAngleToPlayer()
    {
        float angle = Vector3.SignedAngle(transform.up, GetVectorToPlayer(), new Vector3(0,0,1));

        return angle;
    }

    bool CheckPlayerSeen()
    {
        Vector3 playerVec = GetVectorToPlayer();

        float playerAngle = GetAngleToPlayer();

        if(Mathf.Abs(playerAngle) <= visionAngle)
        {
            if(playerVec.magnitude <= visionRange)
            {
                return true;
            }
        }

        return false;

    }

    void CW()
    {
        transform.Rotate(new Vector3(0, 0, 1), RotSpeed * 1 * Time.deltaTime);
  
    }

    void CCW()
    {
        transform.Rotate(new Vector3(0, 0, 1), RotSpeed * -1 * Time.deltaTime);
        
    }

    void FireEngines()
    {
        rb.AddForce(transform.up * impulse);
    }

    void FireWeapon()
    {
        //GameObject bullet = Instantiate(projectile, transform, true);

        gunFire.Play();

        guns.GetShot(transform.position + transform.up * gunOffsetVertical, transform.rotation);

    }
}
