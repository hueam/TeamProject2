using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform _firePos;
    Rigidbody _rigid;
    Camera _mainCam;
    float _camValueX;
    float _camValueY;
    [Range(0f,30f)]
    public float _dis; 
    public LayerMask _objLayer;
    public LineRenderer lineR;
    public Weapon _weapon;
    public Ability _ability;

    Action<GameObject> passiveAction;
    List<ActiveSkill> activeAction = new List<ActiveSkill>();

    bool attacking;
    void Start() {
        _mainCam = Camera.main;
        _rigid = GetComponent<Rigidbody>();
        _weapon = GetComponentInChildren<Weapon>();
        _ability = GetComponentInChildren<Ability>();
        foreach(Skill skill in _ability.skills)
        {
            if(skill is PassiveSkill){
                PassiveSkill passive = (PassiveSkill)skill;
                passiveAction += passive.UseSkill;
            }else if(skill is ActiveSkill){
                ActiveSkill active = (ActiveSkill)skill;
                activeAction.Add(active);
            }
        }
    }
    void Update()
    {
        Vector3 input = (Input.GetAxis("Horizontal") * transform.right
            + Input.GetAxis("Vertical") * transform.forward);
        input.y = _rigid.velocity.y;
        MoveCam();
        if(Input.GetKeyDown(KeyCode.Q)){
            activeAction[0].UseSkill(_mainCam.transform);
        }
        transform.position += input*Time.deltaTime*5;
        if(Input.GetKey(KeyCode.Mouse0)) {
            _weapon.Attack(lineR, _firePos, _mainCam.transform, _dis, _objLayer, passiveAction);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            attacking = false;
        }
        if(!attacking){
            lineR.SetPosition(0,Vector3.Lerp(lineR.GetPosition(0),lineR.GetPosition(1),0.002f));
        }
    }
    void MoveCam(){
        _camValueX += Input.GetAxis("Mouse X");
        _camValueY += Input.GetAxis("Mouse Y");
        Mathf.Clamp(_camValueY,-100,90);
        transform.rotation = Quaternion.Euler(-_camValueY,_camValueX,0);
    }
}
