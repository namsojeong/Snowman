using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffect : MonoBehaviour
{

    Coroutine FadeC;
    private void OnEnable()
    {
        FadeC = StartCoroutine(FadeTextToZero());
    }
    public IEnumerator FadeTextToZero()  // 알파값 1에서 0으로 전환
    {
        yield return new WaitForSeconds(2f);
        //anim.Play("TextFade");
        ObjectPool.Instance.ReturnObject(PoolObjectType.PlusText, gameObject);
    }
}