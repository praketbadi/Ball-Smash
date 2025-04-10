using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startmenu : MonoBehaviour
{
    public void onStart(){
        SceneManager.LoadScene("SampleScene");
    }
    public void onExit(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        #endif
        Application.Quit();
    }
}
