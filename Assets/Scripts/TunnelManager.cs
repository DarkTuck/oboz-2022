using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelManager : MonoBehaviour
{
    [SerializeField] Transform[] tunnelPrefab;
    [SerializeField] int partIndex = 0; // currently change in runtime has no effect
    [SerializeField] int partsCount = 10;
    [SerializeField] int biomeSize = 20;
    int biomeChangeCounter = 0;
    public float offset = 18F; // distance between parts
    [SerializeField] float speed = 1F;
    [SerializeField] float startZ = 0;
    [SerializeField] float cutoffZ = -24F; // when part is gone from screen
    List<Transform> activeParts = new List<Transform>();
    int latestPartIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Destroy template
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        // Spawn initial tunnel parts
        for (int i = 0; i < partsCount; i++)
        {
            Transform newObject= Instantiate(tunnelPrefab[partIndex], new Vector3(0, 0, startZ + i * offset), Quaternion.identity);
            newObject.parent = transform;
            activeParts.Add(newObject);
        }

        latestPartIndex = partsCount-1;
        
        StartCoroutine(ReuseParts());
        StartCoroutine(Every1Meter());
    }

    // Update is called once per frame
    void Update()
    {   
        transform.position -= new Vector3(0, 0, offset * speed * Time.deltaTime);
    }

    IEnumerator Every1Meter()
    {
        while (true)
        {
            GM.instance.Moved1Meter();
            yield return new WaitForSeconds(1 /  offset * speed);
        }
    }
    
    IEnumerator ReuseParts()
    {
        while (true)
        {
            for (int i = 0; i < activeParts.Count; i++)
            {
                if (activeParts[i].position.z < cutoffZ)
                {
                    // Update active parts for chosen level
                    for (int j = 0; j < activeParts[i].childCount; j++)
                    {
                        // Update mesh filter & mesh renderer material
                        activeParts[i].GetChild(j).GetComponent<MeshFilter>().sharedMesh = tunnelPrefab[partIndex].GetChild(j).GetComponent<MeshFilter>().sharedMesh;   
                        activeParts[i].GetChild(j).GetComponent<MeshRenderer>().sharedMaterials = tunnelPrefab[partIndex].GetChild(j).GetComponent<MeshRenderer>().sharedMaterials;
                        
                    }

                    GM.instance.AsteroidEscaped();

                    activeParts[i].position = activeParts[latestPartIndex].position + new Vector3(0, 0, offset);
                    latestPartIndex = i;

                    // Handle biome changes
                    biomeChangeCounter++;
                    if (biomeChangeCounter >= biomeSize)
                    {
                        biomeChangeCounter = 0;
                        partIndex++;
                        if (partIndex >= tunnelPrefab.Length){
                            partIndex = 0;
                        }
                    }
                }
            }

            yield return new WaitForSeconds(1F / speed);
        }
        
    }
}
