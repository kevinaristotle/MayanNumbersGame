using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private TextMeshProUGUI m_scoreText;
    [SerializeField] private TextMeshProUGUI m_finalScoreText;

    private readonly string k_scorePrefix = "Score: ";
    private readonly string k_finalScorePrefix = "You completed the game with a final score of: ";
    #endregion

    #region PROPERTIES
    public int score { get; private set; } = 0;
    public int scoreIncreaseCount { get; private set; } = 0;
    #endregion

    #region PUBLIC_METHODS
    public void ResetScore()
    {
        score = 0;
        scoreIncreaseCount = 0;
        m_scoreText.text = k_scorePrefix + score.ToString();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreIncreaseCount++;
        m_scoreText.text = k_scorePrefix + score.ToString();
    }

    public void ApplyFinalScoreText()
    {
        m_finalScoreText.text = k_finalScorePrefix + score.ToString();
    }
    #endregion
}
