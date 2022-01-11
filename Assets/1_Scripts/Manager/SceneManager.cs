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

    public bool isRunning=false;

    private void Awake()
    {
        Instance = this;
        OpenScene(0);
    }

    //씬 바꾸기 함수
    public void OpenScene(int num)
    {
        isRunning = num == 1 ? true : false;
        
        
        for (int i = 0; i < 3; i++)
        {
            scenePanel[i].SetActive(false);
            sceneObj[i].SetActive(false);
        }
        scenePanel[num].SetActive(true);
        sceneObj[num].SetActive(true);
        if(num==1)
        {
            InGame.Instance.SpawnFoot();
        }
        else if (num == 2)
        {
            InGame.Instance.Reset();
        }
    }

}
