using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    public List<UnityEngine.UI.Text> topScoresTexts;
    static public List<float> topScores = new List<float>() { 60 * 59 + 59, 60 * 59 + 59, 60 * 59 + 59, 60 * 59 + 59 };

    void Start()
    {
        Lala();
    }

    private void Lala()
    {
        for (int i = 0; i < topScoresTexts.Count; ++i)
        {
            UnityEngine.UI.Text topScoresText = topScoresTexts[i];
            if (topScoresText != null)
            {
                topScoresText.text = String.Format("\"{0:D2}:{1:D2}\"", Mathf.FloorToInt(topScores[i / 2] / 60), Mathf.FloorToInt(topScores[i / 2] % 60));
            }
        }
    }

    public static void AddScore(float newScore)
    {
        topScores.Add(newScore);
        topScores.Sort();
    }
}
