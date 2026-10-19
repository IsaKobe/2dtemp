using System;
using UnityEngine;

public class SuperScript : MonoBehaviour
{
    public string slovo;
    [SerializeField] string text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start selhal");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update selhal" + slovo);

    }

    private void Awake()
    {
        Debug.Log("Alan Wake" + text);
    }
}
