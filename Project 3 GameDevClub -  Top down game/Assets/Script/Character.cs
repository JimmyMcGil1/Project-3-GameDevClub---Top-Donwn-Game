using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Character : MonoBehaviour {
    [Header("Visualization")]
    [SerializeField] protected Animation _anim;
    [SerializeField] protected AudioSource _attackAudio;
    [SerializeField] protected AudioSource _skill1Audio;
    [SerializeField] protected AudioSource _skill2Audio;

    [Header("Info")]
    [SerializeField] protected float _minMoveSpeed;
    [SerializeField] protected float _moveSpeedScale;
    [SerializeField] protected float _maxWeight;
    [SerializeField] protected float _maxMana;
    [SerializeField] protected float _dashForce;
    [SerializeField] protected float _minDamageWeight;
    [SerializeField] protected float _maxDamggeWeight;

    [Header("Cost Info")]
    [SerializeField] protected float _dashManaCost;
    [SerializeField] protected float _skill1ManaCost;
    [SerializeField] protected float _skill2ManaCost;

    [Header("Debug Info")]
    [SerializeField] private float _curWeight;
    [SerializeField] private float _curMana;

    //Processing Variables
    private bool _isDashing = false, _isDashAvailable = true;
    private float _dashLength = 0.1f, _dashCoolDown = 5f;
    private Rigidbody2D _rb;
    private Vector2 _moveDir;

    public virtual void Awake() {
        _rb = this.GetComponent<Rigidbody2D>();
    }

    public virtual void Update() {
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");
        _moveDir.x = X != 0 ? X : _moveDir.x;
        _moveDir.y = Y != 0 ? Y : _moveDir.y;
        if (Input.GetKeyDown(KeyCode.Space) && _isDashAvailable) StartCoroutine(Dash()); 
    }

    public virtual void FixedUpdate() {
        if (!_isDashing) 
            _rb.velocity = _moveDir * (_minMoveSpeed + _moveSpeedScale / _curWeight);
        _moveDir = new Vector2(0,0);
    }

    public virtual void Attack() {
        
    }

    public virtual IEnumerator Dash() {
        _isDashing = true;
        _isDashAvailable = false;
        _rb.velocity = _moveDir * _dashForce;
        yield return new WaitForSeconds(_dashLength);
        _isDashing = false;
        _rb.velocity = new Vector2(0,0);
        yield return new WaitForSeconds(_dashCoolDown);
        _isDashAvailable = true;
    }

    public abstract void Skill1();
    public abstract void Skill2();
}