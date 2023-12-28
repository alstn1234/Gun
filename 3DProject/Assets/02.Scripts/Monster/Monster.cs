using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public GameObject DamageText;
    public GameObject DamageTextPos;

    private int _maxHealth;
    private int _currentHealth;
    private float _speed;
    private int _gold;
    private Vector3 _vec;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private Slider _healthBarSlider;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _healthBarSlider = GetComponentInChildren<Slider>();
        _boxCollider = GetComponent<BoxCollider>();
    }


    private void FixedUpdate()
    {
        _rigidbody.velocity = _vec;
    }

    public void SetStat(int health, float speed, int gold)
    {
        _maxHealth = health;
        _currentHealth = health;
        _speed = speed;
        _gold = gold;
        _vec = Vector3.back * _speed;
    }

    public IEnumerator TakeDamage(int damage)
    {
        var damageText = Instantiate(DamageText, DamageTextPos.transform);
        damageText.transform.position += new Vector3(Random.Range(-5f, 5f), 0f, 0f);
        damageText.GetComponent<TextMeshProUGUI>().text = damage.ToString();

        _currentHealth -= damage;
        _healthBarSlider.value = Mathf.Clamp((float)_currentHealth / _maxHealth, 0f, 1f);
        if (_currentHealth <= 0)
        {
            _boxCollider.enabled = false;
            _vec = Vector3.zero;
            GameManager.instance.playerCurrentStats.gold += _gold;
            _animator.SetTrigger("Die");
            StartCoroutine(DelayedDestroy(3f));
        }
        yield return null;
    }

    IEnumerator DelayedDestroy(float timeToDestroy)
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EndZone"))
        {
            GameManager.instance.GameOver();
        }
    }
}
