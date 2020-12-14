using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int score = 0;
    int level = 0;

    public GameObject enemyBase;

    public float xBound = 42;
    public float yBound = 25;

    public int enemiesPerLevel = 3;

    List<GameObject> Enemies;

    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI levelText;

    int enemyCount;

    public GameObject pickup;



    void Start()
    {
        StartLevel();

    }

    // Update is called once per frame
    void UpdateText()
    {
        scoreText.text = score.ToString();
        levelText.text = level.ToString();
    }


    void StartLevel()
    {
        level++;

        UpdateText();

        GameObject p = Instantiate(pickup, new Vector3(0, 0, -1), new Quaternion(0, 0, 0, 0));

        int pickupType = Random.Range(0, 2);
        switch(pickupType)
        {
            case 0:
                {
                    p.GetComponent<Pickup>().type = Pickup.PickupType.BIGSHIP;
                    break;
                }
            case 1:
                {
                    p.GetComponent<Pickup>().type = Pickup.PickupType.BIGSHOT;
                    break;
                }
            case 2:
                {
                    p.GetComponent<Pickup>().type = Pickup.PickupType.MISSILES;
                    break;
                }
        }

        if (Enemies != null)
        {
            foreach (GameObject g in Enemies)
            {
                Destroy(g);
            }


            Enemies.Clear();
        }
        else
        {
            Enemies = new List<GameObject>();
        }

        enemyCount = level * enemiesPerLevel;

        for(int i = 0; i < enemyCount; i++)
        {
            GameObject n = Instantiate(enemyBase, transform);

            n.transform.position = new Vector3(Random.Range(-xBound, xBound), Random.Range(-yBound, yBound), -1);
            Debug.Log(n.transform.position);

            Enemies.Add(n);
        }
    }

    public void ScratchOne()
    {
        enemyCount--;
        score++;

        UpdateText();

        if(enemyCount <= 0)
        {
            StartLevel();
        }
    }

    public void EndGame()
    {
        StartCoroutine(GameOver());
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameOver");
    }
}



    

