using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument doc;
    private Button button;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        doc = GetComponent<UIDocument>();

        button = doc.rootVisualElement.Q("StartGameButton") as Button;
        button.RegisterCallback<ClickEvent>(OnPlayGameClick);
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        StartCoroutine(PressPlay());
    }

    private IEnumerator PressPlay()
    {
        yield return new WaitForSeconds(audioSource.clip.length + 0.5f);

        SceneManager.LoadScene("Gameplay");
    }
}