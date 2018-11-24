using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeModel : MonoBehaviour {

	private GameObject rHand;
	private bool isAnimating = false;
	private bool handRotated = false;
	private bool loopAnim = true;

	// Use this for initialization
	void Start () {
		rHand = GameObject.Find ("R_Hand");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space"))
			isAnimating = !isAnimating;
		Animator anim = GetComponent<Animator> ();
		if (isAnimating) {
			if (!anim.enabled) {
				anim.enabled = true;
				if (loopAnim)
					anim.Play ("Dude Walk", -1, 0.0f);
				else
					anim.Play ("Dude Step", -1, 0.0f);
			}
			handRotated = false;
			if (Input.GetKeyDown (KeyCode.RightBracket))
				anim.speed += 0.1f;
			if (Input.GetKeyDown (KeyCode.LeftBracket) && (anim.speed > 0)) {
				if (anim.speed < 0.1f)
					anim.speed = 0;
				else
					anim.speed -= 0.1f;
			}
			if (Input.GetKeyDown ("l")) {
				loopAnim = !loopAnim;
				anim.enabled = false;
			}
		} else {
			anim.enabled = false;
			anim.StopPlayback ();
			if (Input.GetKeyDown ("`") && handRotated) {
				handRotated = false;
				rHand.transform.Rotate (Vector3.forward, 50);
			} else if (Input.GetKeyDown ("`") && !handRotated) {
				handRotated = true;
				rHand.transform.Rotate (Vector3.forward, -50);
			}
		}
	}
}
