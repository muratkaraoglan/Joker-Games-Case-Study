using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour
{
    #region Event
    private Action<int> _result;
    #endregion

    #region Serialized
    [SerializeField] private Transform[] _diceSides;
    #endregion

    #region Private
    private Rigidbody _rigidbody;
    private bool _delayedCheck;
    private bool _hasRotationStopped;
    private bool _isRiggedDice;
    private bool _isAudioClipPlayed;
    private int _desiredFace = -1;
    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        _delayedCheck = true;
        _hasRotationStopped = false;
        _isAudioClipPlayed = false;
    }

    private void Update()
    {
        if (_isRiggedDice) return;
        if (_delayedCheck) return;

        if (!_hasRotationStopped && _rigidbody.velocity.sqrMagnitude <= .1f && _rigidbody.angularVelocity.sqrMagnitude <= .001f)
        {
            _hasRotationStopped = true;
            GetRollResult();
        }
    }

    private void GetRollResult()
    {
        if (_diceSides.Length == 0) return;

        if (_desiredFace != -1)
        {
            _result.Invoke(_desiredFace);
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            return;
        }

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
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    public void Throw(Vector3 forceDirection, float throwForce, float rollForce, int desiredDiceFace, Action<int> onRollStopped)
    {
        _isRiggedDice = desiredDiceFace != -1;
        _desiredFace = desiredDiceFace;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _result = onRollStopped;

        float randomValue = UnityEngine.Random.Range(-1f, 1f);
        _rigidbody.AddForce(forceDirection * (throwForce + randomValue), ForceMode.Impulse);

        float randomX = UnityEngine.Random.Range(0, 1f);
        float randomY = UnityEngine.Random.Range(0, 1f);
        float randomZ = UnityEngine.Random.Range(0, 1f);

        DelayedTorque(new Vector3(randomX, randomY, randomZ) * (rollForce + randomValue));
        Delay();
    }

    public Quaternion DesiredFaceToRotation(int desiredFace)
    {
        return Quaternion.FromToRotation((_diceSides[desiredFace - 1].position - transform.position).normalized, Vector3.up) * transform.rotation;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.TryGetComponent(out Dice dice))
        {
            if (_isRiggedDice)
            {
                StartCoroutine(FromToRotation(DesiredFaceToRotation(_desiredFace)));
                _isRiggedDice = false;
            }
            if (!_isAudioClipPlayed)
            {
                _isAudioClipPlayed = true;
                DiceManager.Instance.InvokeOnDiceCollide();
            }
        }
    }

    IEnumerator FromToRotation(Quaternion targetRotation)
    {
        yield return new WaitForSeconds(1f);

        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _rigidbody.angularVelocity = Vector3.zero;

        float rotationSpeed = 1f;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime * 360);
            yield return null;
        }
    }
}
