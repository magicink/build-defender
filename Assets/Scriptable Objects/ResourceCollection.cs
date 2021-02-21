using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Build Defender/Collections/Resource Types")]
public class ResourceCollection : ScriptableObject
{
    public List<ResourceData> data;
}