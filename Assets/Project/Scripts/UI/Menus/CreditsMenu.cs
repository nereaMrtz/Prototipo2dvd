using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreditsMenu : MonoBehaviour
{
    [SerializeField]float buttonDelay = 3f;
    [SerializeField]GameObject BackButton;
    public SceneChange sceneChange;
    void Start()
    {
        BackButton.SetActive(false);
        StartCoroutine(ShowBackButton());
    }

    IEnumerator ShowBackButton()
    {
        yield return new WaitForSeconds(buttonDelay);
        BackButton.SetActive(true);
        EventSystem.current.SetSelectedGameObject(BackButton);
    }

    // Update is called once per frame
    public void BackToMenu()
    {
        sceneChange.ChangeScene();
    }
}
