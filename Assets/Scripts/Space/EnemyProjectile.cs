using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float xBoundary;
    public float yBoundary;

    public int id;

    public float damage;

    public EnemyProjectileManager Guns;

    [SerializeField]
    float speed;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.AddForce(transform.up * speed);
        if (rb == null)
        {
            Debug.Log("RigidBody ref is null");
        }
        //Fire();

        Guns = FindObjectOfType<EnemyProjectileManager>();
    }

    public void Fire()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.GetComponent<ShipCon>() != null)
        {
            collision.gameObject.GetComponent<ShipCon>().Damage(damage);
            Guns.Reload(gameObject);
        }

        //if (collision.gameObject.tag != "Enemy")
        //{

        //}

        Guns.Reload(gameObject);
    }

   

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
    }

    void CheckBounds()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (x > xBoundary || x < -xBoundary || y > yBoundary || y < -yBoundary)
        {
           

            Guns.Reload(gameObject);
        }
    }
}
