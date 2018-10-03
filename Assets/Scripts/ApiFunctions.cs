using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[Serializable]
public class Score
{
    public string id;
    public string playerId;
    public int value;
    public DateTime date;
}

[Serializable]
public class Leaderboard
{
    public List<Score> scores;
}

[Serializable]
public class Player
{
    public string id;
    public string name;
}

[Serializable]
public class PlayerList
{
    public List<Player> players;
}

public class ApiFunctions
{
    public static string playerId = "879b08d8-e8e4-4839-be8e-4959785441af";
    private static List<Player> players = new List<Player>();

    public static void SetCurrentPlayer(int playerIndex)
    {
        Debug.Log("Name: " + players[playerIndex].name);
        Debug.Log("Id: " + players[playerIndex].id);
        playerId = players[playerIndex].id;
    }

    public static void PostScore(int score)
    {
        UnityWebRequest request = UnityWebRequest.Post("localhost:5000/api/scores"
            + "?playerId=" + playerId
            + "&value=" + score, "");

        request.SendWebRequest();
    }

    public static IEnumerator GetLeaderboard(GameObject leaderboardText)
    {
        UnityWebRequest request = UnityWebRequest.Get("localhost:5000/api/scores");
        request.chunkedTransfer = false;
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string json = "{\"scores\":" + request.downloadHandler.text + "}";
            Leaderboard board = JsonUtility.FromJson<Leaderboard>(json);
            string result = "";

            foreach (Score score in board.scores)
            {
                UnityWebRequest nameRequest = UnityWebRequest.Get("localhost:5000/api/players/" + score.playerId);
                nameRequest.chunkedTransfer = false;
                yield return nameRequest.SendWebRequest();

                if (!nameRequest.isNetworkError && !nameRequest.isHttpError)
                {
                    Debug.Log("Data: " + nameRequest.downloadHandler.text);
                    string playerJson = nameRequest.downloadHandler.text;
                    Player player = JsonUtility.FromJson<Player>(playerJson);

                    result += player.name + ": " + score.value + "\n";
                }
            }

            Debug.Log("Text:\n" + result);
            leaderboardText.GetComponent<Text>().text = result;
        }
    }

    public static IEnumerator GetPlayerNames(Dropdown playerSelect)
    {
        players.Clear();
        List<string> playerNames = new List<string>();

        UnityWebRequest request = UnityWebRequest.Get("localhost:5000/api/players");
        request.chunkedTransfer = false;
        yield return request.SendWebRequest();

        if (!request.isNetworkError && !request.isHttpError)
        {
            string json = "{\"players\":" + request.downloadHandler.text + "}";
            PlayerList playerList = JsonUtility.FromJson<PlayerList>(json);

            foreach (Player player in playerList.players)
            {
                players.Add(player);
                playerNames.Add(player.name);
            }
        }

        playerSelect.ClearOptions();
        playerSelect.AddOptions(playerNames);
    }

    public static void AddPlayer(string playerName)
    {
        UnityWebRequest request = UnityWebRequest.Post("localhost:5000/api/players/" + playerName, "");
        request.SendWebRequest();
    }

    public static void RenamePlayer(string playerName)
    {
        UnityWebRequest request = UnityWebRequest.Put("localhost:5000/api/players/" + playerId + "?name=" + playerName, "name=" + playerName);
        request.SendWebRequest();
    }
}
