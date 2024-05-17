using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour
{
    [SerializeField] private Transform[] _diceSides;
    Action<int> _result;
    private Rigidbody _rigidbody;
    private bool _delayedCheck;
    private bool _hasRotationStopped;

    private void OnEnable()
    {
        _delayedCheck = true;
        _rigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (_delayedCheck) return;

        if (!_hasRotationStopped && _rigidbody.velocity.sqrMagnitude <= .1f && _rigidbody.angularVelocity.sqrMagnitude <= .1f) 
        {
            _hasRotationStopped = true;
            GetRollResult();
        }
    }

    [ContextMenu("Top Side")]
    private void GetRollResult()
    {
        if (_diceSides.Length == 0) return;

        var currentSideIndex = 0;
        float currentY = _diceSides[0].position.y;

        for (int i = 0; i < _diceSides.Length; i++)
        {
            if (currentY < _diceSides[i].position.y)
            {
                currentY = _diceSides[i].position.y;
                currentSideIndex = i;
            }
        }
        _result.Invoke(currentSideIndex + 1);

    }

    public void Throw(float throwForce, float rollForce, Action<int> onRollStopped)
    {
        _result = onRollStopped;
        float randomValue = UnityEngine.Random.Range(-1f, 1f);
        _rigidbody.AddForce(transform.forward * (throwForce + randomValue), ForceMode.Impulse);

        float randomX = UnityEngine.Random.Range(0, 1f);
        float randomY = UnityEngine.Random.Range(0, 1f);
        float randomZ = UnityEngine.Random.Range(0, 1f);

        DelayedTorque(new Vector3(randomX, randomY, randomZ) * (rollForce + randomValue));
        Delay();
    }

    private async void DelayedTorque(Vector3 torque)
    {
        await Task.Delay(100);
        _rigidbody.AddTorque(torque, ForceMode.Impulse);
    }

    private async void Delay()
    {

        await Task.Delay(1000);
        _delayedCheck = false;
    }

}
