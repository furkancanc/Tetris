using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    int lines;
    public int level = 1;

    public int linesPerLevel = 5;

    public TextMeshProUGUI linesText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreText;

    public bool didLevelUp = false;

    const int minLines = 1;
    const int maxLines = 4;

    public ParticlePlayer levelUpFx;

    public void ScoreLines(int n)
    {
        didLevelUp = false;

        n = Mathf.Clamp(n, minLines, maxLines);
        switch(n)
        {
            case 1:
                score += 40 * level;
                break;
            case 2:
                score += 100 * level;
                break;
            case 3:
                score += 300 * level;
                break;
            case 4:
                score += 1200 * level;
                break;
        }
        lines -= n;

        if (lines <= 0)
        {
            LevelUp();
        }

        UpdateUIText();
    }

    public void Reset()
    {
        level = 1;
        lines = linesPerLevel * level;
    }

    private void Start()
    {
        Reset();
    }

    void UpdateUIText()
    {
        if (linesText)
        {
            linesText.text = lines.ToString();
        }

        if (levelText)
        {
            levelText.text = level.ToString();
        }

        if (scoreText)
        {
            scoreText.text = PadZero(score, 5);
        }
    }

    string PadZero(int n, int padDigits)
    {
        string nStr = n.ToString();

        while (nStr.Length < padDigits)
        {
            nStr = "0" + nStr;
        }

        return nStr;
    }

    public void LevelUp()
    {
        ++level;
        lines = linesPerLevel * level;
        didLevelUp = true;

        if (levelUpFx)
        {
            levelUpFx.Play();
        }
    }
}
