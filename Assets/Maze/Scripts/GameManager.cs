using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public uint Score { get; set; }

    public bool WinState { get; private set; }

    public delegate void StateAction();
    public event StateAction StateChange;

    void Awake ()
    {
		if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
	}

    public void IncreaseScore ()
    {
        Score++;

        UpdateSubscribers();
    }

    public void Win ()
    {
        WinState = true;

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
