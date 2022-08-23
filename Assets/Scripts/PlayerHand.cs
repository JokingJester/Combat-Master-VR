using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private InputActionProperty _button;
    [SerializeField] private XRDirectInteractor _direct;

    private bool _isSelected;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_isSelected == true && _button.action.ReadValue<float>() == 1)
        {
            IsSelectedFalse();
            OffsetInteractable hoverObject = _direct.GetOldestInteractableSelected().transform.GetComponent<OffsetInteractable>();
            if(hoverObject != null)
            {
                hoverObject.StartHovering();
            }
            Debug.Log(_direct.GetOldestInteractableSelected().transform.gameObject);
        }
    }

    public void IsSelectedTrue()
    {
        _isSelected = true;
    }  
    public void IsSelectedFalse()
    {
        _isSelected = false;
    }
}
