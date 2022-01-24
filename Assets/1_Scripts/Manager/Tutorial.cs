using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
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
