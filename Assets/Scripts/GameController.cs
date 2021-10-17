using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	Coroutine _coro;
	FieldController fieldController;
	[SerializeField] Button btn;

    void Start()
    {
        fieldController = GetComponent<FieldController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public void StartGame()
    {
		if (_coro != null) 
		{
            StopCoroutine(_coro);
        }
        _coro = StartCoroutine(StartGameCoro());
    }

	IEnumerator StartGameCoro() {
		var cells = GameObject.Find("cells");  
		Destroy(cells);
		btn.GetComponentInChildren<Text>().text = "RESTART";
		fieldController.NewGame();
		fieldController.ShowAll();
        yield return new WaitForSeconds(3);
		fieldController.HideAll();
		fieldController.EnableCells();
    }
}
