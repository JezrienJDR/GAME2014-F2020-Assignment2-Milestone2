using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStart()
    {
        Debug.Log("StartGame");
        SceneManager.LoadScene("Level0");
    }

    public void OnClickInstructions()
    {
        SceneManager.LoadScene("InstructionsWL");
        Debug.Log("Instructions");
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("StartWL");
    }
}
