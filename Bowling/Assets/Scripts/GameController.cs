using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// Die Text fuer Score, Zeit und den Restart
	public Text scoreText;
	public Text timeText;
	public Text restartText;

	// Werte fuer die verbleibende Zeit, und den Score
	public float timeLeft;
	private int score;

	private bool gameOver;

	// Initialisiere den Score und die Textfelder
	void Start () {
		score = 0;
		scoreText.text = "Points: 0";
		timeText.text = "Time left: 60:000";
	}
	
	// Update is called once per frame
	void Update () {
		// Verringere die Zeit dem Frame entsprechend
		timeLeft -= Time.deltaTime;
		// Falls noch verbleibende Zeit uebrig ist, berechen eine Darstellung
		// in 0:000 und schreibe sie auf das Zeit-Textfeld
		if (timeLeft >= 0f) {
			int seconds = Mathf.FloorToInt (timeLeft);
			int millieconds = Mathf.FloorToInt ((timeLeft - seconds) * 1000);
			string nTime = string.Format ("{0:0}:{1:00}", seconds, millieconds);
			timeText.text = "Time left: " + nTime;
		}
		// Falls keine Zeit mehr uebrig ist, das Spiel aber noch nicht beendet ist
		if (timeLeft < 0f && !gameOver) {
			timeText.text = "Time left: 0:000";
			// Beende das Spiel und setze die Szene zurueck
			gameOver = true;
			GameObject.FindGameObjectWithTag ("Player").SendMessage ("GameOver");
		}

		// Falls das Spiel zuende ist aktiviere das restart Label und lade die Szene neu
		// falls A gedrueckt wird
		if (gameOver) {
			restartText.enabled = true;
			if (Input.GetKeyDown (KeyCode.A))
				SceneManager.LoadScene (0);
		}
	}

	// Eine Funktion um den Score zu erhoehen
	public void AddScore (int points) {
		score += points;
		scoreText.text = "Points: " + score;
	}


}
