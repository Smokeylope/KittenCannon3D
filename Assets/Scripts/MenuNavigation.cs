using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour {

    public GameObject MenuPanel;
    public GameObject Play;
    public GameObject Leaderboard;
    public GameObject LeaderboardNames;
    public GameObject LeaderboardScores;
    public GameObject LeaderboardDates;
    public Dropdown PlayerSelect;
    public InputField RenameField;
    public InputField AddField;

    // Use this for initialization
    void Start()
    {
        MenuPanel.SetActive(true);
        Play.SetActive(false);
        Leaderboard.SetActive(false);

        StartCoroutine(UpdatePlayerSelection());
    }

    public void ShowLeaderboard()
    {
        MenuPanel.SetActive(false);
        Play.SetActive(false);
        Leaderboard.SetActive(true);

        StartCoroutine(ApiFunctions.GetLeaderboard(LeaderboardNames, LeaderboardScores, LeaderboardDates));
    }

    public void ShowPlay()
    {
        MenuPanel.SetActive(false);
        Play.SetActive(true);
        Leaderboard.SetActive(false);

        StartCoroutine(ApiFunctions.GetPlayerNames(PlayerSelect));
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

    public void StartGame()
    {
        ApiFunctions.SetCurrentPlayer(PlayerSelect.value);
        SceneManager.LoadScene("default");
    }

    public void SelectPlayer()
    {
        Debug.Log("Selected player " + PlayerSelect.value);
        ApiFunctions.SetCurrentPlayer(PlayerSelect.value);
    }

    public void RenamePlayer()
    {
        SelectPlayer();
        ApiFunctions.RenamePlayer(RenameField.text);
        RenameField.text = "";
        StartCoroutine(ApiFunctions.GetPlayerNames(PlayerSelect));
    }

    public void AddPlayer()
    {
        ApiFunctions.AddPlayer(AddField.text);
        AddField.text = "";
        StartCoroutine(ApiFunctions.GetPlayerNames(PlayerSelect));
    }

    private IEnumerator UpdatePlayerSelection()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            yield return StartCoroutine(ApiFunctions.GetPlayerNames(PlayerSelect));
        }
    }
}
