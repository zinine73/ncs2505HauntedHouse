using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public UIDocument uiDoc;
    bool isPlayerAtExit;
    bool isPlayerCaught;
    float timer;
    VisualElement endScreen;
    VisualElement caughtScreen;

    void Start()
    {
        endScreen = uiDoc.rootVisualElement.Q<VisualElement>("EndScreen");
        caughtScreen = uiDoc.rootVisualElement.Q<VisualElement>("CaughtScreen");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }

    public void CaughtPlayer()
    {
        isPlayerCaught = true;    
    }

    void Update()
    {
        if (isPlayerAtExit)
        {
            EndLevel(endScreen, false);
        }
        else if (isPlayerCaught)
        {
            EndLevel(caughtScreen, true);
        }
    }

    void EndLevel(VisualElement element, bool doRestart)
    {
        timer += Time.deltaTime;
        element.style.opacity = timer / fadeDuration;
        if (timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene("Main");
            }
            else
            {
                Application.Quit();
                Time.timeScale = 0;
            }
        }
    }
}
