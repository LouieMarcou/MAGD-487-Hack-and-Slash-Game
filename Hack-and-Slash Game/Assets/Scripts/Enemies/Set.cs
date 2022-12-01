using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Set : ScriptableObject
{
    public string Name;
	public List<WaveData> Waves;
	public float statModifier;
}
