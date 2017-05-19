using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	// make game manager public static so can access this from other scripts
	public static GameManager gm;

	// public variables
	public int score=0;

	//public enum gameStates {Playing, Death, GameOver};
	//public gameStates gameState = gameStates.Playing;

	[Tooltip("If not set, the player will default to the gameObject tagged as Player.")]
	public GameObject player;

	public bool canBeatLevel = false;
	public int beatLevelScore=0;

	public int startTime;

	public Text mainScoreDisplay;
	public Text mainTimerDisplay;

	public GameObject gameOverScoreOutline;

	public AudioSource musicAudioSource;

	public bool gameIsOver = false;

	public GameObject playAgainButtons;
	public string playAgainLevelToLoad;

	public GameObject nextLevelButtons;
	public string nextLevelToLoad;

	public GameObject restartGameButtons;

	//private float currentTime;
	//private Health playerHealth;
	private bool playerIsAlive = true;

	// setup the game
	void Start () {

		// set the current time to the startTime specified
		//currentTime = startTime;

		// get a reference to the GameManager component for use by other scripts
		if (gm == null) 
			gm = this.gameObject.GetComponent<GameManager>();

		if (player == null) {
			player = GameObject.FindWithTag("Player");
		}

		playerIsAlive = true;
		//playerHealth = player.GetComponent<Health>();


		// init scoreboard to 0
		mainScoreDisplay.text = "0";

		// inactivate the gameOverScoreOutline gameObject, if it is set
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (false);

		// inactivate the playAgainButtons gameObject, if it is set
		if (playAgainButtons)
			playAgainButtons.SetActive (false);

		// inactivate the nextLevelButtons gameObject, if it is set
		//if (nextLevelButtons)
		//	nextLevelButtons.SetActive (false);
	}

	// this is the main game event loop
	void Update () {
		/*
		switch (gameState)
		{
		case gameStates.Playing:
			if (playerHealth.isAlive == false)
			{
				// update gameState
				gameState = gameStates.Death;

			} 
			break;
		case gameStates.Death:
			EndGame ();
			//gameState = gameStates.GameOver;
			break;
		case gameStates.GameOver:
			//EndGame ();
			break;
		}
		*/
		


		if (!gameIsOver) {
			if (playerIsAlive == false) {
				DestroyAllObjects ();
				EndGame ();
			}
			//if (canBeatLevel && (score >= beatLevelScore)) {  // check to see if beat game
			//	BeatLevel ();
			//} else 
		}
	}

	void EndGame() {
		// game is over
		gameIsOver = true;
		//gameState = gameStates.GameOver;

		// repurpose the timer to display a message to the player
		mainScoreDisplay.text = "GAME OVER, SCORE " + score;

		// activate the gameOverScoreOutline gameObject, if it is set 
		//if (gameOverScoreOutline)
		//	gameOverScoreOutline.SetActive (true);
	
		// activate the playAgainButtons gameObject, if it is set 
		if (playAgainButtons)
			playAgainButtons.SetActive (true);

		// restart game buttons
		if (restartGameButtons) 
		{
			restartGameButtons.SetActive (true);
		}

		// reduce the pitch of the background music, if it is set 
		if (musicAudioSource)
			musicAudioSource.pitch = 0.5f; // slow down the music
	}

	void DestroyAllObjects()
	{
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("Enemy");
		for(var i = 0 ; i < gameObjects.Length ; i ++)
		{
			Destroy(gameObjects[i]);
		}
	}

	public void killPlayer () {
		playerIsAlive = false;
	}

	// public function that can be called to update the score or time
	public void targetHit (int scoreAmount, float timeAmount)
	{
		// increase the score by the scoreAmount and update the text UI
		score += scoreAmount;
		mainScoreDisplay.text = score.ToString ();
		
		// increase the time by the timeAmount
		//currentTime += timeAmount;
		
		// don't let it go negative
		//if (currentTime < 0)
		//	currentTime = 0.0f;

		// update the text UI
		//mainTimerDisplay.text = currentTime.ToString ("0.00");
	}

	// public function that can be called to restart the game
	public void RestartGame ()
	{
		// we are just loading a scene (or reloading this scene)
		// which is an easy way to restart the level
		Application.LoadLevel (playAgainLevelToLoad);
	}

	// public function that can be called to go to the next level of the game
	public void NextLevel ()
	{
		// we are just loading the specified next level (scene)
		Application.LoadLevel (nextLevelToLoad);
	}

	public void StartGameFromFirstLevel()
	{
		Application.LoadLevel ("Level1");
	}
}
