using UnityEngine;
using System.Collections;

public class PrintScore : MonoBehaviour {
	
	
	public CalculScore calcScore;
	
	void OnGUI()
	{
		Rect r = new Rect(Screen.width - 320, 10, 100, 20);
		GUI.Box(r,"Score : "+calcScore._Score.ToString());
		
		Rect s = new Rect(Screen.width - 220, 10, 200, 20);
		string frameball = "Essai: {0}    Ballon:{1}";
		
		GUI.Box(s, string.Format(frameball, calcScore._Frame, calcScore._FrameBall));	
		GUI.backgroundColor = Color.gray; 


		Rect t = new Rect(Screen.width - 300, 30, 300, 20);

	    Rect q = new Rect(Screen.width - 300, 40, 300, 20);

		if (calcScore._FrameBall == 0 && calcScore._Score % 10 != 0) {
		    GUI.Label (t, "Vous avez perdu :(");
        } else if (calcScore._Frame /2== 1 && calcScore._Score % 10 == 0) {	
		    GUI.Label (t, "Vous avez gagné :) ");

		}
	}
}
