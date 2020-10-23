﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToMainMenu ()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
