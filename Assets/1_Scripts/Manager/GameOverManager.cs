using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    Text overText;
    [SerializeField]
    private Text overScoreText;
    [SerializeField]
    private Text overHighScoreText;


    //게임오버 UI
    public void UpdateOverUI()
    {
        overText.text = string.Format("");
        overScoreText.text = string.Format($"SCORE\n{PlayerPrefs.GetInt("SCORE", 0)}");
        overHighScoreText.text = string.Format($"HIGHSCORE {PlayerPrefs.GetInt("HIGHSCORE", 0)}");
    }
    public void OverText()
    {
        overText.DOText("GAME OVER", 2f);
    }
}
