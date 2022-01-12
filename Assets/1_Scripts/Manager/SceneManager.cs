using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    //num = 0 -> Standby
    //num = 1 -> Running
    //num = 2 -> GameOver

    [Header("씬 UI")]
    [SerializeField]
    GameObject[] scenePanel;
    [SerializeField]
    GameObject[] sceneObj;

    public bool isRunning = false;

    private void Awake()
    {
        Instance = this;
        OpenScene(0);
    }

    //씬 바꾸기 함수
    public void OpenScene(int num)
    {
        //Running 상태로 변환
        isRunning = num == 1 ? true : false;

        //모두 끄고 원하는 씬만 키기
        for (int i = 0; i < 3; i++)
        {
            scenePanel[i].SetActive(false);
            sceneObj[i].SetActive(false);
        }
        scenePanel[num].SetActive(true);
        sceneObj[num].SetActive(true);

        //게임오버씬
        if (num == 2)
        {
            InGame.Instance.Reset();
            UI.Instance.OverText();
        }
    }

}
