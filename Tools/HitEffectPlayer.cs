using System;
using System.Collections;
using UnityEngine;

public class HitEffectPlayer : Singleton<HitEffectPlayer>
{
    [SerializeField] public PlayerSoundsSO playerSounds;
    private AudioSource audioSource;
    private AudioClip hitSoundEffect;

    private EventBinding<PlayerAudioEvent> playerAudioEvent;

   
    protected override void Init()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;

        playerAudioEvent = new EventBinding<PlayerAudioEvent>(OnPlayerAudioEvent);
        EventBus<PlayerAudioEvent>.Subscribe(playerAudioEvent);

    }

    private void OnPlayerAudioEvent(PlayerAudioEvent e)
    {
        if (e.Clip != null)
        {
            audioSource.PlayOneShot(e.Clip);
            return;
        }
        PlayerSoundType soundType = e.SoundType;
        
        switch (soundType)
        {
            case PlayerSoundType.Jump:
                audioSource.PlayOneShot(playerSounds.jumpClip);
                break;
            case PlayerSoundType.Land:
                audioSource.PlayOneShot(playerSounds.landClip);
                break;
            case PlayerSoundType.Run:
                audioSource.PlayOneShot(playerSounds.runClips[UnityEngine.Random.Range(0, playerSounds.runClips.Length)]);
                break;
            case PlayerSoundType.Dash:
                audioSource.PlayOneShot(playerSounds.dashClip);
                break;
            case PlayerSoundType.Hit:
                audioSource.PlayOneShot(playerSounds.hitClip);
                break;
            case PlayerSoundType.BowHit:
                audioSource.PlayOneShot(playerSounds.bowHitClip);
                break;
            case PlayerSoundType.Hurt:
                Debug.Log("Hurt sound not implemented");
                break;
            case PlayerSoundType.Death:
                Debug.Log("Death sound not implemented");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void PlayHitEffect(Vector2 position, GameObject hitPrefab, AudioClip hitSound,float localScale = 1)
    {
        GameObject spawnedEffect = localScale == 1 ? Instantiate(hitPrefab, new Vector3(position.x - .3f, position.y), 
        Quaternion.identity) : Instantiate(hitPrefab, new Vector3(position.x + .3f, position.y), Quaternion.identity);
        spawnedEffect.transform.localScale = new Vector3(-localScale, 1, 1);
        StartCoroutine(EffectTimer(spawnedEffect));
        hitSoundEffect = hitSound;
        audioSource.PlayOneShot(hitSoundEffect);
    }
    private IEnumerator EffectTimer(GameObject effectObject)
    {
        yield return new WaitForSeconds(0.35f);
        Destroy(effectObject);
    }

}
