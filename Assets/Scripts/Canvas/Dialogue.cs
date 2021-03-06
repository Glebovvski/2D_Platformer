﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public bool isDoneOnce;
    public DialogueElements[] sentences;
}
[System.Serializable]
public class DialogueElements
{
    public Talker Talker;

    [TextArea(3, 10)]
    public string sentence;
}
