using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public LevelStruct Level;
	public MonsterSpawner monsterSpawner;

	[HideInInspector]
	public static int currentLevel;	// the next index in levelArr[]

	// is static so that derived classes don't get their own;
	// any changes will be made to the one and only int score
	public int score;			// derived classes add to this when monsters are killed or humans saved
	public GameObject scoreObject;		// used to hold the Score GUI Text GameObject

	public int wave;				// holds the current wave for the GUI text
	public GameObject waveObject;

	public int lives;			// holds the number of lives left for the GUI text
	public GameObject livesObject;

	public int levelName;			// holds the name of the level for the GUI text
	public GameObject levelNameObject;

	public Light groundLight;	// the light that illuminates and colors the ground layer

	public bool isPlayerAlive;	// self explanatory

	public GameObject player1;	// represents the Player1 GameObject

	private Vector3 spawnPos;

	private GameObject camera;	// Main Camera GameObject

	private bool isGameOver = false;
	private bool levelWon = false;

	// mainmenu, level1, level2, pausemenu, credits
	//protected static string gameState;

	public int monstersAlive;
	public int mA;

	protected LevelStruct[] levelArr;		// holds all the levels as LevelStructs
	protected GameObject[] monsterArr;		// holds all the monsters present in that wave; gets cleared at the end of each wave/level
	protected GameObject[] mineArr;
	

	// Use this for initialization
	void Start () {

		score = 0;		// starting score at 0
		wave = 0;		// starting wave - 1 ; wave will increase to 1 when the first level is created by LevelMaker()
		lives = 3;		// if player dies when lives == 0 then game over
		spawnPos = new Vector3(-0.3733258f, -0.1376212f, 0.91f);
		levelArr = new LevelStruct[200];

		// change this to start game at different levels ( use 4 to start at level 5, etc
		currentLevel = 0;	// next index in levelArr[]
		player1 = GameObject.Find("Player1");	
		camera = GameObject.Find("Main Camera");

		// creates player
		/**player1 = Instantiate(Resources.Load("Player1"),
										spawnPos,
										transform.rotation) as GameObject;**/

		isPlayerAlive = true;	// setting the player to alive; the monsters will change this

		monstersAlive = 0;

		////
		// begin level definitions
		LevelStruct level1 = LevelStruct.CreateInstance("LevelStruct") as LevelStruct;
		levelArr[0] = level1;
		level1.stumps = 5;
		level1.vasils = 0;
		level1.saucer1s = 5;
		level1.mine_boxes = 0;
		level1.sumMonsters();
		level1.name = "Hello, World!";

		LevelStruct level2 = LevelStruct.CreateInstance("LevelStruct") as LevelStruct;
		levelArr[1] = level2;
		level2.stumps = 6;
		level2.vasils = 0;
		level2.mine_boxes = 4;
		level2.sumMonsters();
		level2.name = "Another day in paradise...";

		LevelStruct level3 = LevelStruct.CreateInstance("LevelStruct") as LevelStruct;
		levelArr[2] = level3;
		level3.stumps = 3;
		level3.vasils = 4;
		level3.mine_boxes = 6;
		level3.sumMonsters();
		level3.name = "I really wish there was some upbeat music playing...";

		LevelStruct level4 = LevelStruct.CreateInstance("LevelStruct") as LevelStruct;
		levelArr[3] = level4;
		level4.stumps = 0;
		level4.vasils = 10;
		level4.mine_boxes = 2;
		level4.sumMonsters();
		level4.name = "WTF are these...pills?";

		LevelStruct level5 = LevelStruct.CreateInstance("LevelStruct") as LevelStruct;
		levelArr[4] = level5;
		level5.stumps = 3;
		level5.vasils = 10;
		level5.mine_boxes = 20;
		level5.sumMonsters();
		level5.name = "Bet you wish you played more minesweeper now...";

		LevelStruct level6 = LevelStruct.CreateInstance("LevelStruct") as LevelStruct;
		levelArr[5] = level6;
		level6.stumps = 10;
		level6.vasils = 10;
		level6.mine_boxes = 0;
		level6.mine_spikeballs = 6;
		level6.sumMonsters();
		level6.name = "Serksth Lerver";
		// end level definitions
		////



		//gameState = "level1";

		//LevelMaker(currentLevel);
	}
	
	// Update is called once per frame
	void Update () {
		mA = monstersAlive;
		if (isGameOver == false) {
			// updates Score GUIText with value of score
			scoreObject.guiText.text = score.ToString();
			waveObject.guiText.text = "wave " + wave.ToString();
			livesObject.guiText.text = "lives X " + lives.ToString();
			levelNameObject.guiText.text = levelArr[(currentLevel == 0 ? currentLevel : currentLevel - 1)].name;
		} else {
			// go to main menu or continue screen if player ran out of lives
		}

		if (monstersAlive <= 0) {	// monstersAlive is in MonsterSpawner.cs
			Debug.Log("monstersAlive if expression triggered");
			currentLevel++;
			
			if (wave == 0) { } else { ClearMonsters(); }		// clears the board of monsters
			Debug.Log("LevelMaker called");
			LevelMaker(currentLevel);	// resets and fills monsterArr[], spawns monsters, 
			Debug.Log("LevelMaker ran successfully");
		}
	}

	void CreateLevels () {
		
	}

	void LevelMaker (int levelIndex) {
		
		player1.transform.position = spawnPos;
		//Debug.Log("player moved");
		LevelStruct workingLevel = levelArr[levelIndex - 1];
		//Debug.Log("workingLevel updated");
		monsterArr = new GameObject[workingLevel.totalMonsters];
		//Debug.Log("monsterArr updated");
		//monsterArr = new GameObject[workingLevel.totalMines];
		monsterSpawner.SpawnMonsters(workingLevel.stumps,
									workingLevel.vasils,
									workingLevel.saucer1s,
									workingLevel.mine_boxes,
									workingLevel.mine_spikeballs);
		//Debug.Log("monsters spawned");
		wave++;
		Debug.Log("Level " + (currentLevel) + " created");
	}

	void ClearMonsters() {
		for (int i = 0; i < monsterArr.Length; i++) {
			Destroy(monsterArr[i]);
		}
	}

	void ClearMines() {
		for (int i = 0; i < monsterArr.Length; i++) {
			//Destroy(monsterArr[i]);
		}
	}
}