using UnityEngine;
using UnityEngine.UI;

public class MayanNumberGenerator : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private Sprite[] m_glyphTextures;
    [SerializeField] private Image m_glyph400;
    [SerializeField] private Image m_glyph020;
    [SerializeField] private Image m_glyph001;

    private int m_digit400;
    private int m_digit020;
    private int m_digit001;
    private int m_numberValue = -1;
    private int m_previousNumberValue = -1;
    #endregion

    #region PUBLIC_METHODS
    public void GenerateNumber(int digitCount)
    {
        if (digitCount == 1)
        {
            m_previousNumberValue = m_numberValue;

            while (m_previousNumberValue == m_numberValue)
            {
                m_digit001 = Random.Range(0, 20);

                m_glyph400.sprite = null;
                m_glyph020.sprite = null;
                m_glyph001.sprite = m_glyphTextures[m_digit001];

                m_numberValue = m_digit001;
            }
        } 
        else if (digitCount == 2)
        {
            m_previousNumberValue = m_numberValue;

            while (m_previousNumberValue == m_numberValue)
            {
                m_digit020 = Random.Range(1, 20);
                m_digit001 = Random.Range(0, 20);

                m_glyph400.sprite = null;
                m_glyph020.sprite = m_glyphTextures[m_digit020];
                m_glyph001.sprite = m_glyphTextures[m_digit001];

                m_numberValue = (20 * m_digit020) + m_digit001;
            }
        } 
        else if (digitCount == 3)
        {
            m_previousNumberValue = m_numberValue;

            while (m_previousNumberValue == m_numberValue)
            {
                m_digit400 = Random.Range(1, 20);
                m_digit020 = Random.Range(1, 20);
                m_digit001 = Random.Range(0, 20);

                m_glyph400.sprite = m_glyphTextures[m_digit400];
                m_glyph020.sprite = m_glyphTextures[m_digit020];
                m_glyph001.sprite = m_glyphTextures[m_digit001];

                m_numberValue = (400 * m_digit400) + (20 * m_digit020) + m_digit001;
            }
        } 
        else
        {
            m_digit400 = 0;
            m_digit020 = 0;
            m_digit001 = 0;

            m_glyph400.sprite = m_glyphTextures[m_digit400];
            m_glyph020.sprite = m_glyphTextures[m_digit020];
            m_glyph001.sprite = m_glyphTextures[m_digit001];

            m_numberValue = (400 * m_digit400) + (20 * m_digit020) + m_digit001;
        }
    }

    public bool IsDecimalNumberEqual(int decimalNumber)
    {
        return decimalNumber == m_numberValue;
    }
    #endregion
}
