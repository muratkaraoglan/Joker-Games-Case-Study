using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using System;

public class DiceManager : Singelton<DiceManager>
{
    public event Action<int> OnRollComplete = _ => { };

    [SerializeField] private Dice _dicePrefab;
    [SerializeField] private int _diceInitCount = 2;// for test
    [SerializeField] private float _throwForce = 3;
    [SerializeField] private float _rollForce = 3;

    private List<int> _resultList = new List<int>();
    private List<GameObject> _throwedDice = new List<GameObject>();
    // for test
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Roll();
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

    private async void Roll()
    {
        if (_dicePrefab == null) return;

        _throwedDice.ForEach(dice => Destroy(dice));
        _throwedDice.Clear();

        for (int i = 0; i < _diceInitCount; i++)
        {
            Dice dice = Instantiate(_dicePrefab, transform.position, Quaternion.identity);
            dice.Throw(transform.forward.normalized, _throwForce, _rollForce, (r) => { AddResult(r); });
            _throwedDice.Add(dice.gameObject);

            await Task.Yield();
            await Task.Yield();
            await Task.Yield();
            await Task.Yield();
            await Task.Yield();
        }

    }
}
