using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument doc;
    private Button startButton;
    private Button quitButton;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private string startButtonName = "StartGameButton";
    [SerializeField] private string quitButtonName = "Quit";

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        doc = GetComponent<UIDocument>();

        startButton = doc.rootVisualElement.Q<Button>(startButtonName);
        quitButton = doc.rootVisualElement.Q<Button>(quitButtonName);

        if (startButton == null)
            Debug.LogError($"No Button named '{startButtonName}' found in the UI Document.");
        else
            SetupButton(startButton, OnPlayGameClick);

        if (quitButton == null)
            Debug.LogError($"No Button named '{quitButtonName}' found in the UI Document.");
        else
            SetupButton(quitButton, OnQuitClick);
    }

    private void SetupButton(Button button, System.Action onClick)
    {
        button.clicked += onClick;
        button.clicked += () => PlaySound(clickSound);

        button.RegisterCallback<PointerEnterEvent>(evt => PlaySound(hoverSound));
        button.RegisterCallback<FocusInEvent>(evt => PlaySound(hoverSound));
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }

    private IEnumerator Start()
    {
        yield return null;
        startButton?.Focus();
    }

    private void OnPlayGameClick()
    {
        Debug.Log("Play clicked");
        StartCoroutine(PressPlay());
    }

    private void OnQuitClick()
    {
        Debug.Log("Quit clicked");
        StartCoroutine(PressQuit());
    }

    private float ClickDelay => clickSound != null ? clickSound.length : 0f;

    private IEnumerator PressPlay()
    {
        yield return new WaitForSecondsRealtime(ClickDelay);

        SceneManager.LoadScene("Gameplay");
    }

    private IEnumerator PressQuit()
    {
        yield return new WaitForSecondsRealtime(ClickDelay);

        Debug.Log("Quitting");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}