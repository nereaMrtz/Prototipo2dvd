using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private string sceneName;    

    protected virtual void ChangeScene()
    {
        LevelLoader.Instance.SetLoad();
        SceneManager.LoadScene(sceneName);
    }
}
