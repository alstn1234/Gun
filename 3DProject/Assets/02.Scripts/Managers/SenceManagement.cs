using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenceManagement : MonoBehaviour
{
    

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    
    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
