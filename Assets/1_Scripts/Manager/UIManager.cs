using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 모든 씬에 공통적으로 필요한 UI 시스템들
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    GameObject quitPanel;

    void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        //백버튼
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitPanel.SetActive(true);
            TimeStop();
        }
    }

    //오브젝트 껐다켰다 + 시간 멈추기
    public void OpenObj(GameObject ui)
    {
        ui.SetActive(true);
    }
    public void CloseObj(GameObject ui)
    {
        ui.SetActive(false);
    }

    //종료
    public void OnQuit()
    {
        Application.Quit();
    }

    //시간 멈추기
    public void TimeStop()
    {
        Time.timeScale = 0;
    }
    public void TimeStart()
    {
        Time.timeScale = 1;
    }
}
