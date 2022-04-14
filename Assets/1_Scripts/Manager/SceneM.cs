using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// ���� ���õ� ��ũ��Ʈ
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

    //�� ��ȯ
    public void SceneChange(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    //���� �� �ϳ��� ����ȭ �ڵ�

    //public bool isRunning = false;
    //num = 0 -> Standby
    //num = 1 -> Running
    //num = 2 -> GameOver

    //[Header("�� UI")]
    //[SerializeField]
    //GameObject[] scenePanel;
    //[SerializeField]
    //public GameObject[] sceneObj;

    //�� �ٲٱ� �Լ�
    //public void OpenScene(int num)
    //{
    //    //Running ���·� ��ȯ
    //    isRunning = num == 1 ? true : false;

    //    //��� ���� ���ϴ� ���� Ű��
    //    for (int i = 0; i < 3; i++)
    //    {
    //        scenePanel[i].SetActive(false);
    //        sceneObj[i].SetActive(false);
    //    }
    //    scenePanel[num].SetActive(true);
    //    sceneObj[num].SetActive(true);

    //    SoundM.Instance.SoundOn("BGM", num);
    //    //���ӿ�����
    //    if (num == 2)
    //    {
    //        InGame.Instance.Reset();
    //        UI.Instance.UpdateOverUI();
    //        UI.Instance.OverText();
    //    }
    //}
}
