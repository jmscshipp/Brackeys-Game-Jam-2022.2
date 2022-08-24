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
}
