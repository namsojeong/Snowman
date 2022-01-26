using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    Text text;

    Coroutine FadeC;
    private void Awake()
    {
        text = GetComponent<Text>();
    }
    private void OnEnable()
    {
        FadeC = StartCoroutine(FadeTextToZero());
    }
    public IEnumerator FadeTextToZero()  // ���İ� 1���� 0���� ��ȯ
    {
        yield return new WaitForSeconds(2f);
        //anim.Play("TextFade");
        gameObject.SetActive(false);
    }
}