using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
/// <summary>
/// 컷씬에 필요한 모든 것
/// </summary>
public class Tutorial : MonoBehaviour
{
    int time = 0;
    private void Awake()
    {
        //해상도 고정
        Screen.SetResolution(2960, 1440, true);

    }
    private void Start()
    {
        InvokeRepeating("TimeCheck", 1f, 1f);
    }

    //시간 체크 후 효과 출력
    void TimeCheck()
    {
        time++;
        if (time == 1)
        {
            SoundM.Instance.SoundOn("SFX", 0);
        }
        if (time == 5)
        {
            Camera.main.DOShakePosition(0.3f);
        }
        if (time == 7)
        {
            SoundM.Instance.SoundOn("SFX", 1);
        }
        if (time == 9)
        {
            SoundM.Instance.SoundOn("SFX", 2);
        }
    }

    //SKIP 버튼
    public void OnClickSkipButton()
    {
        transform.DOKill();
        SceneM.instance.SceneChange("Start");
    }

    //오브젝트 껐다켰다 + 시간 멈추기
    public void OpenObj(GameObject obj)
    {
        obj.SetActive(true);
        Time.timeScale = 0;
    }
    public void CloseObj(GameObject obj)
    {
        Time.timeScale = 1;
        obj.SetActive(false);
    }

    //종료
    public void OnQuit()
    {
        Application.Quit();
    }
}
