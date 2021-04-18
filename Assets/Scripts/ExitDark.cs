using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDark : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Player.maxChkpPriority = 0;
            Destroy(GameObject.Find("BGM1"));
            SceneManager.LoadScene("Title");
        }
    }
}
