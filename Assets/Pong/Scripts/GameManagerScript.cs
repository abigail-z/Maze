using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public BallScript ball;
    public Text leftScoreText;
    public Text rightScoreText;
    public Text winText;
    public uint winningScore;
    public float winWaitTime;
    public string otherScene;
    private uint leftScore = 0;
    private uint rightScore = 0;

    // Use this for initialization
    void Start ()
    {
        winText.enabled = false;

        leftScoreText.text = leftScore.ToString();
        rightScoreText.text = rightScore.ToString();
    }

    public void LeftScores ()
    {
        leftScoreText.text = (++leftScore).ToString();
        if (leftScore >= winningScore)
        {
            winText.text = "Blue Wins!";
            StartCoroutine(Victory());
        }
    }

    public void RightScores ()
    {
        rightScoreText.text = (++rightScore).ToString();
        if (rightScore >= winningScore)
        {
            winText.text = "Red Wins!";
            StartCoroutine(Victory());
        }
    }

    IEnumerator Victory ()
    {
        winText.enabled = true;
        ball.gameObject.SetActive(false);
        yield return new WaitForSeconds(winWaitTime);

        PongSwitchManager.Instance.DisablePong();
    }
}
