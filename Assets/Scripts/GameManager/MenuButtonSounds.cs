using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuButtonSounds : MonoBehaviour
{
    [SerializeField] private float volume = 0.5f;

    private UIDocument doc;
    private AudioSource audioSource;
    private List<Button> menuButtons = new List<Button>();

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        doc = GetComponent<UIDocument>();

        menuButtons = doc.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        audioSource.volume = volume;
        audioSource.Play();
    }
}