using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using System;
[DefaultExecutionOrder(-1)]
public class DiceManager : Singelton<DiceManager>
{
    public event Action<int> OnRollComplete = _ => { };
    public event Action OnDiceCollide = () => { };

    [SerializeField] private Dice _dicePrefab;
    [SerializeField] private int _diceInitCount = 2;// for test
    [SerializeField] private float _throwForce = 3;
    [SerializeField] private float _rollForce = 3;

    private List<int> _resultList = new List<int>();
    private List<Dice> _dicePool = new List<Dice>();

    private void Start()
    {
        for (int i = 0; i < _diceInitCount; i++)
        {
            Dice dice = Instantiate(_dicePrefab, transform.position, Quaternion.identity);
            _dicePool.Add(dice);
        }
    }

    void AddResult(int result)
    {
        _resultList.Add(result);

        if (_resultList.Count == _diceInitCount)
        {
            print("Result: " + _resultList.Sum());
            OnRollComplete.Invoke(_resultList.Sum());
            _resultList.Clear();
        }
    }

    private async void Roll(List<int> diceFaces)
    {
        if (_dicePrefab == null) return;

        _dicePool.ForEach(dice => dice.gameObject.SetActive(false));
        //_throwedDice.Clear();

        if (_diceInitCount > _dicePool.Count)
        {
            int diff = _diceInitCount - _dicePool.Count;
            for (int i = 0; i < diff; i++)
            {
                Dice dice = Instantiate(_dicePrefab, transform.position, Quaternion.identity);
                _dicePool.Add(dice);
            }
        }

        for (int i = 0; i < _diceInitCount; i++)
        {
            Dice dice = _dicePool[i];
            dice.transform.position = transform.position;
            dice.transform.rotation = Quaternion.identity;
            dice.gameObject.SetActive(true);
            dice.Throw(transform.forward.normalized, _throwForce, _rollForce, diceFaces[i], (r) => { AddResult(r); });
            await Task.Delay(10);
        }
        diceFaces = null;

    }
    public void DiceRoll(int firstDiceFace, int secondDiceFace)
    {
        List<int> diceFaces = new List<int>() { firstDiceFace, secondDiceFace };
        Roll(diceFaces);
    }

    public void InvokeOnDiceCollide() => OnDiceCollide.Invoke();//Play dice sound
}
