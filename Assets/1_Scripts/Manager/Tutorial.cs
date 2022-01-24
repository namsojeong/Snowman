using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{

    private void Awake()
    {
        //해상도 고정
        Screen.SetResolution(2960, 1440, true);

    }
    public void OnClickSkipButton()
    {
        SceneManager.LoadScene("Main");
    }

    //오브젝트 껐다켰다 + 시간 멈추기
    public void OpenObj(GameObject obj)
    {
        obj.SetActive(true);
        Time.timeScale = 0;
    }
    public void CloseObj(GameObject obj)
    {
        obj.SetActive(false);
        Time.timeScale = 1;
    }

    //종료
    public void OnQuit()
    {
        Application.Quit();
    }
}
