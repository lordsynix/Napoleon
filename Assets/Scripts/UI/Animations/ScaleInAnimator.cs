using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleInAnimator : MonoBehaviour
{
    public void Open()
    {
        transform.localScale = Vector2.zero;
        transform.LeanScale(Vector2.one, 0.4f).setEaseOutSine();
        gameObject.SetActive(true);
    }
    public void Close()
    {
        transform.localScale = Vector2.one;
        transform.LeanScale(Vector2.zero, 0.4f).setEaseInBack();
        gameObject.SetActive(false);
    }
}
