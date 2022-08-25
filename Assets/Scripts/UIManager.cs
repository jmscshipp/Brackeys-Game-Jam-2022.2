using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI characterCountText;
    public Button playButton;
    
    void Start()
    {
        playButton.gameObject.SetActive(false);
    }

    public void UpdateCharacterPlacementUI(int currentCharacterNum, int totalCharacterNum)
    {
        if (currentCharacterNum == totalCharacterNum)
        {
            characterCountText.text = "All characters placed!";
            playButton.gameObject.SetActive(true);
        }
        else
        {
            characterCountText.text = "Place Characters: " + currentCharacterNum + " / " + totalCharacterNum;
            playButton.gameObject.SetActive(false);
        }
    }

    public void UpdateCharactersAliveUI(int charactersAlive)
    {
        if (charactersAlive == 0)
        {
            characterCountText.text = "All characters were destroyed!";
            playButton.gameObject.SetActive(false);
        }
        else
        {
            characterCountText.text = "Characters remaining: " + charactersAlive;
            playButton.gameObject.SetActive(false);
        }
    }
}
