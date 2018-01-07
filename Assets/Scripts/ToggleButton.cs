using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ToggleButton : MonoBehaviour, IPointerClickHandler{
	public event Action<bool> clickedCallback;

	public GameObject iconOn;
	public GameObject iconOff;
	private bool _isOn = false;

	void Start () {
		ChangeIcon ();
	}

	public bool IsOn{
		get { return _isOn; }
		set { _isOn = value; ChangeIcon();}
	}

	private void ChangeIcon(){
		if (iconOn != null) {
			iconOn.SetActive (_isOn);
		}
		if (iconOff != null) {
			iconOff.SetActive (!_isOn);
		}
	}

	public void OnPointerClick(PointerEventData eventData){	
		IsOn = !_isOn;
		if (clickedCallback != null) {
			clickedCallback (_isOn);
		}
	}

}
