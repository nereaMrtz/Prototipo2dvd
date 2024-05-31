using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] GameObject hud;
    [HideInInspector]public Animator anim;

    private void Update()
    {
        coinsText.text = CoinManager.Instance.GetCoins().ToString();
        anim = hud.GetComponent<Animator>();
    }

    public void Show()
    {
        anim.Play("Show");
        Invoke("Hide", 3);
    }

    public void Hide()
    {
        anim.Play("hide");
    }

    public void ShowPauseMenuHud()
    {
        anim.Play("coinsUIKeep");
    }

}
