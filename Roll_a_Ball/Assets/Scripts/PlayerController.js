#pragma strict

import UnityEngine.UI;


private var rb : Rigidbody;
private var count : int;

var speed : float;
var countText : Text;
var winText : Text;

function Start () {
	rb = GetComponent.<Rigidbody>();
	count = 0;
	SetCountText();
	winText.text = '';
}

function FixedUpdate () {
	var moveHorizontal : float = Input.GetAxis("Horizontal");
	var moveVertical : float = Input.GetAxis("Vertical");

	var movement : Vector3 = new Vector3 (moveHorizontal, 0, moveVertical);

	rb.AddForce(movement * speed);
}

function OnTriggerEnter (other : Collider) {
	if (other.gameObject.CompareTag("Pickup")) {
		other.gameObject.SetActive(false);
		count += 1;
		SetCountText();
	}
}

function SetCountText() {
	countText.text = "Count: " + count;
	if (count >= 8) {
		winText.text = 'You win!';
	}
}