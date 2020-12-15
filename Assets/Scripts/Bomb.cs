using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public ParticleSystem poof;

    public AudioSource audio;
    public AudioClip boom;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddTorque(10);
        audio = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") == false)
        {
            Instantiate(poof,transform.position, Quaternion.Euler(90,0,0));
            
            if(collision.gameObject.CompareTag("Player") == true)
            {
                collision.gameObject.GetComponent<Player>().Damage(5);
            }

            FindObjectOfType<shortTank>().GetComponent<AudioSource>().PlayOneShot(boom);
            

            Debug.Log("KABOOM");
            Destroy(gameObject);

            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
