using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCheckForPool : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (transform.position.z + 10f < GameManager.Instance.PlayerParent.position.z)
        {
            gameObject.SetActive(false);
        }
    }
}
