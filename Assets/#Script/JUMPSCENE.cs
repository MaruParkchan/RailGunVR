using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class JUMPSCENE : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SceneManager.LoadScene("Intro");

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SceneManager.LoadScene("Stage1");

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SceneManager.LoadScene("Stage2");
    }
}
