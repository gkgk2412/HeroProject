using UnityEngine;
using System.Collections;
public class MouseChange : MonoBehaviour
{ 
    //마우스 포인터로 사용할 텍스처를 입력받습니다. 
    public Texture2D cursorTexture ; 
    
    //텍스처의 중심을 마우스 좌표로 할 것인지 체크박스로 입력받습니다.     
    public bool hotSpotIsCenter = false; 
    
    //텍스처의 어느부분을 마우스의 좌표로 할 것인지 텍스처의 좌표를 입력받습니다. 
    public Vector2 adjustHotSpot = Vector2.zero; 
    
    //내부에서 사용할 필드를 선업합니다. 
    private Vector2 hotSpot; 
    public void Start()
    {
        StartCoroutine(MyCursor()); 
    } 
    
    IEnumerator MyCursor() 
    { 
        yield return new WaitForEndOfFrame(); 
        
        if (hotSpotIsCenter) 
        { 
            hotSpot.x = cursorTexture.width / 2;
            hotSpot.y = cursorTexture.height / 2;         
        } 
        else 
        { 
            hotSpot = adjustHotSpot; 
        } 
        
        Cursor.SetCursor (cursorTexture, hotSpot, CursorMode.Auto); 
    } 
}
