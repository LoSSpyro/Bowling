using UnityEngine;
using System.Collections;

public class PinScore : MonoBehaviour {

	// Wert um Punkte fuer das Durchschießen eines Tores einzugeben
	public int points;

	// Die Transform Componente um den Spawn der Pins festzulegen
	private Transform ground;
	private Rigidbody rb;
	private GameController gameController;

	// Ein Wert um festzustellen ob der Score schon gesendet wurde
	private bool scoreSent = false;

	// Use this for initialization
	void Start () {
		ground = GameObject.FindGameObjectWithTag ("Ground").transform;
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		rb = GetComponent<Rigidbody> ();
		// Initialisiere die Pins mit einer zufaelligen Position
		Reset ();
	}
	
	// Update is called once per frame
	void Update () {

		// Falls Pins runter falls und noch kein Score gesendet haben, sende diesen
		if (transform.position.y < 0f && !scoreSent)
			sendScore ();
	}
		
	void OnTriggerEnter (Collider other) {
		// Falls die Pins umgefallen sind und noch keinen Score gesendet haben
		// sende diesen.
		if (other.CompareTag ("Ground") && !scoreSent)
			sendScore ();
		
	}

	// Eine Funktion, die den Score an den GameController sendet
	void sendScore() {
		gameController.AddScore (points);
		scoreSent = true;
	}

	// Eine Funktion die die Pins zuruecksetzt
	void Reset () {
		scoreSent = false;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		transform.position = RandomPinPosition();
		transform.rotation = Quaternion.identity;
	}

	// Eine Funktion die eine neue, zufaellige und freie Position ermittelt
	Vector3 RandomPinPosition () {
		Vector3 offset = ground.position;
		Vector3 scale = ground.localScale;
		float xPosition = Random.Range (scale.x * -5f + 0.5f + offset.x, scale.x * 5f - 0.5f + offset.x);
		float yPosition = 0.5f;
		float zPostion = Random.Range (4f, scale.z * 5f - 0.5f + offset.z);
		Vector3 newPosition = new Vector3 (xPosition, yPosition, zPostion);
		if (!isOccupied (newPosition))
			return newPosition;
		else return RandomPinPosition();
	}

	// Eine Funktion, die ermittelt ob die angegebene Position bereit belegt ist oder nicht
	bool isOccupied(Vector3 pos) {
		if (Physics.OverlapSphere (pos, 0.25f).Length == 0)
			return false;
		else
			return true;
	}
}
