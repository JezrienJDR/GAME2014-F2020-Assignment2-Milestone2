using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformCollapse : MonoBehaviour
{
    public float speed;
    public float width;

    Vector3 home;
    Vector3 target;

    // Start is called before the first frame update

    GameObject player;

    public float JumpForce;
    public float delay;
    public float fallDelay;
    void Start()
    {
        home = transform.position;
        target = new Vector3(home.x, home.y + width, home.z);

        player = null;



        delay = Random.Range(0.0f, 1.0f);

        StartCoroutine("InitialWait");


    }

    IEnumerator InitialWait()
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine("moveDown");
    }


    IEnumerator Shrink()
    {
        yield return new WaitForSeconds(fallDelay);
        while(transform.localScale.x > 0.01f)
        {
            transform.localScale *= 0.99f;
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.SetActive(false);
        yield return new WaitForSeconds(5.0f);
        gameObject.SetActive(true);
        transform.localScale = new Vector3(1, 1, 1);
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
            transform.Translate(new Vector3(0, speed, 0));
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

            StartCoroutine("Shrink");
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
