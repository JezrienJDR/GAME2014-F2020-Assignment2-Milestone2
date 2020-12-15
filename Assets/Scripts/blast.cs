using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blast : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<shortTank>().Damage();
        }

        if (collision.gameObject.CompareTag("Player") == false)
        {
            Instantiate(FindObjectOfType<ParticleSystem>(), transform.position, Quaternion.Euler(90,0,0));
            FindObjectOfType<Player>().boomSound();
            Destroy(gameObject);
        }

    }
}
