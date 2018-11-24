using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    public uint Score { get { return score; } set { score = value; } }
    private uint score;

    public bool WinState { get { return winState; } }
    private bool winState;

    public delegate void StateAction();
    public event StateAction StateChange;

    void Awake ()
    {
		if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
	}

    public void IncreaseScore ()
    {
        score++;

        UpdateSubscribers();
    }

    public void Win ()
    {
        winState = true;

        UpdateSubscribers();
    }

    private void UpdateSubscribers ()
    {
        if (StateChange != null)
        {
            StateChange();
        }
    }
}
