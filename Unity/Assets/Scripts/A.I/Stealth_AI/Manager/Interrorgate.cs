using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interrorgate : MonoBehaviour
{
    public int code;
    private int a, b, c, d;

    public TMPro.TMP_Text _name;
    public TMPro.TMP_Text text;

    void Start()
    {
        a = (int)Random.Range(0f, 9f);
        b = (int)Random.Range(0f, 9f);
        c = (int)Random.Range(0f, 9f);
        d = (int)Random.Range(0f, 9f);
        int.TryParse(a.ToString() + b.ToString() + c.ToString() + d.ToString(), out code);
        Debug.Log(string.Format("Code: {0}{1}{2}{3}", a, b, c, d));
    }

    public void StartInterrorgation()
    {
        StartCoroutine(Interrorgation());
    }

    public IEnumerator Interrorgation()
    {
        _name.gameObject.SetActive(true);
        _name.text = "Manager";
        text.text = string.Format("Okay okay... the code is {0}", code);
        yield return new WaitForSeconds(5);
        _name.text = "";
        text.text = "";
        _name.gameObject.SetActive(false);
    }
}
