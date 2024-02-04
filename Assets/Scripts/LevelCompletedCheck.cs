using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCompletedCheck : MonoBehaviour
{
    [SerializeField] FinalPlatformMovement FinalPlatformMovement;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Collectable")
        {
            FinalPlatformMovement.CollectCount++;
        }
    }
}
