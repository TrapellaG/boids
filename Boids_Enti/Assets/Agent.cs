using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Vector3 velocity;

    public Vector3 separationForce, cohesionForce, alignmentForce;
    [SerializeField]
    public float radius = 5.0f;

    public List<Agent> neightbours;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + separationForce);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + cohesionForce);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + alignmentForce);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + velocity);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);

      
    }

    private void Awake()
    {
        neightbours = new List<Agent>();
    }
    
    public enum DEBUGforceType
    {
        SEPARATION,
        COHESION,
        ALIGNMENT
    }
    public void addForce(Vector3 f, DEBUGforceType type)
    {
        if (type == DEBUGforceType.SEPARATION)
            separationForce = f;
        else if (type == DEBUGforceType.COHESION)
            cohesionForce = f;
        else
            alignmentForce = f;

        velocity += f;
    }
    public void addForce(Vector3 f)
    {
        velocity += f;
    }
    public void updateAgent()
    {
        transform.position += velocity * Time.deltaTime;
    }
    public void checkNeightbours()
    {
        Collider[] checks = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider c in checks)
        {
            neightbours.Add(c.GetComponent<Agent>());
        }
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
