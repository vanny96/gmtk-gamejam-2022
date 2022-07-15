using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

[CreateAssetMenu(fileName = "DieConfiguration", menuName = "ScriptableObjects/Create Die Configuration", order = 1)]
public class DieConfiguration : ScriptableObject
{
    public DieEffect FaceCentral;
    public DieEffect FaceUp;
    public DieEffect FaceLeft;
    public DieEffect FaceRight;
    public DieEffect FaceDown;
    public DieEffect FaceDoubleDown;
}
