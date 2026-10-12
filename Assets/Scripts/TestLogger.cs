using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class TestLogger : MonoBehaviour
{
    [SerializeField] string startMessage = "state";
    [SerializeField] string updateMessage = "hey";
    public int[] ints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(startMessage);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(updateMessage);
    }
}
