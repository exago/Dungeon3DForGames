using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomColliderBehaviour : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            RoomManager.CurrentRoom = GetComponent<Collider>();
            RoomManager.SetEnemyCounterAsync();
        }
    }
}
