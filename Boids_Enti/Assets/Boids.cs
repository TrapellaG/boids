using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{

    [SerializeField]
    GameObject agentPrefab;

    [SerializeField]
    int numBoids = 10;

    Agent[] agents;

    [SerializeField]
    float agentRadius = 5.0f;

    [SerializeField]
    float separationWeight = 1.0f, cohesionWeight = 1.0f, alignmentWeight = 1.0f;

    private void Awake()
    {
        List<Agent> agentlist = new List<Agent>();

        for(int i = 0; i<numBoids; i++)
        {
            Vector3 position = Vector3.up * Random.Range(0, 10)
                + Vector3.right * Random.Range(0, 10) + Vector3.forward * Random.Range(0, 10);
            agentlist.Add(Instantiate(agentPrefab, position, Quaternion.identity).GetComponent<Agent>());
            agentlist[agentlist.Count - 1].radius = agentRadius;

        }
        agents = agentlist.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Agent a in agents)
        {
            a.velocity = Vector3.zero;
            checkForNeightBours(a);
            calculateSeparation(a);
            calculateAlignment(a);
            calculateCohesion(a);
            a.updateAgent();
            a.neightbours.Clear();     
        }
    }

    void checkForNeightBours(Agent a)
    {
        foreach (Agent b in agents)
        {
            float distance = Vector3.Distance(a.transform.position, b.transform.position);

            if (distance <= a.radius)
            {
                a.neightbours.Add(b);
            }
        }
    }

    void calculateSeparation(Agent a)
    {
        //a.addForce(Vector3.up, Agent.DEBUGforceType.SEPARATION);

        foreach(Agent b in a.neightbours)
        {
            float distance = Vector3.Distance(a.transform.position, b.transform.position);
            distance /= a.radius;
            distance = 1 - distance;

            a.separationForce = distance * (a.transform.position - b.transform.position) * separationWeight;
            a.addForce(a.separationForce);
        }
       
    }

    void calculateAlignment(Agent a)
    {
       // a.addForce(Vector3.right, Agent.DEBUGforceType.ALIGNMENT);

        Vector3 centralPosition = new Vector3();

        foreach(Agent b in a.neightbours)
        {
            centralPosition += b.transform.position;
        }

        centralPosition += a.transform.position;
        centralPosition /= a.neightbours.Count + 1;
        a.addForce((centralPosition - a.transform.position) * cohesionWeight);
    }

    void calculateCohesion(Agent a)
    {
        //a.addForce(Vector3.forward, Agent.DEBUGforceType.COHESION);

        Vector3 direccion = new Vector3();

        foreach(Agent b in a.neightbours)
        {
            direccion += b.velocity;
        }

        direccion += a.velocity;
        direccion /= a.neightbours.Count + 1;
        a.addForce(direccion);
    }
   
}
