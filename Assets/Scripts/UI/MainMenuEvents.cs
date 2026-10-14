using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument doc;

    private Button button;

    private List<Button> menuButtons = new List<Button>();

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        doc = GetComponent<UIDocument>();

        button = doc.rootVisualElement.Q("StartGameButton") as Button;
        button.RegisterCallback<ClickEvent>(OnPlayGameClick);

        menuButtons = doc.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        SceneManager.LoadScene("Gameplay");
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        audioSource.volume = .5f;
        audioSource.Play();
    }

}
