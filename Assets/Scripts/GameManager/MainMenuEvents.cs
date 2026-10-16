using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument doc;
    private Button button;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        doc = GetComponent<UIDocument>();

        button = doc.rootVisualElement.Q("StartGameButton") as Button;

        button.clicked += OnPlayGameClick;
        button.clicked += () => audioSource.PlayOneShot(clickSound);

        button.RegisterCallback<PointerEnterEvent>(evt => audioSource.PlayOneShot(hoverSound));
        button.RegisterCallback<FocusInEvent>(evt => audioSource.PlayOneShot(hoverSound));

    }

    private IEnumerator Start()
    {
        yield return null;
        button.Focus();
    }

    private void OnPlayGameClick()
    {
        StartCoroutine(PressPlay());
    }

    private IEnumerator PressPlay()
    {
        yield return new WaitForSeconds(clickSound.length);

        SceneManager.LoadScene("Gameplay");
    }
}