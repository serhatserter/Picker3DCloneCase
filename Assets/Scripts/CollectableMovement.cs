using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMovement : MonoBehaviour
{
    [SerializeField] Rigidbody thisRigidbody;
    [SerializeField] Collider thisCollider;

    void Start()
    {
        GameManager.Instance.CollectAll += OnCollect;

    }

    private void OnDestroy()
    {
        GameManager.Instance.CollectAll -= OnCollect;

    }

    private void OnCollect()
    {
        
        //thisRigidbody.constraints = RigidbodyConstraints.None;
        //thisRigidbody.mass = 1;

        var forceVec = (Vector3.forward * 150) + (Vector3.up * 200);

        thisRigidbody.AddForce(forceVec);
    }

    private bool isCollect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinalPlatformCollectArea" && !isCollect)
        {
            
            isCollect = true;
            CreateCollectPiece();
        }
    }

    void CreateCollectPiece()
    {

        GameObject newPiece = GameManager.Instance.LevelCreator.CollectParticlePooler.GetPooledObject(0);
        newPiece.transform.position = transform.position;
        newPiece.transform.eulerAngles = Vector3.zero;

        newPiece.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
