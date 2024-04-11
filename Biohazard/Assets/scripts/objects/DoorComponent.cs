using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorComponent : MonoBehaviour
{
    public int doorLevel = 2;
    public int wideness = 1;

    bool open = false;

    public GameObject[] doors;

    public void HitDoor()
    {
        if (open)
            return;

        doors[0].transform.position += Vector3.up * wideness;
        doors[1].transform.position += Vector3.down * wideness;

        gameObject.GetComponent<CompositeCollider2D>().isTrigger = true;

        open = true;
    }
}
