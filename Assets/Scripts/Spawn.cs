using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Shape[] allShapes;
    public Transform[] queueObject = new Transform[3];
    Shape[] queuedShapes = new Shape[3];
    public float queueScale = 0.5f;
 
    // Start is called before the first frame update
    void Awake()
    {
        InitQueue();
    }
    public Shape SpawnShape()
    {
        Shape shape = null;
        shape = GetShapeFromQueue();
        shape.transform.position = transform.position;
        shape.transform.localScale = Vector3.one;
        return shape;

    }
    
    public Shape GetRandomShape()
    {
        Shape shape = null;
        int rnd_shp = Random.Range(0, allShapes.Length);
        shape = Instantiate(allShapes[rnd_shp], transform.position, Quaternion.identity) as Shape;
        return shape;
    }
    
    void InitQueue()
    {
        for (int i = 0; i < queuedShapes.Length; i++)
        {
            queuedShapes[i] = null;
        }
        FillQueue();
    }

    void FillQueue()
    {
        for(int i = 0; i < queuedShapes.Length; i++)
        {
            if(!queuedShapes[i])
            {
                queuedShapes[i] = GetRandomShape();
                queuedShapes[i].transform.position = queueObject[i].position;
                queuedShapes[i].transform.localScale = new Vector3(queueScale,queueScale,queueScale);
            }
            
        }
    }

    public Shape GetShapeFromQueue()
    {
        Shape currentShape = null;
        if(queuedShapes[0])
        {
            currentShape = queuedShapes[0];
        }

        for(int i = 1; i < queuedShapes.Length; i++)
        {
            queuedShapes[i-1] = queuedShapes[i];   // Shift shapes to queue head
            queuedShapes[i-1].transform.position = queueObject[i-1].position;
        }

        queuedShapes[2] = null;

        FillQueue();

        return currentShape;
    }
    
    void Update()
    {
        
    }
}
