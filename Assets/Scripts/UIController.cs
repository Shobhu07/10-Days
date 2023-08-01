using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void Scene1()
    {
        SceneManager.LoadScene("Cutscene1");

    }

    public void Scene2()
    {
        SceneManager.LoadScene("Cutscene 2");
    }

    public void Scene3()
    {
        SceneManager.LoadScene("level 1");
    }
}
