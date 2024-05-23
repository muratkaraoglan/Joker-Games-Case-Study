using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiceInputValueManager : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Transform _contentTransform;
    [SerializeField] private DiceInputValue _diceInputValuePrefab;

    private List<DiceInputValue> _diceInputValues = new List<DiceInputValue>();
    private List<DiceInputValue> _activeDiceInputs = new List<DiceInputValue>();
    public void Init(int count)
    {
        for (int i = 0; i < count; i++)
        {
            DiceInputValue diceInputValue = Instantiate(_diceInputValuePrefab, _contentTransform);
            diceInputValue.scrollRect = _scrollRect;
            diceInputValue._placeHolderText.text = (i + 1) + ". Dice Value";
            _diceInputValues.Add(diceInputValue);
        }
        UIManager.Instance.OnDiceCountChanged += OnDiceCountChanged;
    }

    private void OnDiceCountChanged(int diceCount)
    {
        _activeDiceInputs.ForEach(d => d.gameObject.SetActive(false));
        _activeDiceInputs.Clear();
        for (int i = 0; i < diceCount; i++)
        {
            DiceInputValue diceInput = _diceInputValues[i];
            _activeDiceInputs.Add(diceInput);
            diceInput.gameObject.SetActive(true);
        }
    }

    public List<int> GetActiveRiggedDiceValues()
    {
        List<int> values = new List<int>();
        for (int i = 0; i < _activeDiceInputs.Count; i++)
        {
            values.Add(_activeDiceInputs[i].InputValue);
        }
        return values;
    }
}


