using UnityEngine;

public class Player : MonoBehaviour
{

    public float Speed;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move() 
    {
        Vector2 movement = new Vector2(Input.GetAxix("Horizontal"), 0f, 0f;
        transform.position += movement * Time.deltaTime * Speed;
    }     
}
