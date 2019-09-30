using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private bool m_startOnEnable;
    [SerializeField] private float m_duration = 45.0f;
    [SerializeField] private Image m_timerImage;

    private bool m_timerRunning;
    private float m_timeStarted;
    private float m_finishTime;
    private float m_progress;

    public Action timerStarted { get; set; }
    public Action timerCompleted { get; set; }
    public Action timerReset { get; set; }
    #endregion

    #region UNITY_MONOBEHAVIOUR_METHODS
    private void OnEnable()
    {
        if (m_startOnEnable)
        {
            StartTimer();
        }
    }

    private void Update()
    {
        if (m_timerRunning)
        {
            if (m_finishTime <= Time.timeSinceLevelLoad)
            {
                CompleteTimer();
            }
            else
            {
                m_progress = (Time.timeSinceLevelLoad - m_timeStarted) / m_duration;
                m_timerImage.fillAmount = 1.0f - m_progress;
            }
        }
    }
    #endregion

    #region PUBLIC_METHODS
    public void StartTimer()
    {
        m_timerRunning = true;
        m_progress = 0.0f;
        m_timeStarted = Time.timeSinceLevelLoad;
        m_finishTime = m_timeStarted + m_duration;
        timerStarted?.Invoke();
    }

    public void CompleteTimer()
    {
        m_timerRunning = false;
        m_progress = 1.0f;
        m_timerImage.fillAmount = 0.0f;
        timerCompleted?.Invoke();
    }

    public void ResetTimer()
    {
        m_timerRunning = false;
        m_progress = 0.0f;
        m_timerImage.fillAmount = 1.0f;
        timerReset?.Invoke();
    }
    #endregion
}
