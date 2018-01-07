using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {
	private const string MUSIC_GROUP_NAME = "Music Volume";
		
	public AudioMixer mixer;
	public Slider slider;

	// Use this for initialization
	void Start () {
		float decibelValue;
		mixer.GetFloat (MUSIC_GROUP_NAME, out decibelValue);
		slider.value = MathUtil.DecibelToLinear (decibelValue);
	}

	public void OnSliderValueChanged(float newValue){
		mixer.SetFloat (MUSIC_GROUP_NAME, MathUtil.LinearToDecibel (newValue));
	}
}
