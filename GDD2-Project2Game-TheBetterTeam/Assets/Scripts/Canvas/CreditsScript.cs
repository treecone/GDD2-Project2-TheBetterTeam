using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{

    public int seconds;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitToReturn());
    }

    IEnumerator waitToReturn ()
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(0);
    }

}
