using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextBehaviour : MonoBehaviour
{
    [SerializeField] private ChestBehaviour _startChest = null;
    [SerializeField] private TakeOverBehaviour _takeOverBehaviour = null;

    private bool _hasTakenOver = false;

    [SerializeField] private Collider _firstRoom = null;
    [SerializeField] private Collider _secondRoom = null;

    private Text _text = null;
    [SerializeField] private WeaponTutorialBehaviour _tutorialText = null;
    [SerializeField] private Sprite _revolverSprite = null;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        string outputString = string.Empty;

        if (!_startChest.IsChestOpen && _startChest.Highlight)
            outputString = "Use E to interact with highlighted objects.";

        if (!_startChest.IsChestOpen && !_startChest.Highlight)
            outputString = "WASD To move.\nMouse to aim";

        if (RoomManager.CurrentRoom == _firstRoom)
            outputString = "Stab Enemies in the back in order to posses them.";

        if (RoomManager.CurrentRoom == _secondRoom)
           outputString = "Use shift to jump out of your transformation.\nThrow your knife at a new enemy to take him over.";
        
        if(_takeOverBehaviour.TakenOver && !_hasTakenOver)
        {
            _hasTakenOver = true;
            _tutorialText.EnableImage(_revolverSprite, 7);
        }

        _text.text = outputString;

    }
}
