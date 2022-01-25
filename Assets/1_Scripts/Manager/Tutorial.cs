using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    bool isSetting = false;
    int time = 0;
    public void SettingOn(GameObject panel)
    {
        isSetting = isSetting ? false : true;

        if (isSetting)
        {
            panel.transform.DOMove(new Vector3(0, 0, 0), 1f);
        }
        else
        {
            panel.transform.DOMove(new Vector3(0, 9.3f, 0), 1f);
        }
    }
    private void Start()
    {
        isSetting = false;
        InvokeRepeating("TimeCheck", 1f, 1f);
    }
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

    private void Awake()
    {
        //�ػ� ����
        Screen.SetResolution(2960, 1440, true);

    }
    public void OnClickSkipButton()
    {
        SceneManager.LoadScene("Main");
    }

    //������Ʈ �����״� + �ð� ���߱�
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

    //����
    public void OnQuit()
    {
        Application.Quit();
    }
}
