using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class Character : MonoBehaviour {
    [Header("Static Info")]
    [SerializeField] static private float STForceF;
    [SerializeField] static private float STMoveSpeedScale;
    [SerializeField] static private float STForceFScale;

    [Header("Visualization")]
    [SerializeField] protected Animation _anim;
    [SerializeField] protected AudioSource _attackAudio;
    [SerializeField] protected AudioSource _skill1Audio;
    [SerializeField] protected AudioSource _skill2Audio;

    [Header("Info")]
    [SerializeField] protected float _minMoveSpeed;
    [SerializeField] protected float _maxWeight;
    [SerializeField] protected float _maxMana;
    [SerializeField] protected float _dashForce;
    [SerializeField] protected float _minDamageWeight;
    [SerializeField] protected float _maxDamageWeight;

    [Header("Cost Info")]
    [SerializeField] protected float _dashManaCost;
    [SerializeField] protected float _skill1ManaCost;
    [SerializeField] protected float _skill2ManaCost;

    //Processing Variables
    private bool _isDashing = false, _isDashAvailable = true;
    private float _dashLength = 0.1f, _dashCoolDown = 5f;
    private float _curWeight;
    private float _curMana;
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
            _rb.velocity = _moveDir * (_minMoveSpeed + STMoveSpeedScale / _curWeight);
        _moveDir = new Vector2(0,0);
    }

    public virtual void Attack(GameObject otherPlayer) {
        float damageWeight = Random.Range(_minDamageWeight,_maxDamageWeight);
        SendDamge(gameObject,otherPlayer,damageWeight);
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

    public static void SendDamge(GameObject sourcePlayer,GameObject otherPlayer,float damageWeight) {
        Character otherPlayerScript = otherPlayer.GetComponent<Character>();
        Character sourcePlayerScript = sourcePlayer.GetComponent<Character>();
        Rigidbody2D rbOtherPlayer = otherPlayer.GetComponent<Rigidbody2D>();
        Rigidbody2D rbSourcePlayer = sourcePlayer.GetComponent<Rigidbody2D>();
        Vector2 forceDirection = rbOtherPlayer.position - rbSourcePlayer.position;
        rbOtherPlayer.AddForce(forceDirection * (STForceF - otherPlayerScript._curWeight),ForceMode2D.Impulse);
    }
}