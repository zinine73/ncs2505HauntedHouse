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
    public AudioSource exitAudio;
    public AudioSource caughtAudio;
    bool hasAudioPlayed;
    bool isPlayerAtExit;
    bool isPlayerCaught;
    float timer;
    VisualElement endScreen;
    VisualElement caughtScreen;
    float gameTimer = 0f;
    bool gameTimerIsTicking = false;
    Label gameTimerLabel;

    void Start()
    {
        endScreen = uiDoc.rootVisualElement.Q<VisualElement>("EndScreen");
        caughtScreen = uiDoc.rootVisualElement.Q<VisualElement>("CaughtScreen");
        gameTimerLabel = uiDoc.rootVisualElement.Q<Label>("TimerLabel");
        gameTimer = 0f;
        gameTimerIsTicking = true;
        UpdateTimerLabel();
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

    void UpdateTimerLabel()
    {
        gameTimerLabel.text = gameTimer.ToString("0.00");
    }

    void Update()
    {
        if (gameTimerIsTicking)
        {
            gameTimer += Time.deltaTime;
            UpdateTimerLabel();
        }

        if (isPlayerAtExit)
        {
            EndLevel(endScreen, false, exitAudio);
        }
        else if (isPlayerCaught)
        {
            EndLevel(caughtScreen, true, caughtAudio);
        }
    }

    void EndLevel(VisualElement element, bool doRestart, AudioSource audioSource)
    {
        if (!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;    
        }
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
