using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IndexManager : MonoBehaviour
{

    //设置难度
    public void EasyButton()
    {
        GhostMove.speed = 0.05f;
        LoadMain();
    }

    public void MidButton()
    {
        GhostMove.speed = 0.15f;
        LoadMain();
    }

    public void HardButton()
    {
        GhostMove.speed = 0.3f;
        LoadMain();
    }


    public void ScoreButton()
    {
        SceneManager.LoadScene("Score");
    }
    //加载主场景
    private void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }
}
