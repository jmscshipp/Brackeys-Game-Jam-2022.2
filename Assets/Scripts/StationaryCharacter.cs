using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public TextMeshProUGUI textMeshProUGUI;
    public Canvas canvas;

    void Start()
    {
        placed = false;
        manager = GameObject.Find("LEVELMANAGER").GetComponent<LevelManager>();
        playerSelector = GameObject.Find("SelectionControl").GetComponent<Selector>();
        camControl = Camera.main.GetComponent<CameraControl>();
        myClass = GetComponent<CharacterClass>();
        startPos = transform.position;
        manager.ImmovableCharacterPlaced(gameObject);
        textMeshProUGUI.text = health.ToString();
        textMeshProUGUI.color = Color.clear;
    }

    private void OnMouseOver()
    {
        anim.Play("HoveredAnim");
        textMeshProUGUI.color = Color.green;
    }

    private void OnMouseExit()
    {
        anim.Play("Unhover");
        textMeshProUGUI.color = Color.clear;
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

    private void Update() 
    {
        canvas.gameObject.transform.rotation = Quaternion.Euler(0, 0, -gameObject.transform.rotation.z);
    }
}
