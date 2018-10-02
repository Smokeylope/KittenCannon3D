using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[Serializable]
public class Score
{
    public Guid id;
    public Guid playerId;
    public int value;
    public DateTime date;
}

[Serializable]
public class Leaderboard
{
    public List<Score> scores;
}

public class Player
{
    public Guid id;
    public string name;
}

public class ApiFunctions
{
    public static Guid playerId = new Guid("879b08d8-e8e4-4839-be8e-4959785441af");

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
                /*UnityWebRequest nameRequest = UnityWebRequest.Get("localhost:5000/api/players/" + score.playerId);
                nameRequest.chunkedTransfer = false;
                yield return nameRequest.SendWebRequest();

                Debug.Log("Data: " + nameRequest.downloadHandler.text);
                string playerJson = nameRequest.downloadHandler.text;
                Player player = JsonUtility.FromJson<Player>(playerJson);*/

                result += score.value + "\n";
                Debug.Log("Add: " + score.value);
            }

            Debug.Log("Text:\n" + result);
            leaderboardText.GetComponent<Text>().text = result;
        }
    }
}
