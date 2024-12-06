using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_BombBehaviour : SCR_TileBehaviour
{
    public enum BombType { Width, Height, Propeller, TNT };

    [Header("Type Variables")]
    [SerializeField] BombType m_bombType;

    void Update()
    {
        MoveTile();
    }

    public override void FindMatches()
    {
        //implement
    }

    #region Get/Set
    public void SetBombType(int value)
    {
        m_bombType = GetBombTypeFromInt(value);
    }
    public string GetBombTypeName()
    {
        return System.Enum.GetName(typeof(BombType), m_bombType);
    }
    public int GetBombTypeNum()
    {
        return (int)m_bombType;
    }
    BombType GetBombTypeFromInt(int value)
    {
        return (BombType)value;
    }
    #endregion

}
