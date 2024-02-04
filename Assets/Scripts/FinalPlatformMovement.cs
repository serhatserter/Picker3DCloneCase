using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FinalPlatformMovement : MonoBehaviour
{
    public int CollectCount;
    [SerializeField] Transform greenPlatform;
    [SerializeField] TMP_Text collectText;

    [SerializeField] Transform leftGate, rightGate;

    private int passCount;

    private void Start()
    {
        GetComponent<Collider>().enabled = true;
        passCount = GameManager.Instance.LevelCreator.AllLevelFeatures.Levels[GameManager.Instance.LevelIndex].LevelPassCount;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CollectAll();
        }
    }

    void CollectAll()
    {
        GameManager.Instance.PlayerAndCamMovement.PauseMovement();
        GameManager.Instance.CollectAll();

        CheckLevel();
    }

    async void CheckLevel()
    {
        GameManager.Instance.CurrentFinalPlatformMovement = this;

        await Task.Delay(2500);

        if (CollectCount >= passCount)
        {
            GameManager.Instance.InGamePanel.SetActive(false);

            GameManager.Instance.WinPanel.SetActive(true);

        }
        else
        {
            GameManager.Instance.InGamePanel.SetActive(true);

            GameManager.Instance.FailPanel.SetActive(true);

        }
    }

    public void PassLevel()
    {
        leftGate.DORotate(Vector3.up * -90, 1f).SetDelay(0.2f).SetEase(Ease.InOutBack);
        rightGate.DORotate(Vector3.up * 90, 1f).SetDelay(0.2f).SetEase(Ease.InOutBack);

        greenPlatform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            GameManager.Instance.PlayerAndCamMovement.ResumeMovement();

            GameManager.Instance.LevelCreator.CreateLevel(GameManager.Instance.LevelIndex + 1);

            GameManager.Instance.InGamePanel.SetActive(true);

            CollectCount = 0;

            GameManager.Instance.SetNextLevel();


        });
    }

    private void OnDisable()
    {
        leftGate.transform.eulerAngles = Vector3.zero;
        rightGate.transform.eulerAngles= Vector3.zero;

        greenPlatform.transform.localPosition = Vector3.up * -10;

    }

    private void Update()
    {
        passCount = GameManager.Instance.LevelCreator.AllLevelFeatures.Levels[GameManager.Instance.LevelIndex].LevelPassCount;
        collectText.SetText(CollectCount.ToString() + "/" + passCount);
    }
}
