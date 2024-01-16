using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ClosestLamp : MonoBehaviour
{
    public static readonly HashSet<ClosestLamp> Entities = new HashSet<ClosestLamp>();

    void Awake()
    {
        Entities.Add(this);
    }

    void OnDestroy()
    {
        Entities.Remove(this);
    }
}
