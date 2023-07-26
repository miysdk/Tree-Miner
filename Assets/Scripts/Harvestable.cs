using ScoredProductions.DTM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    public int capacity = 3;
    public float respawnTime = 5;
    public Transform visual;
    public GameObject destructable;
    public GameObject prefab;
    public GameObject drop;

    BoxCollider bc;
    float sizeDecrement;
    int currentCapacity;

    void Start()
    {
        bc = GetComponent<BoxCollider>();
        sizeDecrement = 1 / ((float)capacity + 1);
        currentCapacity = capacity;
    }

    void Damage()
    {
        if (currentCapacity == 0) return;

        for (int i = 0; i < destructable.transform.childCount; i++)
        {
            BlockDestructionChild temp = destructable.transform.GetChild(i).GetComponent<BlockDestructionChild>();
            if (temp is null) continue;
            temp.ReceiveDamage(1);
        }

        if(drop is not null)
            Instantiate(drop, transform.position + new Vector3(0, 5, 0), new Quaternion());

        currentCapacity--;

        if(currentCapacity == 0)
        {
            StartCoroutine(Grow());
        }
    }

    IEnumerator Grow()
    {
        bc.enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(destructable);
        while (visual.transform.localScale.x < 1)
        {
            float size = visual.localScale.x + 1 / respawnTime;
            size = Mathf.Clamp(size, 0, 1);
            visual.transform.localScale = new Vector3(size, size, size);
            yield return new WaitForSeconds(1);
        }
        destructable = Instantiate(prefab, transform);
        visual.transform.localScale = Vector3.zero;
        currentCapacity = capacity;
        bc.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damage"))
        {
            Damage();
        }
    }
}
