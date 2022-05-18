using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean teleportAction;
    public SteamVR_Action_Boolean gripAction;
    public SteamVR_Action_Boolean sideAction;
    public HitBox hitBox;

    public string sceneName;

    private void Update()
    {
        // Debug.Log(posValue.
        if (teleportAction.GetStateDown(handType))
        {
            if (hitBox.isStart)
                SceneManager.LoadScene(sceneName);
        }

        if (teleportAction.GetStateUp(handType))
            Debug.Log("텔레포트땜");

        if (gripAction.GetStateDown(handType))
        {
            if (hitBox.isStart)
                SceneManager.LoadScene(sceneName);
        }

        if (gripAction.GetStateUp(handType))
            Debug.Log("그립버튼땜");

        if (sideAction.GetStateDown(handType))
        {
            if (hitBox.isStart)
                SceneManager.LoadScene(sceneName);
        }

        if (sideAction.GetStateUp(handType))
            Debug.Log("사이드버튼땜");


    }
}
