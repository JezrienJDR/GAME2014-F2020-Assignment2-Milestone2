using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformRetract : MonoBehaviour
{


    public float speed;
    public float width;

    Vector3 home;
    Vector3 target;

    public float wait;
    public float delay;

    // Start is called before the first frame update

    GameObject player;

    void Start()
    {
        home = transform.position;
        target = new Vector3(home.x + width, home.y, home.z);

        player = null;

        StartCoroutine("InitialWait");

        



    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator InitialWait()
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine("moveRight");
    }

    IEnumerator moveRight()
    {
        while (transform.position.x < target.x)
        {
            if (player != null)
            {
                player.transform.Translate(new Vector3(speed, 0, 0));
            }
            transform.Translate(new Vector3(speed, 0, 0));
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(wait);
        StartCoroutine("moveLeft");
    }

    IEnumerator moveLeft()
    {
        while (transform.position.x > home.x)
        {
            if (player != null)
            {
                player.transform.Translate(new Vector3(-speed, 0, 0));
            }
            transform.Translate(new Vector3(-speed, 0, 0));
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(wait);
        StartCoroutine("moveRight");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
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
