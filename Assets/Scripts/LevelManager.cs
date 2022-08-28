using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        charactersPlaced = 0;
        characterTotal = archerNum + swordsmanNum + doubleArcherAngleNum + doubleArcherStraightNum + tankNum + healerNum + defenderNum;
        charactersAlive = characterTotal + otherNum;
        gameReady = false;
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

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
            Instantiate(archerPrefab, new Vector3(-1.0f, i, -1.0f), Quaternion.identity);
        // set up double archer angle
        for (int i = 0; i < doubleArcherAngleNum; i++)
            Instantiate(doubleArcherAnglePrefab, new Vector3(-2.0f, i, -1.0f), Quaternion.identity);
        // set up double archer straight
        for (int i = 0; i < doubleArcherStraightNum; i++)
            Instantiate(doubleArcherStraightPrefab, new Vector3(-3.0f, i, -1.0f), Quaternion.identity);
        // set up swordsmen
        for (int i = 0; i < swordsmanNum; i++)
            Instantiate(swordsmanPrefab, new Vector3(-4.0f, i, -1.0f), Quaternion.identity);
        // set up tanks
        for (int i = 0; i < tankNum; i++)
            Instantiate(tankPrefab, new Vector3(-5.0f, i, -1.0f), Quaternion.identity);
        // set up healers
        for (int i = 0; i < healerNum; i++)
            Instantiate(healerPrefab, new Vector3(-6.0f, i, -1.0f), Quaternion.identity);
        // set up defenders
        for (int i = 0; i < defenderNum; i++)
            Instantiate(defenderPrefab, new Vector3(-7.0f, i, -1.0f), Quaternion.identity);
    }

    public void BeginTurns() //public to be activated by ui
    {
        Debug.Log("buttonPressed");

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

    // called by a character when they're placed on the board
    public void CharacterPlaced(GameObject character)
    {
        Debug.Log("Character placed");
        charactersPlaced++;
        placedCharacters.Add(character);
        uiManager.UpdateCharacterPlacementUI(charactersPlaced, characterTotal);
        if (charactersPlaced == characterTotal)
            gameReady = true;
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

    public void CharacterKilled()
    {
        charactersAlive--;
        uiManager.UpdateCharactersAliveUI(charactersAlive);
        if (charactersAlive < 1)
        {
            // level complete
        }
    }
}
