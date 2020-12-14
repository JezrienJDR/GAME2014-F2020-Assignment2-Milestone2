using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Sprite s0;
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;

    Sprite mySprite;

    int frameCount;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(frameCount)
        {
            case 0:
                {
                    mySprite = s0;
                    break;
                }
            case 1:
                {
                    mySprite = s1;
                    break;
                }
            case 2:
                {
                    mySprite = s2;
                    break;
                }
            case 3:
                {
                    mySprite = s3;
                    break;
                }
            case 4:
                {
                    mySprite = s4;
                    break;
                }
            case 5:
                {
                    Destroy(gameObject);
                    break;
                }
        }

        frameCount++;
    }

}
