using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Color _successColor;
    private Color _missColor;

    private ParticleSystem _hitParticle;
    private Collider _collider;
    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;

    private float _zOffset = 1.5f;

    public void Initialize(Color successColor, Color missColor)
    {
        _successColor = successColor;
        _missColor = missColor;

        _hitParticle = GetComponentInChildren<ParticleSystem>();
        var main = _hitParticle.main;
        main.startColor = new Color(_successColor.r, _successColor.g, _successColor.b, 255);

        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rigidbody.velocity = Vector3.zero;
        transform.position = new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, collision.contacts[0].point.z - _zOffset);
        var mesh = collision.gameObject.GetComponent<MeshRenderer>();

        _collider.enabled = false;
        _meshRenderer.enabled = false;

        _hitParticle.gameObject.transform.SetParent(collision.transform);
        _hitParticle.Play();

        if (!mesh.enabled)
        {
            mesh.enabled = true;
            mesh.material.color = _successColor;
        }
        else
        {
            mesh.material.color = _missColor;
            Debug.Log("game over");
        }

        StartCoroutine(DelayDeactivation());
    }

    private IEnumerator DelayDeactivation()
    {
        yield return new WaitForSeconds(_hitParticle.main.duration);

        _hitParticle.gameObject.transform.SetParent(transform);
        _hitParticle.transform.localPosition = Vector3.zero;

        gameObject.SetActive(false);
        _collider.enabled = true;
        _meshRenderer.enabled = true;
    }
}
