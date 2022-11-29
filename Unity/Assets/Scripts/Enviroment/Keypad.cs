using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Player;
using Enviroment;

public class Keypad : MonoBehaviour
{
    int code;
    int inde;
    int a, b, c, d;

    public TMP_Text codeText;
    public Interrorgate interror;
    public Alarm alr;
    public GameObject keypad;
    public GameObject door;
    public GameObject keypadOBJ;

    private void Start()
    {
        code = interror.code;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            keypad.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        switch (inde)
        {
            case 1:
                codeText.text = a.ToString();
                break;
            case 2:
                codeText.text = a.ToString() + b.ToString();
                break;
            case 3:
                codeText.text = a.ToString() + b.ToString() + c.ToString();
                break;
            case 4:
                codeText.text = a.ToString() + b.ToString() + c.ToString() + d.ToString();
                break;
            default:
                codeText.text = null;
                break;
        }
    }

    public void SetCode(int num)
    {
        inde++;
        Debug.Log(inde);
        switch (inde)
        {
            case 1:
                a = num;
                break;
            case 2:
                b = num;
                break;
            case 3:
                c = num;
                break;
            case 4:
                d = num;
                break;
            case >= 5:
                inde--;
                break;
        }
    }

    public void Accept()
    {
        int tryCode;
        int.TryParse(a.ToString() + b.ToString() + c.ToString() + d.ToString(), out tryCode);
        Debug.Log(tryCode + " " + code);

        if(tryCode == code) 
        { 
            keypad.SetActive(false);
            keypadOBJ.GetComponent<Interaction>().enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Destroy(door);
        }
        else { alr.AlarmFunc(); }
    }

    public void RestartCode()
    {
        a = 0;
        b = 0;
        c = 0;
        d = 0;
        inde = 0;
    }
}
