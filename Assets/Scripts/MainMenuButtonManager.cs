using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuButtonManager : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onQuestionPress(){
		SceneManager.LoadScene("mainmenu_question");
	}

	public void onInfoPress(){
		SceneManager.LoadScene("mainmenu_info");
	}

	public void onBackPress(){
		SceneManager.LoadScene ("main_menu");
	}
}
