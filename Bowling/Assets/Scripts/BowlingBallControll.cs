using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BowlingBallControll : MonoBehaviour {

	// Die Materialien zwischen denen die Kugel wechseln soll
	public Material material1;
	public Material material2;

	// Wie schnell der Ball die Maximale Kraft erreicht haben soll
	public float speedToMaximum;
	// Die maximale Kraft, die erricht werden kann
	public float maxForce;

	// Maus wurde gedrueckt
	private bool mousePressed;
	// Maus wurde geklickt
	private bool mouseReleased;
	// Maus wurde bereits losgelassen
	private bool mouseWasReleased;
	// Wie lange die Maus gedrueckt wird
	private float mousePressedTime = 0f;
	// Wie lange die Kugel schon losgelassen wurde
	private float throwTime = 0f;

	// Die Distanz zur Kamera
	private float dist;
	// Die Kraft die auf die Kugel wirkt
	private float force = 0f;

	// Ist das Spiel vorbei?
	private bool gameOver;
	//Kann das Spiel beendet werden
	private bool endGame;

	// Text fuer neuen Wurf
	public Text newThrowText;

	Renderer rend;
	Rigidbody rb;


	// Use this for initialization
	void Start () {
		// Initialisierung des Renderes sowie des Rigidbodys
		rend = GetComponent<Renderer> ();
		rend.material = material1;
		rb = GetComponent<Rigidbody> ();

		// Ermitteln der benoetigten Distanz fuer die SetBallOnMouse funktion
		dist = transform.position.z - Camera.main.transform.position.z;
		mousePressed = false;
		mouseReleased = false;
		mouseWasReleased = false;

	}
	
	// Update is called once per frame
	void Update () {
		

		if (Input.GetMouseButtonDown (0))
			mousePressed = true;

		if (!mouseReleased && !mouseWasReleased) {
			SetBallOnMouse ();
		}

		// Falls das Spiel noch nicht vorbei ist
		if (!gameOver) {

			if (Input.GetMouseButtonUp (0)) {
				mousePressed = false;
				mouseReleased = true;
			}

			// Falls Maus gedrueckt wurdem, aber nocht nicht losgelassen,
			// erhoehe die Kraft, die am Ende auf den Ball wirken soll
			// und aendere das Material dementsprechen
			if (mousePressed && !mouseWasReleased) {
				mousePressedTime += Time.deltaTime;
				force = Mathf.MoveTowards (0f, maxForce, mousePressedTime * speedToMaximum * maxForce);
				LerpColor (material1, material2);
			}

			// Falls die das erste Mal losgelassen wird
			if (mouseReleased && !mouseWasReleased) {
				//Wirke die Kraft auf die Kugel
				rb.AddForce (Vector3.forward * force);
				mouseReleased = false;
				mouseWasReleased = true;
			}

			// Falls die Maus losgelassen wurde, zaehle mit wie lange
			if (mouseWasReleased)
				throwTime += Time.deltaTime;

			// Falls die Zeit laenger als 2 Sekunden betraegt oder die Kugel
			// unterhalb des Bodens ist aktivieren das InfoLabel fuer "neuen Wurf"
			if (throwTime > 2f || transform.position.y < 0f) {
				newThrowText.enabled = true;
				// Falls dann noch der R Knopf gedrueckt wurde, Resete die Szene
				if (Input.GetKeyDown (KeyCode.R)) {
					Reset ();
				}
			}
			// Fall das Spiel vorbei ist (gameOver == true) setze die Szene zurueck
		} else if (!endGame) {
			Reset ();
			endGame = true;
		}
	}


	// FUnktion um von Material 1 zu Material 2 zu wechseln
	void LerpColor (Material mat1, Material mat2) {
		float lerp = Mathf.MoveTowards (0f, 1f, mousePressedTime * speedToMaximum);
		rend.material.Lerp (mat1, mat2, lerp);
	}

	// Funktion um den Ball auf der Maus zu halten
	void SetBallOnMouse () {
		Vector3 pos = Input.mousePosition;
		pos.z = dist;
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (pos);
		mousePosition.y = (mousePosition.y < 0.55f) ? 0.55f : mousePosition.y;
		transform.position = mousePosition;
	}

	// Funktion um die Szene zurueckzusetzen
	void Reset() {
		mousePressed = false;
		mouseReleased = false;
		mouseWasReleased = false;
		mousePressedTime = 0f;
		throwTime = 0f;
		force = 0f;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rend.material = material1;
		newThrowText.enabled = false;

		// Sende Reset an jeden Pin in der Szene
		foreach (GameObject pin in GameObject.FindGameObjectsWithTag("Pin"))
			pin.SendMessage ("Reset");
	}

	// Oeffentliche Funktion, damit der GameController dem Spieler sagen kann,
	// dass das Spiel vorbei ist.
	public void GameOver() {
		gameOver = true;
	}
}
