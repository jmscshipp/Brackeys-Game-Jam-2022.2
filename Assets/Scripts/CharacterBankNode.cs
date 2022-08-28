using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBankNode : MonoBehaviour
{
    CharacterBankNode prev;
    GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        prev = null;
        character = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // called by bank to fill gaps when characters are removed
    public void Collapse()
    {
        character = null;

        if (prev.GetCharacter() != null)
        {
            AssignCharacter(prev.GetCharacter());
            prev.Collapse();
        }
    }

    public void AssignCharacter(GameObject newCharacter)
    {
        character = newCharacter;
        character.transform.position = transform.position;
    }

    public GameObject GetCharacter()
    {
        return character;
    }

    public void SetPrev(CharacterBankNode behindNode)
    {
        prev = behindNode;
    }

    public CharacterBankNode GetPrev()
    {
        return prev;
    }
}
