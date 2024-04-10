using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] GameObject hud;

    private void Update()
    {
        coinsText.text = CoinManager.Instance.GetCoins().ToString();
    }

    public void Show()
    {
       hud.SetActive(true);

        Invoke("Hide", 3);
    }

    public void Hide()
    {
        hud.SetActive(false);
    }

}
