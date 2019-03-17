using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookme : MonoBehaviour
{


    [SerializeField] Collider2D char_;
    [SerializeField] Collider2D sowrd_;
    Rigidbody2D rigd;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(char_, sowrd_);
        rigd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 player_pos = gameObject.transform.position;
        Vector2 mouse_pos = new Vector2(pos.x - player_pos.x, pos.y - player_pos.y);
        float rad = Mathf.Atan2(mouse_pos.x, mouse_pos.y);
        var mouse_rotate = (rad * 180) / Mathf.PI;
        transform.localEulerAngles = new Vector3(0, 0, (-mouse_rotate + 90));
    }

    private void FixedUpdate()
    {
    }
}