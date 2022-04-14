using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 씬에 관련된 스크립트
/// </summary>
public class SceneM : MonoBehaviour
{
    public static SceneM instance;
    public static SceneM Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneM>();
                if (instance != null)
                {
                    GameObject container = new GameObject("SceneM");
                    instance = container.AddComponent<SceneM>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }

    //씬 전환
    public void SceneChange(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    //옛날 씬 하나로 최적화 코드

    //public bool isRunning = false;
    //num = 0 -> Standby
    //num = 1 -> Running
    //num = 2 -> GameOver

    //[Header("씬 UI")]
    //[SerializeField]
    //GameObject[] scenePanel;
    //[SerializeField]
    //public GameObject[] sceneObj;

    //씬 바꾸기 함수
    //public void OpenScene(int num)
    //{
    //    //Running 상태로 변환
    //    isRunning = num == 1 ? true : false;

    //    //모두 끄고 원하는 씬만 키기
    //    for (int i = 0; i < 3; i++)
    //    {
    //        scenePanel[i].SetActive(false);
    //        sceneObj[i].SetActive(false);
    //    }
    //    scenePanel[num].SetActive(true);
    //    sceneObj[num].SetActive(true);

    //    SoundM.Instance.SoundOn("BGM", num);
    //    //게임오버씬
    //    if (num == 2)
    //    {
    //        InGame.Instance.Reset();
    //        UI.Instance.UpdateOverUI();
    //        UI.Instance.OverText();
    //    }
    //}
}
