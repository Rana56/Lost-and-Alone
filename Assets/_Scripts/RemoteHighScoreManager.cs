using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HighScoreResult
{
    public float Score;
    public string code; // error code
    public string message; // error message
}

public class RemoteHighScoreManager : MonoBehaviour {
    public static RemoteHighScoreManager Instance { get; private set; }
    private IEnumerator coroutineSend;
    private IEnumerator coroutineReceive;
    void Awake ()
    {
        // force singleton instance
        if (Instance == null) { Instance = this; }
        else { Destroy (gameObject); }
        // do not destroy this object when we load the scene
        DontDestroyOnLoad(gameObject);
    }

    public void GetHighScore(Action<float> callback) {
        coroutineReceive = GetHighScoreCR(callback);
        StartCoroutine(coroutineReceive);

    }

    public void SetHighScore(float score) {
        coroutineSend = SetHighScoreCR(score);
        StartCoroutine(coroutineSend);

    }

    // TODO #1 - change the signature to be a Coroutine, add callback
    // parameter with int parameter
    public IEnumerator GetHighScoreCR(Action<float> OnCompleteCallback)
    {
        string strTableName = "HighScore";

        // TODO #2 - construct the url for our request, including objectid!
        const string objectID = "4C57FFC2-220E-4D0C-82D1-4FE30401D082";
        string url = "https://eu-api.backendless.com/" +
                    Globals.APPLICATION_ID + "/" +
                    Globals.REST_SECRET_KEY +
                    "/data/" +
                    strTableName +
                    "/" +
                    objectID;

        // TODO #3 - create a GET UnityWebRequest, passing in our URL
        UnityWebRequest webreq = UnityWebRequest.Get(url);

        // TODO #4 - set the request headers as dictated by the backendless documentation
        // (3 headers)
        webreq.SetRequestHeader("application-id", Globals.APPLICATION_ID);
        webreq.SetRequestHeader("secret-key", Globals.REST_SECRET_KEY);
        webreq.SetRequestHeader("application-type", "REST");

        // TODO #5 - Send the webrequest and yield (so the script waits until it returns
        // with a result)
        yield return webreq.SendWebRequest();

        // TODO #6 - check for webrequest errors
        if (webreq.result == UnityWebRequest.Result.ConnectionError) {
            Debug.Log("ConnectionError");
        } else if (webreq.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ProtocolError");
        } else if (webreq.result == UnityWebRequest.Result.DataProcessingError) {
            Debug.Log("DataProcessingError");
        /*} else if (webreq.result == UnityWebRequest.Result.Success) {
            Debug.Log("Success");*/
        } else {
            // TODO #7 - Convert the downloadHandler.text property to HighScoreResult (currently JSON)
            HighScoreResult highScoreData = JsonUtility.FromJson<HighScoreResult>(webreq.downloadHandler.text);

            // TODO #8 - check that there are no backendless errors
            if (!string.IsNullOrEmpty(highScoreData.code)) {
                Debug.Log("Error:" + highScoreData.code + " " + highScoreData.message);
            } else {
                OnCompleteCallback(highScoreData.Score);
            }
        }
    }

    // TODO #1 - change the signature to be a Coroutine, add score parameter,
    // add callback parameter
    public IEnumerator SetHighScoreCR(float score)
    {
        string strTableName = "HighScore";
        const string objectID = "4C57FFC2-220E-4D0C-82D1-4FE30401D082";
        // TODO #2 - construct the url for our request, including objectid!
        string url = "https://eu-api.backendless.com/" +
                    Globals.APPLICATION_ID + "/" +
                    Globals.REST_SECRET_KEY +
                    "/data/" +
                    strTableName +
                    "/" +
                    objectID;

        // TODO #3 - construct JSON string of data we want to send
        string data = JsonUtility.ToJson(new HighScoreResult { Score = score });

        // TODO #4 - create PUT UnityWebRequest passing our url and JSON data
        UnityWebRequest webreq = UnityWebRequest.Put(url, data);

        // TODO #5 - set the request headers as dictated by the backendless documentation
        // (4 headers)
        webreq.SetRequestHeader("Content-Type", "application/json");
        webreq.SetRequestHeader("application-id", Globals.APPLICATION_ID);
        webreq.SetRequestHeader("secret-key", Globals.REST_SECRET_KEY);
        webreq.SetRequestHeader("application-type", "REST");

        // TODO #6 - Send the webrequest and yield (so the script waits until it returns
        // with a result)
        yield return webreq.SendWebRequest();

        // TODO #7 - check for webrequest errors
        if (webreq.result == UnityWebRequest.Result.ConnectionError) {
            Debug.Log("ConnectionError");
        } else if (webreq.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ProtocolError");
        } else if (webreq.result == UnityWebRequest.Result.DataProcessingError) {
            Debug.Log("DataProcessingError");
        /*} else if (webreq.result == UnityWebRequest.Result.Success) {
            Debug.Log("Success");*/
        } else {
            Debug.Log("Success");
            // TODO #7 - Convert the downloadHandler.text property to HighScoreResult (currently JSON)
            HighScoreResult highScoreData = JsonUtility.FromJson<HighScoreResult>(webreq.downloadHandler.text);

            // TODO #8 - check that there are no backendless errors
            if (!string.IsNullOrEmpty(highScoreData.code)) {
                Debug.Log("Error:" + highScoreData.code + " " + highScoreData.message);
            }
        }
    }
}
