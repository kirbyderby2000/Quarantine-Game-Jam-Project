using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/AudioData", fileName = "AudioData")]
public class AudioData : ScriptableObject
{
[Header("Environment")]

    [FMODUnity.EventRef]
    public string Ambience = "event:/Environment/Ambience";

    [Header("Player")]
    [FMODUnity.EventRef]
	public string Jump = "event:/Player/Jump";

	[FMODUnity.EventRef]
	public string Footsteps = "event:/Player/Footsteps";

	[FMODUnity.EventRef]
	public string Land = "event:/Player/Land";

	[FMODUnity.EventRef]
	public string Shoot = "event:/Player/Shoot";

    [FMODUnity.EventRef]
    public string Wind = "event:/Player/Wind";


}