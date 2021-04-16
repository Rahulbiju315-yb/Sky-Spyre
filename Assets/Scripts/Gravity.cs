using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
   public Vector2 f;
    public bool g;
    public Player player;
    public Transform tr;
   /* Vector3 a;
    Vector3 b;*/

    

    void Start(){
        g=false;
        f.x=0;
        f.y=-175;
        
    }

    void Update(){
        if(g){
            this.GetComponent<Rigidbody2D>().AddForce(f);
            player.jumpSpeed = +95;
            tr.rotation=Quaternion.Euler(0,0,0);
        }
        else {
            tr.rotation=Quaternion.Euler(0,180,180);
            player.jumpSpeed = -83;
        }
    }

}
