using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GenerateNavMesh : MonoBehaviour
{
    public NavMeshSurface NavMeshSurface;
    // Start is called before the first frame update
    void Start()
    {
        NavMeshSurface.BuildNavMesh();
    }

}
