using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // variables set for each level
    public int archerNum = 0;
    public int swordsmanNum = 0;
    public int lancerNum = 0;

    public GameObject archerPrefab;
    public GameObject swordsmanPrefab;
    public GameObject lancerPrefab;

    int charactersPlaced;
    int characterTotal;
    int charactersAlive;
    List<GameObject> placedCharacters = new List<GameObject>();
    bool gameReady;
    UIManager uiManager;

    void Start()
    {
        charactersPlaced = 0;
        characterTotal = archerNum + swordsmanNum + lancerNum;
        charactersAlive = characterTotal;
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
            Instantiate(archerPrefab, new Vector3(-2.0f, i, 0.0f), Quaternion.identity);
        // set up swordsmen
        for (int i = 0; i < swordsmanNum; i++)
            Instantiate(swordsmanPrefab, new Vector3(-3.0f, i, 0.0f), Quaternion.identity);
        // set up lancers
        for (int i = 0; i < lancerNum; i++)
            Instantiate(lancerPrefab, new Vector3(-1.0f, i, 0.0f), Quaternion.identity);
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
                character.GetComponent<Character>().Attack();
            }
        }
    }

    // called by a character when they're placed on the board
    public void CharacterPlaced(GameObject character)
    {
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