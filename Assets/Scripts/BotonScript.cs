using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BotonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public enum Type
    {
        Rotation,
        MagicQuantity
    }
    public Type _type;
    public GameObject Eventsystem;
    public Vector2 StartScale;

    public int TipoDeObjeto;
    // Start is called before the first frame update
    void Start()
    {
        Eventsystem = GameObject.Find("EventSystem");
        StartScale = this.gameObject.transform.localScale;
    }

    // Update is called once per frame
    public void ChosedColor()
    {
        //if(_type == Type.Rotation) { 
        //}
        if(TipoDeObjeto == 0)
        {
            Eventsystem.GetComponent<ControladorTaza>().ColorAza = this.gameObject.GetComponent<Image>().color;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Eventsystem.GetComponent<ControladorTaza>().OnButtonOver = true;
        //Debug.Log("si");
   
        if(TipoDeObjeto == 0)
        {
            this.transform.localScale = new Vector2(StartScale.x + 0.1f, StartScale.y + 0.1f);

        }
              
             
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Eventsystem.GetComponent<ControladorTaza>().OnButtonOver = false;
        if (TipoDeObjeto == 0)
        {
            this.transform.localScale = new Vector2(StartScale.x, StartScale.y);
        }
       
        }
        
}
