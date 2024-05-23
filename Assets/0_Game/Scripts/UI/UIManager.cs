using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class UIManager : Singelton<UIManager>
{
    #region Event
    public event Action<PlayerBase> OnPlayerSelectedEvent = _ => { };
    public event Action<int> OnDiceCountChanged = _ => { };
    #endregion

    #region Serialized
    [Header("Dice Settings")]
    [SerializeField] private TMP_Dropdown _diceCountDropDown;
    [SerializeField] private int _maxDiceCount = 50;
    [SerializeField] private DiceInputValueManager _diceInputValueManager;
    [SerializeField] private GameObject _inputHolderGameObject;
    [SerializeField] private GameObject _diceCountDropDownParentGameObject;

    [Header("Throw Button")]
    [SerializeField] private GameObject _throwButtonGameObject;

    [Header("Player Selection")]
    [SerializeField] private GameObject _playerSelectorGameObject;
    [SerializeField] private Transform _playerParentTransform;

    [Header("Roll Result")]
    [SerializeField] private Animator _rollResultAnimator;
    [SerializeField] private TextMeshProUGUI _rollResultText;
    #endregion

    #region Private
    private PlayerBase _player;
    private int _playerIndex;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        _diceCountDropDown.options.Clear();
        List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < _maxDiceCount; i++)
        {
            TMPro.TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = (i + 1).ToString();
            optionDataList.Add(option);
        }
        _diceCountDropDown.AddOptions(optionDataList);
        _diceCountDropDown.onValueChanged.AddListener(v => OnDropDownValueChanged(v));
        _diceCountDropDown.value = 0;
        _diceInputValueManager.Init(_maxDiceCount);
    }

    private void OnEnable()
    {
        TileSpawner.Instance.OnTileOrderFinished += OnTileOrderFinished;
    }

    private void OnDisable()
    {
        TileSpawner.Instance.OnTileOrderFinished -= OnTileOrderFinished;
        DiceManager.Instance.OnRollComplete -= OnRollComplete;
        _player.OnMoveCompleteEvent -= OnMoveCompleteEvent;
    }

    private void OnDropDownValueChanged(int index)
    {
        OnDiceCountChanged.Invoke(index + 1);
    }

    private void OnTileOrderFinished()
    {
        _playerSelectorGameObject.SetActive(true);
        _playerParentTransform.gameObject.SetActive(true);

    }
    private void OnRollComplete(int moveCount, bool isDouble)
    {
        _rollResultText.text = moveCount.ToString() + (isDouble ? "\nx2 Prize" : "");
        _rollResultAnimator.Play("FloatingText", -1, 0f);
    }

    private void OnMoveCompleteEvent(bool hasPrize)
    {
        _throwButtonGameObject.SetActive(true);
    }

    public void OnRollButtonClicked()
    {
        _throwButtonGameObject.SetActive(false);
        DiceManager.Instance.DiceRoll(_diceInputValueManager.GetActiveRiggedDiceValues());
    }

    public void OnPlayerSelected()
    {
        _throwButtonGameObject.SetActive(true);
        _playerSelectorGameObject.SetActive(false);

        _player = _playerParentTransform.GetChild(_playerIndex).GetComponent<PlayerBase>();
        _player.OnMoveCompleteEvent += OnMoveCompleteEvent;
        _playerParentTransform.GetChild(_playerIndex).SetParent(null);

        _inputHolderGameObject.SetActive(true);
        _diceCountDropDownParentGameObject.SetActive(true);

        Destroy(_playerParentTransform.gameObject);
        OnPlayerSelectedEvent.Invoke(_player);

        DiceManager.Instance.OnRollComplete += OnRollComplete;

        _diceCountDropDown.onValueChanged.Invoke(_diceCountDropDown.value);
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
