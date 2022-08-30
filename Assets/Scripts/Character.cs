using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
    Selector playerSelector;
    bool placed;
    LevelManager manager;
    CameraControl camControl;
    CharacterClass myClass;

    public float currentRotation;
    public float rotationGoal; // to be rotated to
    float rotationLerpCounter = 0f;

    public int health = 1;
    public Animator anim;
    public TextMeshProUGUI textMeshProUGUI;
    public Canvas canvas;

    void Start()
    {
        placed = false;
        manager = GameObject.Find("LEVELMANAGER").GetComponent<LevelManager>();
        playerSelector = GameObject.Find("SelectionControl").GetComponent<Selector>();
        camControl = Camera.main.GetComponent<CameraControl>();
        myClass = GetComponent<CharacterClass>();
        textMeshProUGUI.text = health.ToString();
        textMeshProUGUI.color = Color.clear;
    }

    private void Update()
    {
        if (currentRotation != rotationGoal)
        {
            rotationLerpCounter += Time.deltaTime * 10f;
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, currentRotation), Quaternion.Euler(0, 0, rotationGoal), rotationLerpCounter);
        }

        canvas.gameObject.transform.rotation = Quaternion.Euler(0, 0, -gameObject.transform.rotation.z);
    }

    // called by selector when character is put on tile
    public void Placed()
    {
        anim.Play("Unhover");
        camControl.TriggerLightScreenShake();
        if (!placed)
        {
            manager.CharacterPlaced(gameObject); // passes in self so the level manager can keep a record of all placed characters
            placed = true;
        }


    }

    // called by selector when character is dropped anywhere not on the grid
    public void Reset()
    {
        anim.Play("Unhover");
        if (placed)
        {
            manager.CharacterRemoved(gameObject);
            placed = false;
        }
    }

    // called by selector when character is right clicked
    public void Rotate()
    {
        currentRotation = rotationGoal;
        rotationLerpCounter = 0f;
        rotationGoal -= 90.0f;
    }

    private void OnMouseOver()
    {
        anim.Play("HoveredAnim");
        playerSelector.SetSelectedCharacter(gameObject);
        textMeshProUGUI.color = Color.green;

    }

    private void OnMouseExit()
    {
        if(GetComponent<BoxCollider2D>().enabled == true)
        {
            anim.Play("Unhover");
        }
        
        playerSelector.SetSelectedCharacter(null);
        textMeshProUGUI.color = Color.clear;
    }

    public void Attack()
    {
        if (gameObject.GetComponent<SwordsmanClass>() != null)
        {
            anim.Play("SwordsmanSwing");
        }
        myClass.Attack();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Attack")
        {
            manager.ValidateTurn(); // something is happening this turn so lets do another right after!
            health -= other.GetComponent<Weapon>().damage;
            other.GetComponent<Weapon>().Death();
            textMeshProUGUI.text = health.ToString();
            if (health <= 0)
            {
                camControl.TriggerHeavyScreenShake();
                manager.CharacterKilled(gameObject);
                Destroy(gameObject);
            }
        }
        else if (other.tag == "Healing")
        {
            health += other.GetComponent<Weapon>().damage;
            other.GetComponent<Weapon>().Death();
            textMeshProUGUI.text = health.ToString();
        }
    }
}
