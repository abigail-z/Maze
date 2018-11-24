using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDBehaviour : MonoBehaviour
{
    public float saveStatusScreentime;

    private Text win;
    private Text score;
    private Text saveStatus;

	// Use this for initialization
	void Awake ()
    {
        win = transform.Find("Win Text").GetComponent<Text>();
        score = transform.Find("Score Text").GetComponent<Text>();
        saveStatus = transform.Find("Save Text").GetComponent<Text>();
        saveStatus.enabled = false;
    }

    void Start ()
    {
        GameManager.Instance.StateChange += UpdateHUD;
        UpdateHUD();
    }

    void UpdateHUD ()
    {
        win.enabled = GameManager.Instance.WinState;
        score.text = GameManager.Instance.Score.ToString();
    }

    public void UpdateSaveStatus (string message)
    {
        StartCoroutine(SaveDisplay(message));
    }

    IEnumerator SaveDisplay (string message)
    {
        saveStatus.enabled = true;
        saveStatus.text = message;
        yield return new WaitForSeconds(saveStatusScreentime);
        saveStatus.enabled = false;
    }
}
