using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanClass : CharacterClass
{
    public GameObject slicePrefab;
    public GameObject slicePoint;

    public override void Attack()
    {
        GameObject siceInstance;
        siceInstance = Instantiate(slicePrefab, slicePoint.transform.position, transform.rotation);
        siceInstance.GetComponent<Slice>().Death();
    }
}
