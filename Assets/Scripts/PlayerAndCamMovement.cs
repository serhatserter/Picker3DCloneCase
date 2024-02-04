using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndCamMovement : MonoBehaviour
{
    [SerializeField] float zSpeed;

    private bool isPause;

    void Update()
    {
        SetZPos();
    }

    void SetZPos()
    {
        if (!isPause)
        {
            var nextPos = transform.localPosition + Vector3.forward;
            var currentSpeed = zSpeed + (GameManager.Instance.CurrentLevel * 0.1f);
            transform.position = Vector3.MoveTowards(transform.localPosition, nextPos, Time.deltaTime * currentSpeed);

        }

    }

    public void PauseMovement()
    {
        isPause = true;
    }

    public void ResumeMovement()
    {
        isPause = false;
    }
}
