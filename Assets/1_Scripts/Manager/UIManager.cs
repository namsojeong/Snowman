using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��� ���� ���������� �ʿ��� UI �ý��۵�
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
        //���ư
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitPanel.SetActive(true);
            TimeStop();
        }
    }

    //������Ʈ �����״� + �ð� ���߱�
    public void OpenObj(GameObject ui)
    {
        ui.SetActive(true);
    }
    public void CloseObj(GameObject ui)
    {
        ui.SetActive(false);
    }

    //����
    public void OnQuit()
    {
        Application.Quit();
    }

    //�ð� ���߱�
    public void TimeStop()
    {
        Time.timeScale = 0;
    }
    public void TimeStart()
    {
        Time.timeScale = 1;
    }
}
