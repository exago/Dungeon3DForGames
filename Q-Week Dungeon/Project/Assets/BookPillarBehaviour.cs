using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookPillarBehaviour : InterActableBehaviour
{
    private int _nextScene = 0;
    protected override void Awake()
    {
        _nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        base.Awake();
    }
    public override void Interaction()
    {
        if(_nextScene <= SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(_nextScene);
    }
}
