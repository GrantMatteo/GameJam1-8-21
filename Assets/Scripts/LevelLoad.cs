using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    void Start()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene("Untitled");
    }
}