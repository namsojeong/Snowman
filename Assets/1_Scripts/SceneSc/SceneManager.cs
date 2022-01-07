using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    [SerializeField]
    GameObject[] scenePanel;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenScene(int num)
    {
        for(int i=0;i<scenePanel.Length;i++)
        {
            scenePanel[i].SetActive(false);
        }
        scenePanel[num].SetActive(true);
    }

}
