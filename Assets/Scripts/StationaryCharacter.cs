using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryCharacter : MonoBehaviour
{
    Selector playerSelector;
    bool placed;
    Vector3 startPos; // where character was instantiated off the grid
    LevelManager manager;
    CameraControl camControl;
    CharacterClass myClass;

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
        manager.ImmovableCharacterPlaced(gameObject);
    }

    private void OnMouseOver()
    {
        anim.Play("HoveredAnim");
    }

    private void OnMouseExit()
    {
        anim.Play("Unhover");
    }

    public void Attack()
    {
        myClass.Attack();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Attack")
        {
            manager.ValidateTurn(); // something is happening this turn so lets do another right after!
            health -= other.GetComponent<Weapon>().damage;
            other.GetComponent<Weapon>().Death();
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
        }
    }
}
