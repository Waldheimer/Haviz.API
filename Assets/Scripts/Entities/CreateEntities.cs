using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEntities : MonoBehaviour
{

    [SerializeField]
    public int count;
    [SerializeField]
    float radius;
    List<Vector3> entityPositions;
    // Start is called before the first frame update
    void Start()
    {
        GameObject entities = GameObject.Find("Entities");
        entityPositions = calculateStartPositions();
        foreach (var item in entityPositions)
        {
            GameObject caps = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            caps.transform.parent = entities.transform;
            caps.transform.position = item;
            caps.transform.localScale = new Vector3(.5f, .25f, .5f);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<Vector3> calculateStartPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0;i< count; i++)
        {
            float rad = i * (2*Mathf.PI/count);
            positions.Add(new Vector3(Mathf.Sin(rad)*radius,1,Mathf.Cos(rad)*radius));
        }

        return positions;
    }
}
