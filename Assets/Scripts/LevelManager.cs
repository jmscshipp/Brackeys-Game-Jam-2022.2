using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // variables set for each level
    public int archerNum = 0;
    public int doubleArcherAngleNum = 0;
    public int doubleArcherStraightNum = 0;
    public int swordsmanNum = 0;
    public int tankNum = 0;
    public int healerNum = 0;
    public int defenderNum = 0;
    public int otherNum = 0;

    public GameObject archerPrefab;
    public GameObject doubleArcherAnglePrefab;
    public GameObject doubleArcherStraightPrefab;
    public GameObject swordsmanPrefab;
    public GameObject tankPrefab;
    public GameObject healerPrefab;
    public GameObject defenderPrefab;

    int charactersPlaced;
    int characterTotal;
    int charactersAlive;
    List<GameObject> placedCharacters = new List<GameObject>();
    bool gameReady;
    UIManager uiManager;
    CharacterBank bank;
    LevelAudio audioController;

    // this variable determines if another turn will be played after the first one
    public bool turnValid;
    public int activeAttackNum;

    void Start()
    {
        charactersPlaced = 0;
        characterTotal = archerNum + swordsmanNum + doubleArcherAngleNum + doubleArcherStraightNum + tankNum + healerNum + defenderNum;
        charactersAlive = characterTotal + otherNum;
        gameReady = false;
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        bank = GameObject.Find("CHARACTERBANK").GetComponent<CharacterBank>();
        audioController = GetComponent<LevelAudio>();

        uiManager.UpdateCharacterPlacementUI(0, characterTotal);
        SetUpCharacters();
    }

    void Update()
    {

    }

    void SetUpCharacters()
    {
        // set up archers
        for (int i = 0; i < archerNum; i++)
        {
            GameObject newChar = Instantiate(archerPrefab);
            bank.AddToBank(newChar);
        }
        // set up double archer angle
        for (int i = 0; i < doubleArcherAngleNum; i++)
        {
            GameObject newChar = Instantiate(doubleArcherAnglePrefab);
            bank.AddToBank(newChar);
        }
        // set up double archer straight
        for (int i = 0; i < doubleArcherStraightNum; i++)
        {
            GameObject newChar = Instantiate(doubleArcherStraightPrefab);
            bank.AddToBank(newChar);
        }
        // set up swordsmen
        for (int i = 0; i < swordsmanNum; i++)
        {
            GameObject newChar = Instantiate(swordsmanPrefab);
            bank.AddToBank(newChar);
        }
        // set up tanks
        for (int i = 0; i < tankNum; i++)
        {
            GameObject newChar = Instantiate(tankPrefab);
            bank.AddToBank(newChar);
        }
        // set up healers
        for (int i = 0; i < healerNum; i++)
        {
            GameObject newChar = Instantiate(healerPrefab);
            bank.AddToBank(newChar);
        }
        // set up defenders
        for (int i = 0; i < defenderNum; i++)
        {
            GameObject newChar = Instantiate(defenderPrefab);
            bank.AddToBank(newChar);
        }
    }

    public void BeginTurns() //public to be activated by ui
    {
        audioController.PlayLevelStartAudio();

        turnValid = false;
        uiManager.UpdateCharactersAliveUI(charactersAlive); // reset ui to display characters remaining instead of placed

        if (gameReady)
        {
            gameReady = false;
            foreach (GameObject character in placedCharacters) // goes through the list of all placed characters and tells them to shoot
            {
                if (character.GetComponent<Character>() != null)
                {
                    character.GetComponent<Character>().Attack();
                }
                else
                {
                    character.GetComponent<StationaryCharacter>().Attack();
                }
                
            }
        }
    }

    // called by immovable character at the start of the game
    public void ImmovableCharacterPlaced(GameObject character)
    {
        placedCharacters.Add(character);
    }

    // called by a character when they're placed on the board
    public void CharacterPlaced(GameObject character)
    {
        Debug.Log("Character placed");
        charactersPlaced++;
        placedCharacters.Add(character);
        uiManager.UpdateCharacterPlacementUI(charactersPlaced, characterTotal);
        if (charactersPlaced == characterTotal)
        {
            audioController.PlayCharacterPlacedAudio();
            gameReady = true;
        }
    }

    // called by a character when they're removed from the board
    public void CharacterRemoved(GameObject character)
    {
        charactersPlaced--;
        placedCharacters.Remove(character);
        uiManager.UpdateCharacterPlacementUI(charactersPlaced, characterTotal);
        if (charactersPlaced < characterTotal)
            gameReady = false;
    }

    public void CharacterKilled(GameObject character)
    {
        charactersAlive--;
        placedCharacters.Remove(character);

        uiManager.UpdateCharactersAliveUI(charactersAlive);
        if (charactersAlive < 1)
        {
            // level complete
        }
    }

    // called by character if hit, makes the turn valid
    public void ValidateTurn()
    {
        turnValid = true;
    }

    // called by attack when spawned, to keep track of turn status
    public void AttackSpawned()
    {
        activeAttackNum++;
    }

    // called by attack when over, to keep track of turn status
    public void AttackDespawned()
    {
        activeAttackNum--;

        // check to see if we should start another turn
        if (activeAttackNum == 0)
        {
            if (turnValid)
            {
                gameReady = true;
                BeginTurns();
            }
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}