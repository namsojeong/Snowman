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
        //���ư
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    //������Ʈ �����״� + �ð� ���߱�
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

    //����
    public void OnQuit()
    {
        Application.Quit();
    }
}
