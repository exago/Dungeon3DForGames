using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InterActionBehavior : MonoBehaviour
{
    private GameObject _interactable = null;
    private GameObject _previousInteractable = null;

    [SerializeField] private float _interactionRadius = 2.0f;
    [SerializeField] private LayerMask _selectableLayerMask = 12;
 

    public void Interact()
    {
        if (_interactable)
            _interactable.GetComponent<InterActableBehaviour>().Interaction();
    }

    private void FixedUpdate()
    {
        CheckForInteraction();
    }

    private void CheckForInteraction()
    {
        Collider[] currentInteractbles = Physics.OverlapSphere(this.transform.position, _interactionRadius, _selectableLayerMask);
        
        if(_previousInteractable != null)
        {
            if (!currentInteractbles.Contains(_previousInteractable.GetComponent<Collider>()))                     //Reset highlight
            {
                _previousInteractable.GetComponent<HighlightObjectBehaviour>().Highlight = false;
                _previousInteractable = null;
            }

        }

        if (currentInteractbles.Length > 0)                                                                                                     //assign Interactable and highlight
        {
            _interactable = currentInteractbles[0].gameObject;
            _interactable.GetComponent<HighlightObjectBehaviour>().Highlight = true;
            _previousInteractable = _interactable;
        }
        else
        {
            _interactable = null;                                                                                                               //cleanup Interactable
        }

    }
}
