using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Build Defender/Collections/Buildings")]
public class BuildingCollection : ScriptableObject
{
    public List<BuildingData> data;
}