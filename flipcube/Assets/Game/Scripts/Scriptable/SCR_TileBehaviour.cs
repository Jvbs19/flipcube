using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SCR_TileBehaviour : MonoBehaviour
{
    enum TileType { Red, Blue, Green, Yellow};

    [Header("Type Variables")]
    [SerializeField] TileType m_myType;

    [Header("Visual Variables")]
    [SerializeField] Color[] m_colors;
    [SerializeField] MeshRenderer m_tileVisual;

    SCR_BoardManager _board;

    bool _isRandom;
    bool _isMatched = false;
    bool _canSwitch = true;

    int _myWidth;
    int _myHeight;


    void Start()
    {
        InitTile();
    }

    void InitTile()
    {
        if (_isRandom)
        {
            int num = Random.Range(0, 3);
            SetType(num);

            while (CheckMatches(_myWidth, _myHeight)) 
            {
                num = Random.Range(0, 3);
                SetType(num);
            }

            SetUpColor(num);
        }
        else
        {
            SetUpColor(GetMyTypeNum());
        }
    }
    void SetUpColor(int color)
    {
        m_tileVisual.material.color = m_colors[color];

    }

    public void SwitchSide()
    {
        if (_canSwitch)
        {
            if (GetMyTypeName() == "Red")
            {
                m_myType = TileType.Green;
                SetUpColor(GetMyTypeNum());
            }
            else if (GetMyTypeName() == "Blue")
            {
                m_myType = TileType.Yellow;
                SetUpColor(GetMyTypeNum());
            }
            else if (GetMyTypeName() == "Green")
            {
                m_myType = TileType.Red;
                SetUpColor(GetMyTypeNum());
            }
            else
            {
                m_myType = TileType.Blue;
                SetUpColor(GetMyTypeNum());
            }
        }
    }

    public void FindMatches()
    {
        if (_canSwitch)
        {
            // i -> Coluna == Width = X
            // j -> Linha = Row = Height = Y
            if (_myWidth > 0 && _myWidth < _board.GetBoardWidth() - 1)
            {
                SCR_TileBehaviour leftTile = _board.GetTileByPos(_myWidth - 1, _myHeight);
                SCR_TileBehaviour rightTile = _board.GetTileByPos(_myWidth + 1, _myHeight);

                if (leftTile.GetMyTypeName() == GetMyTypeName() && rightTile.GetMyTypeName() == GetMyTypeName()) // Left and Right
                {
                    leftTile.SetIsMatched(true);
                    rightTile.SetIsMatched(true);
                    SetIsMatched(true);
                    Debug.Log("MATCH FOUND");
                }
            }
            if (_myHeight > 0 && _myHeight < _board.GetBoardHeight() - 1)
            {
                SCR_TileBehaviour upTile = _board.GetTileByPos(_myWidth, _myHeight + 1);
                SCR_TileBehaviour downTile = _board.GetTileByPos(_myWidth, _myHeight - 1);

                if (upTile.GetMyTypeName() == GetMyTypeName() && downTile.GetMyTypeName() == GetMyTypeName()) // Up and Down
                {
                    upTile.SetIsMatched(true);
                    downTile.SetIsMatched(true);
                    SetIsMatched(true);
                    Debug.Log("MATCH FOUND");
                }
            }
            if (_myWidth > 1 && _myHeight > 1)
            {
                if (_board.GetTileByPos(_myWidth - 1, _myHeight).GetMyTypeName() == GetMyTypeName() && _board.GetTileByPos(_myWidth - 2, _myHeight).GetMyTypeName() == GetMyTypeName()) // Left Left
                {
                    _board.GetTileByPos(_myWidth - 1, _myHeight).SetIsMatched(true);
                    _board.GetTileByPos(_myWidth - 2, _myHeight).SetIsMatched(true);
                    SetIsMatched(true);
                    Debug.Log("MATCH FOUND");
                }
                if (_board.GetTileByPos(_myWidth, _myHeight - 1).GetMyTypeName() == GetMyTypeName() && _board.GetTileByPos(_myWidth, _myHeight - 2).GetMyTypeName() == GetMyTypeName()) // Down Down
                {
                    _board.GetTileByPos(_myWidth, _myHeight - 1).SetIsMatched(true);
                    _board.GetTileByPos(_myWidth, _myHeight - 2).SetIsMatched(true);
                    SetIsMatched(true);
                    Debug.Log("MATCH FOUND");
                }
            }
            else if (_myWidth <= 1 || _myHeight <= 1)
            {
                if (_myHeight > 1)
                {
                    if (_board.GetTileByPos(_myWidth, _myHeight - 1).GetMyTypeName() == GetMyTypeName() && _board.GetTileByPos(_myWidth, _myHeight - 2).GetMyTypeName() == GetMyTypeName()) //Down Down
                    {
                        _board.GetTileByPos(_myWidth, _myHeight - 1).SetIsMatched(true);
                        _board.GetTileByPos(_myWidth, _myHeight - 2).SetIsMatched(true);
                        SetIsMatched(true);
                        Debug.Log("MATCH FOUND");
                    }
                }
                if (_myWidth > 1)
                {
                    if (_board.GetTileByPos(_myWidth - 1, _myHeight).GetMyTypeName() == GetMyTypeName() && _board.GetTileByPos(_myWidth - 2, _myHeight).GetMyTypeName() == GetMyTypeName()) //Left Left
                    {
                        _board.GetTileByPos(_myWidth - 1, _myHeight).SetIsMatched(true);
                        _board.GetTileByPos(_myWidth - 2, _myHeight).SetIsMatched(true);
                        SetIsMatched(true);
                        Debug.Log("MATCH FOUND");
                    }
                }
            }
        }
    }
    bool CheckMatches(int width, int height)
    {
        // i -> Coluna == Width = X
        // j -> Linha = Row = Height = Y

        if (width > 1 && height > 1)
        {
            if (_board.GetTileByPos(width - 1, height).GetMyTypeName() == GetMyTypeName() && _board.GetTileByPos(width - 2, height).GetMyTypeName() == GetMyTypeName())
            {
                return true;
            }
            if (_board.GetTileByPos(width, height - 1).GetMyTypeName() == GetMyTypeName() && _board.GetTileByPos(width, height - 2).GetMyTypeName() == GetMyTypeName())
            {
                return true;
            }
            return false;
        }
        else if (width <= 1 || height <= 1)
        {
            if(height > 1)
                if (_board.GetTileByPos(width, height - 1).GetMyTypeName() == GetMyTypeName() && _board.GetTileByPos(width, height - 2).GetMyTypeName() == GetMyTypeName())
                    return true;
            if (width > 1)
                if (_board.GetTileByPos(width - 1, height).GetMyTypeName() == GetMyTypeName() && _board.GetTileByPos(width -2, height).GetMyTypeName() == GetMyTypeName())
                    return true; 
        }
        return false;
    }
    #region Get/Set
    public void SetType(int value)
    {
        m_myType = GetTypeFromInt(value);
    }
    public void SetRandomBehaviour(bool isRandom)
    {
        _isRandom = isRandom;
    }
    public string GetMyTypeName()
    {
        return System.Enum.GetName(typeof(TileType), m_myType);
    }
    int GetMyTypeNum()
    {
        return (int)m_myType;
    }
    TileType GetTypeFromInt(int value)
    {
        return (TileType)value;
    }
    #endregion

    public void SetMyWidth(int val)
    {
        _myWidth = val;
    }
    public int GetMyWidth()
    {
        return _myWidth;
    }
    public void SetMyHeight(int val)
    {
        _myHeight = val;
    }
    public int GetMyHeight()
    {
        return _myHeight;
    }

    public void SetBoard(SCR_BoardManager board)
    {
        _board = board; 
    }
    public void SetIsMatched(bool val)
    {
        _isMatched = val;
        _canSwitch = !val;
    }
    public bool GetIsMatched()
    {
        return _isMatched;
    }
}
