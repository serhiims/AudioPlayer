using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CallbackButton : MonoBehaviour, IPointerClickHandler {
	public event Action clickedCallback;

	public void OnPointerClick(PointerEventData eventData){
		if(clickedCallback != null){
			clickedCallback();
		}
	}
}
