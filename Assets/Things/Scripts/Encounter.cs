using UnityEngine;


public class Encounter : MonoBehaviour
{
   
    public static void Jump()
    {
        FindObjectOfType<AudioManager>().Play("jumpjoke");
        Cursor.lockState = CursorLockMode.None;
    }


    
}
