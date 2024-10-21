using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRWeapon : MonoBehaviour
{
    public VRPlyaerController controller;
    public enum Type { Melee, Range };
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider damageArea;
    public Rigidbody rigid;
    public TrailRenderer trailEffect;

    public void Update()
    {
        damage = controller.return_damage();
    }

}