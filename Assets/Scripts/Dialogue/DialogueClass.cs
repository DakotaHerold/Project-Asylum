using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public struct DialogueContainer
{

    [Serializable]
    public struct DialogueEntry
    {
        public string entry_name; 
        public string[] text;
        public string[] choices;
        public string[] requirements; 
        public string[] outcomes;
    }

    public DialogueEntry[] container;
}