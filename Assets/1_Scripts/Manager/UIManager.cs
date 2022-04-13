using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Time.timeScale = 0;
        }
    }

    //오브젝트 껐다켰다 + 시간 멈추기
    public void OpenObj(GameObject ui)
    {
        ui.SetActive(true);
        Time.timeScale = 0;
    }
    public void CloseObj(GameObject ui)
    {
        Time.timeScale = 1;
        ui.SetActive(false);
    }

    //종료
    public void OnQuit()
    {
        Application.Quit();
    }
}
