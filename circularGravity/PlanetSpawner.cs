using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject planetPreFab;
    Transform planetTran;
    GameObject planetObj;
    GameObject planetObj2;
    Renderer ObjectRenderer;
    public Material mat;
    public Transform player;
    public float gravity = 2;
    int iter = 0;
    List<GameObject> planets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 0; i < 10; i++)
        {
            int distance = iter * 6;
            planets.Add(spawnPlanet(distance, Random.value * 4 - 2));
            iter++;
            Debug.Log(distance);
        }

    }

    // Update is called once per frame
    void Update()
    {
        List<int> toBeRemoved = new List<int>();
        for(int i = 0; i < planets.Count;i++)
        {
            if(planets[i].transform.position.x < -15f)
            {
                int distance = iter * 2;
                Object.Destroy(planets[i]);
                toBeRemoved.Add(i);
                planets.Add(spawnPlanet(distance+22, Random.value * 4 - 2));
                iter++;
                Debug.Log(distance);
            }
        }
        foreach(int i in toBeRemoved)
        {
            planets.RemoveAt(i);
        }
    }

    GameObject spawnPlanet(float offsetX, float offsetY)
    {
        GameObject tempPlanet = Instantiate(planetPreFab, new Vector3(offsetX, offsetY, 0), Quaternion.Euler(90, 0, 0));
        Transform tempTran = tempPlanet.transform;
        float radius = (float)(Random.value * 2.5f + 0.5f);
        Debug.Log(radius);
        tempPlanet.AddComponent<planetScript>();
        planetScript pscript = tempPlanet.GetComponent<planetScript>();
        pscript.player = player;
        pscript.gravity = gravity;
        ObjectRenderer = tempPlanet.GetComponent<Renderer>();
        ObjectRenderer.material = mat;
        tempTran.localScale += new Vector3(radius, 1.5f, radius);
        tempTran.parent = transform;
        return tempPlanet;

    }
}
