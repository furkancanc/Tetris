using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    int lines;
    int level = 1;

    public int linesPerLevel = 5;

    const int minLines = 1;
    const int maxLines = 4;

    public void ScoreLines(int n)
    {
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
}
