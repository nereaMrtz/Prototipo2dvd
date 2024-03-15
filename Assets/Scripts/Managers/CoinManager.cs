using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField]int coins = 0;

    static public CoinManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one AudioManager!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddCoin()
    {
        coins++;
    }
}
