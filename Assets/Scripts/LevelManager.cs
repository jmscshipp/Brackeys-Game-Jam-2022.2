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
    bool gameReady;
    UIManager uiManager;

    void Start()
    {
        charactersPlaced = 0;
        characterTotal = archerNum + swordsmanNum + lancerNum;
        gameReady = false;
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        uiManager.UpdateCharacterPlacementUI(0, characterTotal);
        SetUpCharacters();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameReady)
        {
            BeginTurns();
        }
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

    void BeginTurns()
    {
        gameReady = false;
    }

    // called by a character when they're placed on the board
    public void CharacterPlaced()
    {
        charactersPlaced++;
        uiManager.UpdateCharacterPlacementUI(charactersPlaced, characterTotal);
        if (charactersPlaced == characterTotal)
            gameReady = true;
    }

    // called by a character when they're removed from the board
    public void CharacterRemoved()
    {
        charactersPlaced--;
        uiManager.UpdateCharacterPlacementUI(charactersPlaced, characterTotal);
        if (charactersPlaced < characterTotal)
            gameReady = false;
    }
}
