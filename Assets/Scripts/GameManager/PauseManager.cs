using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseManager : MonoBehaviour
{
    public UIDocument UIDoc;

    public UIDocument GameUIDoc;

    private VisualElement pauseMenu;
    private VisualElement GameUI;
    private Button resumeButton;

    private Button mainMenuButton;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;


    private bool isPaused = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        UIDoc = GetComponent<UIDocument>();

        resumeButton = UIDoc.rootVisualElement.Q("ResumeButton") as Button;
        resumeButton.clicked += OnResumeClick;
        resumeButton.RegisterCallback<PointerEnterEvent>(evt => audioSource.PlayOneShot(hoverSound));
        resumeButton.RegisterCallback<FocusInEvent>(evt => audioSource.PlayOneShot(hoverSound));

        mainMenuButton = UIDoc.rootVisualElement.Q("MainMenuButton") as Button;
        mainMenuButton.clicked += OnMainMenuClick;
        mainMenuButton.RegisterCallback<PointerEnterEvent>(evt => audioSource.PlayOneShot(hoverSound));
        mainMenuButton.RegisterCallback<FocusInEvent>(evt => audioSource.PlayOneShot(hoverSound));


    }

    void Start()
    {
        pauseMenu = UIDoc.rootVisualElement.Q<VisualElement>("Container");
        GameUI = GameUIDoc.rootVisualElement.Q<VisualElement>("Container");

        pauseMenu.style.display = DisplayStyle.None;
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (upgradeWindow.instance.isOpen) return;
        
        if (context.performed) 
        {
            
            if (!isPaused)
            {
                SwitchPauseMenuState();
                SwitchGameUIState();
            }
            else
            {
                SwitchPauseMenuState();
                SwitchGameUIState();
            }
        }
    }

    private void SwitchPauseMenuState()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseMenu.style.display = DisplayStyle.None;
        }

        else
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseMenu.style.display = DisplayStyle.Flex;
            resumeButton.Focus();
        }
    }

    private void SwitchGameUIState()
    {
        if (isPaused)
        {
            GameUI.style.display = DisplayStyle.None;
        }

        else
        {
            GameUI.style.display = DisplayStyle.Flex;
        }
    }


    private void OnResumeClick()
    {
        SwitchPauseMenuState();
        SwitchGameUIState();
    }

    private void OnMainMenuClick()
    {
        StartCoroutine(PressMainMenu());
    }

    private IEnumerator PressMainMenu()
    {
        yield return new WaitForSecondsRealtime(clickSound.length);
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
