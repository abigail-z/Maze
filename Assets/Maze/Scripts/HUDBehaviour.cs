using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDBehaviour : MonoBehaviour
{
    private Text win;
    private Text score;

	// Use this for initialization
	void Awake ()
    {
        win = transform.Find("Win Text").GetComponent<Text>();
        score = transform.Find("Score Text").GetComponent<Text>();
    }

    void Start ()
    {
        GameManager.Instance.StateChange += UpdateHUD;
        UpdateHUD();
    }

    public void UpdateHUD ()
    {
        win.enabled = GameManager.Instance.WinState;
        score.text = GameManager.Instance.Score.ToString();
    }
}
