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

    [Header("Cost Info")]
    [SerializeField] protected float _dashManaCost;
    [SerializeField] protected float _skill1ManaCost;
    [SerializeField] protected float _skill2ManaCost;

    [Header("Debug Info")]
    [SerializeField] private float _curMoveSpeed;
    [SerializeField] private float _curWeight;
    [SerializeField] private float _curMana;

    //Processing Variables
    private bool _dashAvailable;
    private float _dashDuration = 2f;
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
    }

    public virtual void Attack() {
        
    }

    public virtual IEnumerator Dash() {
        _dashAvailable = false;
        _rb.AddForce(_moveDir * _dashForce, ForceMode2D.Impulse);
        yield return WaitForSeconds(_dashDuration);
    }
}