using UnityEngine;
using System.Collections;

public class GateScore : MonoBehaviour {

	// Wert um Punkte fuer das Durchschießen eines Tores einzugeben
	public int points;

	// Der GameController
	private GameController gameController;

	// Ein Boolean um zu ermitteln ob der Spieler auch durch das Tor gekommen ist und nicht nur dran abgeprallt
	private bool closed;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		closed = false;
	}

	void OnTriggerEnter (Collider other) {
		// Falls das Tor nicht geschlossen ist und ein Spieler ins Triggerfeld kommt
		if (!closed && other.CompareTag("Player")) {
			// Schließe das Tor und erhoehe den PlayerScore
			gameController.AddScore (points);
			closed = true;
		}
	}

	// Falls der Spieler austritt, oeffne das Tor wieder
	void OnTriggerExit (Collider other) {
		if (other.CompareTag("Player"))
			closed = false;
	}
}
