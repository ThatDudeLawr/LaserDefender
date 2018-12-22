using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	public AudioClip startClip;
	public AudioClip endClip;
	public AudioClip gameClip;
	private AudioSource music;
	
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.loop=true;
			music.Play();
		}
		
	}
//OnLevelWasLoaded is deprecated, this is the new way of doing it


private void OnEnable() {
	SceneManager.sceneLoaded += OnSceneLoaded; // subscribe
}
private void OnDisable() {
	SceneManager.sceneLoaded -= OnSceneLoaded; //unsubscribe
}

// The replacement for the OnLevelWasLoaded method. You may name it as you want. Make sure to subscribe/unsubscribe the correct method name (see above)
private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
	Debug.Log("Music Player: loaded level "+ scene.buildIndex);
	music.Stop();
	if(scene.buildIndex == 0){
		music.clip = startClip;
	}else if(scene.buildIndex == 1){
		music.clip = gameClip;
		}else if(scene.buildIndex == 2){
			music.clip = endClip;
		}
		music.loop=true;
		music.Play();
}


/*	void OnLevelWasLoaded(int level){

	}
	*/
}
