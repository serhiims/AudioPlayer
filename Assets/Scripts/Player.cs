using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;
using TMPro;
using System;


public class Player : MonoBehaviour {
	public TextMeshProUGUI infoTF;
	private List<string> _soundtrackPathes;
	private int _currentIndex = -1;
	public AudioSource _audio;

	void Start () 
	{
		bool empty = !FillSoundtrackPathes ();
		if (empty) {
			printInfo ("Soundtracks not found");
			return;
		} 
		StartCoroutine (LoadSoundTrack());
	}

	private IEnumerator LoadSoundTrack(){
		int nextIndex = UnityEngine.Random.Range (0, _soundtrackPathes.Count - 1);
		var loader = new WWW ("file://" + _soundtrackPathes [nextIndex]);
		yield return loader;
			if (!String.IsNullOrEmpty (loader.error)) {
				printInfo (loader.error);
			} else {
				StartPlaying (loader.GetAudioClip ());
			}
	}

	private void StartPlaying(AudioClip audioClip){
		if (audioClip != null) {
			_audio.clip = audioClip;
			_audio.Play ();
		}
	}

	private bool FillSoundtrackPathes() {
		DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/SoundTracks");
		FileInfo [] files = di.GetFiles("*.ogg",SearchOption.TopDirectoryOnly);
		if (files.Length > 0) {
			_soundtrackPathes = new List<string> ();
			for(int i = 0; i < files.Length; i++){
				_soundtrackPathes.Add(files[i].FullName);
			}

		}
		return _soundtrackPathes != null && _soundtrackPathes.Count > 0;
	}

	private void printInfo(string message){
		if (infoTF != null) {
			infoTF.text = message;
		}
	}

}