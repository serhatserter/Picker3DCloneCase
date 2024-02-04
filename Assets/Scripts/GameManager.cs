using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    public static GameManager Instance { get; private set; }

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;


        GetLevel();

    }
    #endregion SINGLETON

    public int LevelIndex;
    public int CurrentLevel;
    public Transform PlayerParent;

    [Space(5)]

    public PlayerAndCamMovement PlayerAndCamMovement;
    public LevelCreator LevelCreator;

    public Action CollectAll;

    [HideInInspector] public FinalPlatformMovement CurrentFinalPlatformMovement;

    public static void SaveLevelsJSON(string fileName, object recordObj)
    {
        string strOutput = JsonUtility.ToJson(recordObj);
        File.WriteAllText("Assets/Resources/Levels/" + fileName + ".json", strOutput);

        Debug.Log("SAVING JSON in resources folder... ");
    }

    public static bool LoadLevelsJSON<T>(string fileName, ref T recordObj)
    {
        string path = "Assets/Resources/Levels/" + fileName + ".json";

        Debug.Log("LOADING JSON...");


        if (File.Exists(path))
        {
            string contents = File.ReadAllText(path);

            Type recordType = recordObj.GetType();

            recordObj = (T)JsonUtility.FromJson(contents, recordType);

            Debug.Log("LOADING JSON COMPLETED");

            return true;
        }
        else
        {
            Debug.LogError("LOADING JSON NOT COMPLETED!");

            return false;
        }
    }


    [Header("CANVAS")]

    public GameObject WinPanel;
    public GameObject FailPanel;
    public GameObject TapToStartPanel;
    public GameObject InGamePanel;

    public Transform TapToStartText;
    public TMP_Text LevelText;

    public void TapToStart()
    {
        TapToStartPanel.SetActive(false);
        InGamePanel.SetActive(true);

        PlayerAndCamMovement.enabled = true;
    }

    public void NextLevel()
    {
        WinPanel.SetActive(false);
        InGamePanel.SetActive(false);

        CurrentFinalPlatformMovement.PassLevel();
    }

    public void RetryLevel()
    {
        FailPanel.SetActive(false);
        InGamePanel.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void GetLevel()
    {
        LevelIndex = PlayerPrefs.GetInt("LevelIndex");


        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
        else
        {
            CurrentLevel = 1;
            PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);

        }
    }

    public void SetNextLevel()
    {
        if (LevelIndex < LevelCreator.AllLevelFeatures.Levels.Count - 2)
        {
            LevelIndex++;
        }
        else
        {
            int randomIndex = UnityEngine.Random.Range(0, LevelCreator.AllLevelFeatures.Levels.Count - 1);
            LevelIndex = randomIndex;
        }

        PlayerPrefs.SetInt("LevelIndex", LevelIndex);

        CurrentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);

    }

    private void Start()
    {
        TapToStartText.DOScale(0.7f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        LevelText.SetText("Level " + CurrentLevel);
    }
}
