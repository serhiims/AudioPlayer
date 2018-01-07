using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.UI;


public class Player : MonoBehaviour {
	public TextMeshProUGUI infoTF;
	public AudioSource _audio;
	public ToggleButton playStartButton;
	public CallbackButton prevButton;
	public CallbackButton nextButton;
	public ToggleButton randomButton;

	private Coroutine _onCompleteCorutine;

	private List<string> _soundtrackPathes;
	private int _currentIndex = 0;

	void Start () 
	{
		playStartButton.clickedCallback += PlayStartMusic;
		prevButton.clickedCallback += PrevSoundtrack;
		nextButton.clickedCallback += NextSoundtrack;
		FillSoundtrackPathes ();
		bool empty = IsEmptySoundtrackPathes ();
		_currentIndex = ValidateSoundTrackIndex (_currentIndex);
		StartCoroutine (LoadSoundTrack(_currentIndex));
	}

	private int ValidateSoundTrackIndex(int index) {		
		if (randomButton.IsOn && _soundtrackPathes.Count > 1) {
			index = UnityEngine.Random.Range (0, _soundtrackPathes.Count - 1);
			if (index == _currentIndex) {
				return ValidateSoundTrackIndex (index);
			}
		}
		index = index < 0 ? _soundtrackPathes.Count - 1 : index;
		index = index >= _soundtrackPathes.Count ? 0 : index;
		return index;
	}


	private IEnumerator LoadSoundTrack(int index){
		if (IsEmptySoundtrackPathes ()) {
			printInfo ("Soundtracks not found");
			yield return null;
		}
		var loader = new WWW ("file://" + _soundtrackPathes [index]);
		yield return loader;
		if (!String.IsNullOrEmpty (loader.error)) {
			_soundtrackPathes.RemoveAt (index);
			printInfo (loader.error);
		} else {
			StartPlaying (loader.GetAudioClip ());
		}
	}

	private void StartPlaying(AudioClip audioClip){
		if (audioClip != null) {
			if (_audio.clip != null) {
				_audio.Stop ();
				_audio.clip.UnloadAudioData ();
			}
			_audio.clip = audioClip;
			_audio.Play ();
			playStartButton.IsOn = true;
			printInfo ("Current index is " + _currentIndex);
			StartOnCompleteCorutine(_audio.clip.length);
		}
	}

	private void StopOnCompleteCourutine(){
		if (_onCompleteCorutine != null) {
			StopCoroutine (_onCompleteCorutine);
		}
	}

	private void StartOnCompleteCorutine(float sec){
		StopOnCompleteCourutine ();
		_onCompleteCorutine = StartCoroutine (SetOnCompeteClip (sec));
	}

	private IEnumerator SetOnCompeteClip(float sec){
		yield return new WaitForSeconds(sec);
		NextSoundtrack ();
	}

	private void NextSoundtrack(){
		_currentIndex = ValidateSoundTrackIndex (_currentIndex + 1);
		StartCoroutine (LoadSoundTrack(_currentIndex));
	}

	private void PrevSoundtrack(){
		_currentIndex = ValidateSoundTrackIndex (_currentIndex - 1);
		StartCoroutine (LoadSoundTrack(_currentIndex));
	}

	private void PlayStartMusic(bool isPlaying){
		if (!isPlaying && _audio.isPlaying) {
			StopOnCompleteCourutine ();
			_audio.Pause ();
		} else if(isPlaying && !_audio.isPlaying){
			_audio.Play ();
			StartOnCompleteCorutine (_audio.clip.length);
		}
		playStartButton.IsOn = isPlaying;

	}

	private bool IsEmptySoundtrackPathes(){
		return _soundtrackPathes == null || _soundtrackPathes.Count == 0;
	}


	private void FillSoundtrackPathes() {
		DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/SoundTracks");
		FileInfo [] files = di.GetFiles("*.ogg",SearchOption.TopDirectoryOnly);
		if (files.Length > 0) {
			_soundtrackPathes = new List<string> ();
			for(int i = 0; i < files.Length; i++){
				_soundtrackPathes.Add(files[i].FullName);
			}

		}
	}

	private void printInfo(string message){
		if (infoTF != null) {
			infoTF.text = message;
		}
	}

}