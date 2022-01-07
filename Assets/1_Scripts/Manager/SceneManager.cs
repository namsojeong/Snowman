using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    [SerializeField]
    GameObject[] scenePanel;
    
    [SerializeField]
    GameObject[] sceneObj;

    private void Awake()
    {
        Instance = this;
        OpenScene(1);
    }

    public void OpenScene(int num)
    {
        for (int i = 0; i < 3; i++)
        {
            scenePanel[i].SetActive(false);
            sceneObj[i].SetActive(false);
        }
        scenePanel[num].SetActive(true);
        sceneObj[num].SetActive(true);
    }

}
