using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Selector playerSelector;
    bool placed;
    Vector3 startPos; // where character was instantiated off the grid
    LevelManager manager;
    CameraControl camControl;
    CharacterClass myClass;

    public float currentRotation;
    public float rotationGoal; // to be rotated to
    float rotationLerpCounter = 0f;

    public int health = 1;
    public Animator anim;

    void Start()
    {
        placed = false;
        manager = GameObject.Find("LEVELMANAGER").GetComponent<LevelManager>();
        playerSelector = GameObject.Find("SelectionControl").GetComponent<Selector>();
        camControl = Camera.main.GetComponent<CameraControl>();
        myClass = GetComponent<CharacterClass>();
        startPos = transform.position;
    }

    private void Update()
    {
        if (currentRotation != rotationGoal)
        {
            rotationLerpCounter += Time.deltaTime * 10f;
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, currentRotation), Quaternion.Euler(0, 0, rotationGoal), rotationLerpCounter);
        }
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
    public void ResetPos()
    {
        transform.position = startPos;
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

    }

    private void OnMouseExit()
    {
        if(GetComponent<BoxCollider2D>().enabled == true)
        {
            anim.Play("Unhover");
        }
        
        playerSelector.SetSelectedCharacter(null);
    }

    public void Attack()
    {
        myClass.Attack();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log(gameObject.name + "collided with " + other.gameObject.name);
        if (other.tag == "Attack")
        {
            health -= other.GetComponent<Weapon>().damage;
            other.GetComponent<Weapon>().Death();
            if (health <= 0)
            {
                camControl.TriggerHeavyScreenShake();
                manager.CharacterKilled();
                Destroy(gameObject);
            }
        }
        else if (other.tag == "Healing")
        {
            health += other.GetComponent<Weapon>().damage;
            other.GetComponent<Weapon>().Death();
        }
    }
}
