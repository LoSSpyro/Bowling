using UnityEngine;
using System.Collections;

public class BowlingBallControll : MonoBehaviour {

	public Material material1;
	public Material material2;
	public float speedToMaximum;
	public float maxForce;

	bool mousePressed;
	bool mouseReleased;
	float mousePressedTime = 0;

	float dist;

	Renderer rend;
	Rigidbody rb;


	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		rend.material = material1;

		rb = GetComponent<Rigidbody> ();

		dist = transform.position.z - Camera.main.transform.position.z;
		mousePressed = false;
		mouseReleased = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
			mousePressed = true;

		if (Input.GetMouseButtonUp(0)) {
			mousePressed = false;
			mouseReleased = true;
		}

		if (mousePressed) {
			mousePressedTime += Time.deltaTime;
			float force = Mathf.MoveTowards (0f, maxForce, mousePressedTime * speedToMaximum * maxForce);
			rb.AddForce (Vector3.forward * force);
			LerpColor (material1, material2);
		}

		if (!mouseReleased) {
			KeepBallOnMouse ();
		}

		if (Input.GetKey (KeyCode.R)) {
			Reset ();
		}
	}

	public void Reset () {
		mousePressed = false;
		mouseReleased = false;
		mousePressedTime = 0;
		rend.material = material1;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}

	void LerpColor (Material mat1, Material mat2) {
		float lerp = Mathf.MoveTowards (0f, 1f, mousePressedTime * speedToMaximum);
		rend.material.Lerp (mat1, mat2, lerp);
	}

	void KeepBallOnMouse () {
		Vector3 pos = Input.mousePosition;
		pos.z = dist;
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (pos);
		mousePosition.y = (mousePosition.y < 0.55f) ? 0.55f : mousePosition.y;
		transform.position = mousePosition;
	}
}
