using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    //num = 0 -> Standby
    //num = 1 -> Running
    //num = 2 -> GameOver

    [Header("�� UI")]
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

    //�� �ٲٱ� �Լ�
    public void OpenScene(int num)
    {
        //Running ���·� ��ȯ
        isRunning = num == 1 ? true : false;

        //��� ���� ���ϴ� ���� Ű��
        for (int i = 0; i < 3; i++)
        {
            scenePanel[i].SetActive(false);
            sceneObj[i].SetActive(false);
        }
        scenePanel[num].SetActive(true);
        sceneObj[num].SetActive(true);

        //���ӿ�����
        if (num == 2)
        {
            InGame.Instance.Reset();
            UI.Instance.OverText();
        }
    }

}
