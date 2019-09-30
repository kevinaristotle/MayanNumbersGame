using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private GameObject[] m_slides;
    [SerializeField] private Button m_previousButton;
    [SerializeField] private Button m_nextButton;

    private int m_currentSlideIndex;
    #endregion

    #region PUBLIC_METHODS
    public void ResetTutorialSlides() 
    {
        for (int i = 0; i < m_slides.Length; i++) 
        {
            m_slides[i].SetActive(false);
        }

        m_currentSlideIndex = 0;
        m_slides[m_currentSlideIndex].SetActive(true);
        UpdateButtoninteractableState();
    }

    public void PreviousSlide()
    {
        m_slides[m_currentSlideIndex].SetActive(false);
        m_currentSlideIndex--;
        m_slides[m_currentSlideIndex].SetActive(true);
        UpdateButtoninteractableState();
    }

    public void NextSlide()
    {
        m_slides[m_currentSlideIndex].SetActive(false);
        m_currentSlideIndex++;
        m_slides[m_currentSlideIndex].SetActive(true);
        UpdateButtoninteractableState();
    }
    #endregion

    #region PRIVATE_METHODS
    private void UpdateButtoninteractableState()
    {
        if (m_currentSlideIndex > 0 && m_currentSlideIndex < m_slides.Length)
        {
            m_previousButton.transform.gameObject.SetActive(true);
        }
        else
        {
            m_previousButton.transform.gameObject.SetActive(false);
        }

        if (m_currentSlideIndex >= 0 && m_currentSlideIndex < m_slides.Length - 1)
        {
            m_nextButton.transform.gameObject.SetActive(true);
        }
        else
        {
            m_nextButton.transform.gameObject.SetActive(false);
        }
    }
    #endregion
}
