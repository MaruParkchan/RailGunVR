using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoChange : MonoBehaviour
{
    public string sceneName;
    private void Awake()
    {
        StartCoroutine(AutoSceneChange());
    }

    IEnumerator AutoSceneChange()
    {
        yield return new WaitForSeconds(40.0f);
        SceneManager.LoadScene(sceneName);
    }
}
