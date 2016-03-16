using UnityEngine;
using System.Collections;

public class GlobalController : MonoBehaviour {
    public AnimationPlayer startAnimPlayer;
    AnimationPlayer[] animPlayers;
    public bool debugMode = false;
	// Use this for initialization
	void Awake() {
        animPlayers = FindObjectsOfType<AnimationPlayer>();
        if (debugMode)
            return;
        foreach(AnimationPlayer ap in animPlayers)
        {
            ap.gameObject.SetActive(false);
        }
        startAnimPlayer.EnterAnimation();
	}
}
