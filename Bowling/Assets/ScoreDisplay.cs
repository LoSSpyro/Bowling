using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {
	
	
	public ScoreKeeper _ScoreKeeper;
	
	void OnGUI()
	{
		Rect r = new Rect(Screen.width - 320, 10, 300, 20);
		GUI.Box(r, _ScoreKeeper._Score.ToString());		
		Rect s = new Rect(Screen.width - 320, 30, 300, 20);
		string frameball = "Frame: {0}    Ball:{1}";
		
		GUI.Box(s, string.Format(frameball, _ScoreKeeper._Frame, _ScoreKeeper._FrameBall));	
		
		// todo: prettify!
		Rect t = new Rect(Screen.width - 320, 60, 300, 20);
		GUI.Label(t, "Use left and right arrows to aim");

	    Rect q = new Rect(Screen.width - 320, 90, 300, 20);
		GUI.Label(q, "hold space bar to charge, release to throw");

	}
}
