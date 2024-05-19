using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UIManager : Singelton<UIManager>
{
    [SerializeField] private GameObject _throwButtonGameObject;
    [SerializeField] private GameObject _playerSelectorGameObject;
    [SerializeField] private Transform _playerParentTransform;
    private PlayerBase _player;
    private int _playerIndex;
    private void OnEnable()
    {
        TileSpawner.Instance.OnTileOrderFinished += OnTileOrderFinished;
        DiceManager.Instance.OnRollComplete += OnRollComplete;
        //_player = FindObjectOfType<PlayerBase>();
        //_player.OnMoveCompleteEvent += OnMoveCompleteEvent;
    }

    private void OnDisable()
    {
        TileSpawner.Instance.OnTileOrderFinished -= OnTileOrderFinished;
        DiceManager.Instance.OnRollComplete -= OnRollComplete;
        //_player.OnMoveCompleteEvent -= OnMoveCompleteEvent;
    }

    private void OnTileOrderFinished()
    {
        _playerSelectorGameObject.SetActive(true);
        _playerParentTransform.gameObject.SetActive(true);
        //open inventory
    }
    private void OnRollComplete(int moveCount)
    {
        print("UI roll Complete show roll count");
        //Close Inventory
    }

    private void OnMoveCompleteEvent()
    {
        _throwButtonGameObject.SetActive(true);
        //Open Inventory
    }

    public void OnRollButtonClicked()
    {
        _throwButtonGameObject.SetActive(false);
        DiceManager.Instance.DiceRoll();
    }

    public void OnPlayerSelected()
    {
        _playerSelectorGameObject.SetActive(false);
        _throwButtonGameObject.SetActive(true);
        _player = _playerParentTransform.GetChild(_playerIndex).GetComponent<PlayerBase>();
        _player.OnMoveCompleteEvent += OnMoveCompleteEvent;
        _playerParentTransform.GetChild(_playerIndex).SetParent(null);
        Destroy(_playerParentTransform.gameObject);
    }

    public void PlayerSelectDirectionButtonClicked(int direction)
    {
        _playerParentTransform.GetChild(_playerIndex).gameObject.SetActive(false);
        _playerIndex += direction;
        if(_playerIndex < 0 )  _playerIndex = _playerParentTransform.childCount - 1; 
        else if(_playerIndex>=_playerParentTransform.childCount) _playerIndex = 0;

        _playerParentTransform.GetChild(_playerIndex).gameObject.SetActive(true);
    }
}
