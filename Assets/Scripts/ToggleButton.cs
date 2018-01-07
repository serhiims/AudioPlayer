using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour{

	public GameObject iconOn;
	public GameObject iconOff;
	private bool _isOn = true;

	void Start () {
		ChangeIcon ();
	}

	public bool IsOn
	{
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
}
