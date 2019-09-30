using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private MenuManager m_menuManager;
    [SerializeField] private TutorialManager m_tutorialManager;
    [SerializeField] private ScoreManager m_scoreManager;
    [SerializeField] private MayanNumberGenerator m_mayanNumberGenerator;
    [SerializeField] private Timer m_timer;
    [SerializeField] private TMP_InputField m_userInputField;
    [SerializeField] private GameObject backgroundImage;

    private bool m_isStartingGame;
    private bool m_isStartingTutorial;
    private bool m_isExitingToStartMenu;
    private bool m_isShakingInputField;
    #endregion

    #region UNITY_MONOBEHAVIOUR_METHODS
    private void Start()
    {
        m_menuManager.fadeCompleted += OnFadeCompleted;
        m_timer.timerCompleted += OnTimerCompleted;
        m_menuManager.ShowMenu(MenuManager.MenuType.Start);
    }
    #endregion

    #region PUBLIC_METHODS
    public void StartGame()
    {
        m_isStartingGame = true;
        m_scoreManager.ResetScore();
        m_mayanNumberGenerator.GenerateNumber(1);
        backgroundImage.SetActive(false);
        m_menuManager.FadeOutMenu(m_menuManager.currentMenu);
    }

    public void StartTutorial()
    {
        m_isStartingTutorial = true;
        m_tutorialManager.ResetTutorialSlides();
        m_menuManager.FadeOutMenu(m_menuManager.currentMenu);
    }

    public void ExitToStartMenu()
    {
        m_isExitingToStartMenu = true;
        backgroundImage.SetActive(true);
        m_menuManager.FadeOutMenu(m_menuManager.currentMenu);
    }

    public void EvaluateInputField() 
    {
        int userInputTextAsInt = -1;
        Int32.TryParse(m_userInputField.text, out userInputTextAsInt);

        if (m_mayanNumberGenerator.IsDecimalNumberEqual(userInputTextAsInt)) 
        {
            IncreaseScoreAndGenerateNumber();
            FocusAndSelectAllInInputField();
        }
        else {
            if (!m_isShakingInputField)
            {
                ShakeInputField(0.3f, 124.0f, 12.0f);
            }
        }
    }
    #endregion

    #region PRIVATE_METHODS
    private void ShakeInputField(float duration, float frequency, float shakeAmount) {
        StartCoroutine(ShakeInputFieldCoroutine(duration, frequency, shakeAmount));
    }

    private IEnumerator ShakeInputFieldCoroutine(float duration, float frequency, float shakeAmount) {
        m_isShakingInputField = true;
        float elapsedTime = 0.0f;
        Vector3 inputFieldStartPosition = m_userInputField.transform.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float positionOffset = Mathf.PingPong(Time.time * frequency, shakeAmount) - shakeAmount * 0.5f;

            m_userInputField.transform.position = new Vector3(
                inputFieldStartPosition.x + positionOffset, 
                inputFieldStartPosition.y,
                inputFieldStartPosition.z
            );

            yield return null;
        }

        m_isShakingInputField = false;
        m_userInputField.transform.position = inputFieldStartPosition;
    }


    private void FocusAndSelectAllInInputField()
    {
        EventSystem.current.SetSelectedGameObject(m_userInputField.gameObject, null);
        m_userInputField.OnPointerClick(new PointerEventData(EventSystem.current));
    }

    private void IncreaseScoreAndGenerateNumber() 
    {
        if (m_scoreManager.scoreIncreaseCount < 5)
        {
            m_scoreManager.IncreaseScore(100);
            m_mayanNumberGenerator.GenerateNumber(1);
        }
        else if (m_scoreManager.scoreIncreaseCount < 10)
        {
            m_scoreManager.IncreaseScore(250);
            m_mayanNumberGenerator.GenerateNumber(2);
        } 
        else {
            m_scoreManager.IncreaseScore(500);
            m_mayanNumberGenerator.GenerateNumber(3);
        }
    }

    private void OnFadeCompleted(MenuManager.MenuType menu, MenuManager.FadeType fade)
    {
        if (fade == MenuManager.FadeType.FadeOut)
        {
            if (m_isStartingGame)
            {
                m_isStartingGame = false;
                FocusAndSelectAllInInputField();
                m_timer.StartTimer();
            }
            else if (m_isStartingTutorial)
            {
                m_isStartingTutorial = false;
                m_menuManager.FadeInMenu(MenuManager.MenuType.Tutorial);
            } 
            else if(m_isExitingToStartMenu)
            {
                m_isExitingToStartMenu = false;
                m_menuManager.FadeInMenu(MenuManager.MenuType.Start);
            }
        }
    }

    private void OnTimerCompleted()
    {
        m_scoreManager.ApplyFinalScoreText();
        m_menuManager.FadeInMenu(MenuManager.MenuType.Replay);
    }
    #endregion
}
