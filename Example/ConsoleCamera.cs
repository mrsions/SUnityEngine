using System.Diagnostics;
using UnityEngine;

public class ConsoleCamera : MonoBehaviour
{
    public override void Awake()
    {
        Console.SetWindowSize(80, 80);

        Console.SetBufferSize(80, 80);

    }

    public override void LateUpdate()
    {
        Console.SetCursorPosition(0, 0);
        Console.Clear();

        Console.WriteLine("Test " + Time.frameCount);
    }
}