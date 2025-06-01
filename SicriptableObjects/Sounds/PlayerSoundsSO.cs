using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSounds", menuName = "ScriptableObjects/Sounds/PlayerSoundsSO")]
public class PlayerSoundsSO : ScriptableObject
{
    public AudioClip jumpClip;
    public AudioClip landClip;

    public AudioClip[] runClips;

    public AudioClip dashClip;
    public AudioClip hitClip;
    public AudioClip bowHitClip;
}
