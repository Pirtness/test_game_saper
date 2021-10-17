using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FieldController : MonoBehaviour
{
    [SerializeField] private int rowsNum = 10;
    [SerializeField] private int colsNum = 10;
    private Color lightGrayBg = new Color(0.7f, 0.7f, 0.7f, 1);
           private Color darkGrayBg = new Color(0.55f, 0.55f, 0.55f, 1);
    private Color grayCell = new Color(0.35f, 0.35f, 0.35f, 1);
    private Color blueCell = new Color(0, 0, 1, 1);
    private Color redCell = new Color(1, 0, 0, 1);
    [SerializeField] private GameObject cell;
    [SerializeField] private GameObject cellSquare;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text endText;
    private List<CellController> cells = new List<CellController>();
    private int score = 0;
    private int mainColor = 0;
    private bool cellsEnabled;

    // Start is called before the first frame update
    void Start()
    {
		endText.enabled = false;
        cellsEnabled = false;
        score = 0;
        UpdateScore();
		InstantiateBackground();
        InstantiateCells();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void NewGame()
    {
		mainColor = 0;
        endText.enabled = false;
        cellsEnabled = false;
        score = 0;
        UpdateScore();
		InstantiateCells();
        ColorCells();
    }

	public void EnableCells() 
	{
		cellsEnabled = true;
	}

    private void ColorCells()
    {
        var redCells = cells.OrderBy(x => Random.value).Take(rowsNum * colsNum / 2).ToList();
        for (int i = 0; i < rowsNum * colsNum / 2; ++i)
        {
            redCells[i].isRed = true;
        }
    }

    public void ShowAll()
    {
        for (int i = 0; i < cells.Count; ++i)
        {
            cells[i].SetColor(cells[i].isRed ? redCell : blueCell);
        }
    }
    
    public void HideAll()
    {
        for (int i = 0; i < cells.Count; ++i)
        {
            cells[i].SetColor(grayCell);
        }
    }

    private void ClickCell(CellController c)
    {
        if (!cellsEnabled)
            return;
		if (c.clicked)
			return;
		c.clicked = true;
        if (mainColor == 0)
            mainColor = c.isRed ? 1 : 2;
        if (c.isRed)
        {
            c.SetColor(redCell);
            if (mainColor == 1)
            {
                score += 1;
                UpdateScore();
            }
            else
            {
                loose();
            }
        }
        else
        {
            c.SetColor(blueCell);
            if (mainColor == 2)
            {
                score += 1;
                UpdateScore();
            }
            else
            {
                loose();
            }
        }
    }

    private void UpdateScore()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }
    
    private void loose()
    {
        cellsEnabled = false;
        endText.enabled = true;
        endText.text = "GAME OVER, YOUR SCORE: " + score;
    }
    private void InstantiateCells()
    {
		cells = new List<CellController>();
        GameObject cellsParent = new GameObject();
        cellsParent.name = "cells";
        Transform cellsParentTransform = cellsParent.transform;
        
        float size = cell.GetComponent<BoxCollider2D>().size.x * cell.transform.localScale.x;
        float leftBorder = -size * colsNum / 2;
        float topBorder = -size * rowsNum / 2;
        Vector3 offset = new Vector2(leftBorder, topBorder);

        for (int i = 0; i < rowsNum; ++i)
        {
            for (int j = 0; j < colsNum; ++j)
            {
                CellController newCell = Instantiate(cell, transform.position + offset, Quaternion.identity, cellsParentTransform)
                    .GetComponent<CellController>();
                newCell.SetColor(grayCell);
                newCell.onClick += ClickCell;
                cells.Add(newCell);
                offset = new Vector2(offset.x + size, offset.y);
            }
            offset = new Vector2(leftBorder, offset.y + size);
        }
    }
    
    private void InstantiateBackground()
    {
        GameObject cellsParent = new GameObject();
        cellsParent.name = "background";
        Transform cellsParentTransform = cellsParent.transform;
        
        float size = cell.GetComponent<BoxCollider2D>().size.x * cell.transform.localScale.x;
        float leftBorder = -size * colsNum / 2;
        float topBorder = -size * rowsNum / 2;
        Vector3 offset = new Vector2(leftBorder, topBorder);
        bool col = true;

        for (int i = 0; i < rowsNum; ++i)
        {
            for (int j = 0; j < colsNum; ++j)
            {
                GameObject newCell = Instantiate(cellSquare, transform.position + offset, Quaternion.identity, cellsParentTransform);
                newCell.GetComponent<SpriteRenderer>().color = col ? darkGrayBg : lightGrayBg;
                col = !col;
                offset = new Vector2(offset.x + size, offset.y);
            }
            col = !col;
            offset = new Vector2(leftBorder, offset.y + size);
        }
    }
}
