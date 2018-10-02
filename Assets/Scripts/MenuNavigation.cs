using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigation : MonoBehaviour {

    public GameObject MenuPanel;
    public GameObject Play;
    public GameObject Leaderboard;
    public GameObject LeaderboardText;

    // Use this for initialization
    void Start()
    {
        MenuPanel.SetActive(true);
        Play.SetActive(false);
        Leaderboard.SetActive(false);
    }

    public void ShowLeaderboard()
    {
        MenuPanel.SetActive(false);
        Play.SetActive(false);
        Leaderboard.SetActive(true);

        StartCoroutine(ApiFunctions.GetLeaderboard(LeaderboardText));
    }

    public void ShowPlay()
    {
        MenuPanel.SetActive(false);
        Play.SetActive(true);
        Leaderboard.SetActive(false);
    }

    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
        Play.SetActive(false);
        Leaderboard.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
