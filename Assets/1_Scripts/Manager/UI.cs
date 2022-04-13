using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [Header("RUNNING")]
    [SerializeField]
    Text scoreText = null;
    [SerializeField]
    Text highScoreText = null;
    [SerializeField]
    Slider snowbar;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InvokeRepeating("ScoreUp", 1f, 1f);
    }

    

    //������ �� UI + ����캼 ����
    public void UpdateSlider(float value)
    {
        snowbar.value = value;
        
        if (snowbar.value <= 0)
        {
            snowbar.value = 0;
            InGame.Instance.snowball = 0;
        }
        else
        {
            int count = 0;
            if(snowbar.value<0.8f)
            {
                InGame.Instance.snowball = 0;
                return;
            }
            for (float i = value; i > 0.6f; i -= 0.2f)
            {
                count++;
            }
            InGame.Instance.snowball = count;
            count = 0;
        }
    }
    
    

    //�ΰ��� ���ھ� UI
    public void UpdateUI()
    {
        scoreText.text = string.Format($"{GameManager.Instance.score}");
        highScoreText.text = string.Format($"BEST {PlayerPrefs.GetInt("HIGHSCORE",0)}");
    }

}
