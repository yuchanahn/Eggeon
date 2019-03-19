using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookme : MonoBehaviour
{
    [SerializeField] Collider2D char_;
    [SerializeField] Collider2D sword_;

    [SerializeField] Transform tip_;

    [SerializeField] float speed_;

    public bool bInGround_;

    Rigidbody2D Rg_;
    PlayerCharacter player_;

    [SerializeField] Camera mainCam_;

    void Start()
    {
        Physics2D.IgnoreCollision(char_, sword_);
        Rg_ = GetComponent<Rigidbody2D>();
        player_ = char_.GetComponent<PlayerCharacter>();
        mainCam_ = Camera.main;
    }

    void Update()
    {
        Vector3 pos = mainCam_.ScreenToWorldPoint(Input.mousePosition);
        Vector3 player_pos = gameObject.transform.position;
        Vector2 mouse_pos = new Vector2(pos.x - player_pos.x, pos.y - player_pos.y);
        float rad = Mathf.Atan2(mouse_pos.x, mouse_pos.y);
        var mouse_rotate = (rad * 180) / Mathf.PI;


        //transform.localEulerAngles = new Vector3(0, 0, (-mouse_rotate + 90));

        

        if (bInGround_ && !player_.swordDashing)
        {
            player_.SwordDash(-transform.right);
            
            //charRigid_.transform.parent = tip_;
            //charRigid_.transform.SetParent(tip_, false);

            /*charRigid_.constraints = 
                  RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            */
            //tip_.localEulerAngles = new Vector3(0,0, mouse_rotate + 90);

            //charRigid_.MovePosition(charRigid_.position - reversed * speed_ * Time.deltaTime);


        }
        else
        {
            Rg_.rotation = (-mouse_rotate + 90);
        }



        transform.localPosition = Vector3.zero;
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            bInGround_ = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            bInGround_ = false;
        }
    }

    private void FixedUpdate()
    {
    }
}