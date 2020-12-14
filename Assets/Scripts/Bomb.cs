using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public ParticleSystem poof;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddTorque(10);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") == false)
        {
            Instantiate(poof,transform.position, Quaternion.Euler(90,0,0));
            
            Debug.Log("KABOOM");
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
