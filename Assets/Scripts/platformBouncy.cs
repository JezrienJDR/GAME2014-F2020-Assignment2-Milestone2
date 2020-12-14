using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformBouncy : MonoBehaviour
{
    public float speed;
    public float width;

    Vector3 home;
    Vector3 target;

    // Start is called before the first frame update

    GameObject player;

    public float JumpForce;

    void Start()
    {
        home = transform.position;
        target = new Vector3(home.x, home.y + width, home.z);

        player = null;

        StartCoroutine("moveDown");


    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator moveUp()
    {
        while (transform.position.y < target.y)
        {
            if (player != null)
            {
                player.transform.Translate(new Vector3(0, speed, 0));
            }
            transform.Translate(new Vector3(0, speed,0));
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine("moveDown");
    }

    IEnumerator moveDown()
    {
        while (transform.position.y > home.y)
        {
            if (player != null)
            {
                player.transform.Translate(new Vector3(0, -speed, 0));
            }
            transform.Translate(new Vector3(0, -speed, 0));
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine("moveUp");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            if(player.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpForce));
            }

            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, JumpForce);
            //player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpForce));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }
}
