using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject[] _possibleDrops = null;

    public void Drop()
    {
        Instantiate(CalculateDrop(), this.transform.position, this.transform.rotation);
    }

    private GameObject CalculateDrop()
    {
        int randomNumber = UnityEngine.Random.Range(0, _possibleDrops.Length - 1);
        
        return _possibleDrops[randomNumber];        
    }
}
