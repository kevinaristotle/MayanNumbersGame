using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    #region ENUM_DEFINITIONS
    public enum MenuType
    {
        Start, Tutorial, Replay, None
    }

    public enum FadeType
    {
        FadeIn, FadeOut, None
    }
    #endregion

    #region VARIABLES
    [Header("Start Menu Items")]
    [SerializeField] private GameObject m_startMenuGameObject;
    [SerializeField] private Image[] m_startMenuImages;
    [SerializeField] private TextMeshProUGUI[] m_startMenuTexts;
    [SerializeField] private Button[] m_startMenuButtons;

    [Header("Tutorial Menu Items")]
    [SerializeField] private GameObject m_tutorialMenuGameObject;
    [SerializeField] private Image[] m_tutorialMenuImages;
    [SerializeField] private TextMeshProUGUI[] m_tutorialMenuTexts;
    [SerializeField] private Button[] m_tutorialMenuButtons;

    [Header("Replay Menu Items")]
    [SerializeField] private GameObject m_replayMenuGameObject;
    [SerializeField] private Image[] m_replayMenuImages;
    [SerializeField] private TextMeshProUGUI[] m_replayMenuTexts;
    [SerializeField] private Button[] m_replayMenuButtons;

    private MenuType m_currentMenu = MenuType.None;

    private FadeType m_currentFadeState = FadeType.None;
    private float m_fadeDuration = 0.75f;
    private float m_progress;
    private float m_elapsedTime;

    public Action<MenuType, FadeType> fadeCompleted { get; set; }
    #endregion

    #region PROPERTIES
    public MenuType currentMenu
    {
        get { return m_currentMenu; }
    }

    public bool isFading
    {
        get { return m_currentFadeState == FadeType.FadeIn || m_currentFadeState == FadeType.FadeOut; }
    }
    #endregion

    #region UNITY_MONOBEHAVIOUR_METHODS
    public void Update()
    {
        if (isFading)
        {
            if (m_progress < 1.0f)
            {
                ProgressFadeMneu();
            }
            else
            {
                CompleteFadeMenu();
            }
        }
    }
    #endregion

    #region PUBLIC_METHODS
    public void ShowMenu(MenuType menu)
    {
        m_currentMenu = menu;

        SetMenuGameObjectActiveState(m_currentMenu, true);
        SetMenuButtonInteractableState(m_currentMenu, true);
    }

    public void FadeInMenu(MenuType menu)
    {
        m_currentMenu = menu;

        m_currentFadeState = FadeType.FadeIn;
        m_elapsedTime = 0.0f;
        m_progress = 0.0f;
        ApplyFadeValueToMenu();

        SetMenuGameObjectActiveState(m_currentMenu, true);
    }

    public void FadeOutMenu(MenuType menu)
    {
        m_currentMenu = menu;

        m_currentFadeState = FadeType.FadeOut;
        m_elapsedTime = 0.0f;
        m_progress = 0.0f;
        ApplyFadeValueToMenu();

        SetMenuGameObjectActiveState(m_currentMenu, true);
        SetMenuButtonInteractableState(m_currentMenu, false);
    }
    #endregion

    #region PRIVATE_METHODS
    private void ProgressFadeMneu()
    {
        m_elapsedTime += Time.deltaTime;
        m_progress = m_elapsedTime / m_fadeDuration;
        ApplyFadeValueToMenu();
    }

    private void CompleteFadeMenu()
    {
        MenuType previousMenu = m_currentMenu;
        FadeType previousFadeType = m_currentFadeState;

        m_progress = 1.0f;
        ApplyFadeValueToMenu();

        if (m_currentFadeState == FadeType.FadeOut)
        {
            SetMenuGameObjectActiveState(m_currentMenu, false);
        }
        else if (m_currentFadeState == FadeType.FadeIn)
        {
            SetMenuButtonInteractableState(m_currentMenu, true);
        }

        m_currentFadeState = FadeType.None;

        if (m_currentFadeState == FadeType.FadeOut)
        {
            m_currentMenu = MenuType.None;
        }

        fadeCompleted?.Invoke(previousMenu, previousFadeType);
    }

    private void SetMenuButtonInteractableState(MenuType menu, bool interactable)
    {
        switch (menu)
        {
            case MenuType.Start:
                for (int i = 0; i < m_startMenuButtons.Length; i++)
                {
                    m_startMenuButtons[i].interactable = interactable;
                }
                break;
            case MenuType.Tutorial:
                for (int i = 0; i < m_tutorialMenuButtons.Length; i++)
                {
                    m_tutorialMenuButtons[i].interactable = interactable;
                }
                break;
            case MenuType.Replay:
                for (int i = 0; i < m_replayMenuButtons.Length; i++)
                {
                    m_replayMenuButtons[i].interactable = interactable;
                }
                break;
        }
    }

    private void SetMenuGameObjectActiveState(MenuType menu, bool activeState)
    {
        switch (menu)
        {
            case MenuType.Start:
                m_startMenuGameObject.SetActive(activeState);
                break;
            case MenuType.Tutorial:
                m_tutorialMenuGameObject.SetActive(activeState);
                break;
            case MenuType.Replay:
                m_replayMenuGameObject.SetActive(activeState);
                break;
        }
    }

    private void ApplyFadeValueToMenu()
    {
        switch (m_currentFadeState)
        {
            case FadeType.FadeIn:
                SetMenuOpacity(m_currentMenu, m_progress);
                break;
            case FadeType.FadeOut:
                SetMenuOpacity(m_currentMenu, 1.0f - m_progress);
                break;
        }
    }

    private void SetMenuOpacity(MenuType menu, float opacity)
    {
        switch(menu)
        {
            case MenuType.Start:
                for (int i = 0; i < m_startMenuImages.Length; i++)
                {
                    Image image = m_startMenuImages[i];
                    Color color = image.color;
                    color.a = opacity;
                    image.color = color;
                }

                for (int i = 0; i < m_startMenuTexts.Length; i++)
                {
                    TextMeshProUGUI text = m_startMenuTexts[i];
                    Color color = text.color;
                    color.a = opacity;
                    text.color = color;
                }
                break;
            case MenuType.Tutorial:
                for (int i = 0; i < m_tutorialMenuImages.Length; i++)
                {
                    Image image = m_tutorialMenuImages[i];
                    Color color = image.color;
                    color.a = opacity;
                    image.color = color;
                }

                for (int i = 0; i < m_tutorialMenuTexts.Length; i++)
                {
                    TextMeshProUGUI text = m_tutorialMenuTexts[i];
                    Color color = text.color;
                    color.a = opacity;
                    text.color = color;
                }
                break;
            case MenuType.Replay:
                for (int i = 0; i < m_replayMenuImages.Length; i++)
                {
                    Image image = m_replayMenuImages[i];
                    Color color = image.color;
                    color.a = opacity;
                    image.color = color;
                }

                for (int i = 0; i < m_replayMenuTexts.Length; i++)
                {
                    TextMeshProUGUI text = m_replayMenuTexts[i];
                    Color color = text.color;
                    color.a = opacity;
                    text.color = color;
                }
                break;
        } 
    }
    #endregion
}
