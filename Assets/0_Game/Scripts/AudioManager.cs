using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClipHolder> _audioClips;

    private PlayerBase _player;
    private void Start()
    {
        _audioSource.playOnAwake = false;
        DiceManager.Instance.OnDiceCollide += OnDiceCollide;
        UIManager.Instance.OnPlayerSelectedEvent += OnPlayerSelectedEvent;
    }

    private void OnPlayerSelectedEvent(PlayerBase obj)
    {
        _player = obj;
        _player.OnStepCompleteEvent += Player_OnStepComplete;
        _player.OnMoveCompleteEvent += Player_OnMoveCompleteEvent;
    }

    private void Player_OnMoveCompleteEvent(bool hasPrize)
    {
        if (hasPrize) _audioSource.PlayOneShot(GetClip(SoundType.Prize));
    }

    private void Player_OnStepComplete()
    {
        _audioSource.PlayOneShot(GetClip(SoundType.Step));
    }

    private void OnDiceCollide()
    {
        _audioSource.PlayOneShot(GetClip(SoundType.DiceColiide));
    }

    private AudioClip GetClip(SoundType soundType) => _audioClips.Where(a => a.Type == soundType).FirstOrDefault().Clip;

    private void OnDisable()
    {
        DiceManager.Instance.OnDiceCollide -= OnDiceCollide;
        UIManager.Instance.OnPlayerSelectedEvent -= OnPlayerSelectedEvent;
        _player.OnMoveCompleteEvent -= Player_OnMoveCompleteEvent;
        _player.OnStepCompleteEvent -= Player_OnStepComplete;
    }

}

[System.Serializable]
struct AudioClipHolder
{
    public SoundType Type;
    public AudioClip Clip;
}

public enum SoundType
{
    DiceColiide,
    Step,
    Prize
}
