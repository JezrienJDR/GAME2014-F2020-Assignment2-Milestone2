using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public enum PickupType
    {
        BIGSHOT,
        MISSILES,
        BIGSHIP
        
    }

    public PickupType type;

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Player"))
        {

            switch(type)
            {
                case PickupType.BIGSHOT:
                    {
                        collision.gameObject.GetComponent<ShipCon>().BigShot();
                        break;
                    }
                case PickupType.MISSILES:
                    {
                        collision.gameObject.GetComponent<ShipCon>().SwitchMissiles();

                        break;
                    }
                case PickupType.BIGSHIP:
                    {
                        collision.gameObject.GetComponent<ShipCon>().BigShip();

                        break;
                    }
            }

            gameObject.SetActive(false);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
