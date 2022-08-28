using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBank : MonoBehaviour
{
    public CharacterBankNode[] nodes;
    
    void Start()
    {
        // set up node refs
        for (int i = 0; i < nodes.Length - 1; i++)
            nodes[i].SetPrev(nodes[i + 1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToBank(GameObject character)
    {
        // find first empty node
        CharacterBankNode node = nodes[0];
        while (node.GetCharacter() != null)
            node = node.GetPrev();

        node.AssignCharacter(character);
    }

    public void RemoveFromBank(GameObject character)
    {
        // find node with character being removed
        CharacterBankNode node = nodes[0];
        while (node.GetCharacter() != character)
            node = node.GetPrev();

        node.Collapse();
    }
}
