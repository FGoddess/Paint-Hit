using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Color _success—olor;
    private Color _missColor = Color.red;

    private ParticleSystem _hitParticle;
    private Collider _collider;
    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;

    private float _zOffset = 1.5f;

    private void Awake()
    {
        _hitParticle = GetComponentInChildren<ParticleSystem>();
        var main = _hitParticle.main;
        main.startColor = new Color(_success—olor.r, _success—olor.g, _success—olor.b, 255);

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
        Destroy(gameObject, _hitParticle.main.duration);

        if (!mesh.enabled)
        {
            mesh.enabled = true;
            mesh.material.color = _success—olor;
        }
        else
        {
            mesh.material.color = _missColor;
            Debug.Log("game over");
        }
    }
}
