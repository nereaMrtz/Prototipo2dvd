using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsManager : MonoBehaviour
{
    [SerializeField] Gem[] cursedGems;
    [SerializeField] Gem[] notCursedGems;
    int totalGems = 0;
    bool gemsCollected = false;

    private void Start()
    {
        totalGems += cursedGems.Length;
        totalGems += notCursedGems.Length;
    }

    void Update()
    {
        foreach (var gem in cursedGems)
        {
            if (gem.collected)
            {
                Destroy(gem);
                totalGems--;
            }
        }
        foreach (var gem in notCursedGems)
        {
            if (gem.collected)
            {
                Destroy(gem);
                totalGems--;
            }
        }

        if (totalGems == 0)
            gemsCollected = true;
    }
}
