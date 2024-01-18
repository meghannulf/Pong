using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ball_Movement : MonoBehaviour
{
    [SerializeField] private float Initial_Speed = 10;
    [SerializeField] private float Speed_Increase = 0.25f;
    [SerializeField] private Text Player_Score;
    [SerializeField] private Text AI_Score;
    [SerializeField] private Text Print_Who_Won;
    private int hit_count;
    private Rigidbody2D rb;
    public static bool Game_Start = false;
    public GameObject Main_Menu_UI;
    public static bool Game_is_Won = false;
    public GameObject Winner_Screen;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }
    void Update(){
        //removes start menu
        if(Input.GetKeyDown(KeyCode.Space)){
            if(!Game_Start){
            Start_Ball();
            }
        }
        int Player_Value = int.Parse(Player_Score.text);
        int AI_Value = int.Parse(AI_Score.text);

        if(Player_Value == 5){
            Print_Who_Won.text = "YOU WON!";
            Stop_Game();
        } 
        else if (AI_Value == 5){
            Print_Who_Won.text = "YOU LOST!";
            Stop_Game();
        }
    }
    private void FixedUpdate(){
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, Initial_Speed + (Speed_Increase * hit_count)); //clamp magnitude makes sure the ball speed never goes passed the set max
    }

    //initialize ball 
    private void Start_Ball(){
        Main_Menu_UI.SetActive(false);
        Game_Start = true;
        rb.velocity = new Vector2(-1, 0) * (Initial_Speed + Speed_Increase * hit_count);
    }

    // resets ball by placing in the center of field at the start of each round
    private void Reset_Ball(){
        rb.velocity = new Vector2(0,0);
        transform.position = new Vector2(0,0);
        hit_count = 0;
        Invoke("Start_Ball", 2f);
    }
    private void Player_Bounce(Transform myObject){
        hit_count++;
        Vector2 Ball_Pos = transform.position;
        Vector2 Player_Pos = myObject.position; //paddle the ball has just hit
        float X_Direction, Y_Direction;
        if(transform.position.x > 0){
            X_Direction = -1; //makes ball move the the left
        } else {
            X_Direction = 1; //makes ball move to the right
        }
        Y_Direction = (Ball_Pos.y - Player_Pos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        // ensures the ball will not continuoslly move in a straight line if it hits the center of a paddle 
        if(Y_Direction == 0){
            Y_Direction = 0.25f;
        }
        rb.velocity = new Vector2(X_Direction, Y_Direction) * (Initial_Speed + (Speed_Increase * hit_count)); 
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.name == "Paddle" || collision.gameObject.name == "AI Paddle"){
            Player_Bounce(collision.transform);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(transform.position.x > 0){
            Reset_Ball();
            Player_Score.text = (int.Parse(Player_Score.text) + 1).ToString();
        } else if(transform.position.x < 0){
            AI_Score.text = (int.Parse(AI_Score.text) + 1).ToString();
            Reset_Ball();
        }
    }
    private void Stop_Game(){
        Time.timeScale = 0f;
        Winner_Screen.SetActive(true);
        if(Input.GetKeyDown(KeyCode.Space)){
            Winner_Screen.SetActive(false);
            Time.timeScale = 1f;
            Player_Score.text = "0";
            AI_Score.text = "0";
            Reset_Ball();
        }
    }
}