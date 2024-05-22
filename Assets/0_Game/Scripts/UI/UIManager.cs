using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UIManager : Singelton<UIManager>
{
    public event Action<PlayerBase> OnPlayerSelectedEvent = _ => { };

    [SerializeField] private GameObject _throwButtonGameObject;
    [Header("Player Selection")]
    [SerializeField] private GameObject _playerSelectorGameObject;
    [SerializeField] private Transform _playerParentTransform;
    [Header("Dice Input")]
    [SerializeField] private GameObject _inputHolderGameObject;
    [SerializeField] private TextMeshProUGUI _firstDiceValueText;
    [SerializeField] private TextMeshProUGUI _secondDiceValueText;

    private PlayerBase _player;
    private int _playerIndex;
    private void OnEnable()
    {
        TileSpawner.Instance.OnTileOrderFinished += OnTileOrderFinished;
        DiceManager.Instance.OnRollComplete += OnRollComplete;
    }

    private void OnDisable()
    {
        TileSpawner.Instance.OnTileOrderFinished -= OnTileOrderFinished;
        DiceManager.Instance.OnRollComplete -= OnRollComplete;
        _player.OnMoveCompleteEvent -= OnMoveCompleteEvent;
    }

    private void OnTileOrderFinished()
    {
        _playerSelectorGameObject.SetActive(true);
        _playerParentTransform.gameObject.SetActive(true);

    }
    private void OnRollComplete(int moveCount)
    {
        print("UI roll Complete show roll count");
    }

    private void OnMoveCompleteEvent(bool hasPrize)
    {
        _throwButtonGameObject.SetActive(true);
    }

    public void OnRollButtonClicked()
    {
        _throwButtonGameObject.SetActive(false);
        int first, second;

        first = _firstDiceValueText.text[0] - '0';
        second = _secondDiceValueText.text[0] - '0';
        first = first > 6 ? -1 : first;
        second = second > 6 ? -1 : second;

        DiceManager.Instance.DiceRoll(first, second);
    }

    public void OnPlayerSelected()
    {
        _playerSelectorGameObject.SetActive(false);
        _throwButtonGameObject.SetActive(true);
        _player = _playerParentTransform.GetChild(_playerIndex).GetComponent<PlayerBase>();
        _player.OnMoveCompleteEvent += OnMoveCompleteEvent;
        _playerParentTransform.GetChild(_playerIndex).SetParent(null);
        _inputHolderGameObject.SetActive(true);
        Destroy(_playerParentTransform.gameObject);
        OnPlayerSelectedEvent.Invoke(_player);
    }

    public void PlayerSelectDirectionButtonClicked(int direction)
    {
        _playerParentTransform.GetChild(_playerIndex).gameObject.SetActive(false);
        _playerIndex += direction;
        if (_playerIndex < 0) _playerIndex = _playerParentTransform.childCount - 1;
        else if (_playerIndex >= _playerParentTransform.childCount) _playerIndex = 0;

        _playerParentTransform.GetChild(_playerIndex).gameObject.SetActive(true);
    }
}
