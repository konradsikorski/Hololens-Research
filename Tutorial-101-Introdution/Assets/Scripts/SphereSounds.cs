using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSounds : MonoBehaviour {
    private AudioSource _audioSource;
    private AudioClip _impactClip;
    private AudioClip _rollingClip;
    private Rigidbody _body;

    bool _isRolling = false;

	// Use this for initialization
	void Start ()
    {
        _body = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _impactClip = Resources.Load<AudioClip>("Impact");
        _rollingClip = Resources.Load<AudioClip>("Rolling");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= 0.1f)
        {
            _audioSource.clip = _impactClip;
            _audioSource.Play();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(!_isRolling && _body.velocity.magnitude >= 0.01f)
        {
            _isRolling = true;
            _audioSource.clip = _rollingClip;
            _audioSource.Play();
        }
        else if(_isRolling && _body.velocity.magnitude <= 0.01f)
        {
            StoRolling();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        StoRolling();
    }

    private void StoRolling()
    {
        _isRolling = false;
        _audioSource.Stop();
    }
}
