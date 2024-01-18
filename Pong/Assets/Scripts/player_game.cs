using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool isAI;
    [SerializeField] private GameObject ball; //AI to move with the ball 
    private Rigidbody2D rb;
    private Vector2 Player_Move;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }
    void Update(){
        if (Input.GetKey("escape")){
            Escape_Game();
        }
        if(isAI){
            AI_Control();
        } else {
            Player_Control();
        }
    }
    private void Escape_Game(){
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    private void Player_Control(){
        Player_Move = new Vector2(0, Input.GetAxisRaw("Vertical"));
    }

    private void AI_Control(){
        if(ball.transform.position.y > transform.position.y + 0.5f){ //0.75 AI to move moves like a real player
            Player_Move = new Vector2(0,1);
        } 
        else if(ball.transform.position.y < transform.position.y - 0.5f){
            Player_Move = new Vector2(0,-1);
        } else {
            Player_Move = new Vector2(0,0);
        }
    }
    private void FixedUpdate(){
    rb.velocity = Player_Move * movementSpeed;
    }
}
