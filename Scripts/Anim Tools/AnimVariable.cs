using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimVariable : MonoBehaviour
{
    public int val;
    public void TriggerAnimVar(){
       GameFlow.GF.state = val;
    }
}
