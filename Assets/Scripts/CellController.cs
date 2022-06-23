using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public GameObject cross;
    public GameObject zero;
    public GameObject gameController;

    private bool isTouched = false;

    public void OnButtonDown()
    {
        if(!isTouched)
        {
            if(gameController.GetComponent<GameController>().crossesOrZeros != 1)
            {
                isTouched = true;
                gameController.GetComponent<GameController>().crossesOrZeros = 1;
                zero.SetActive(false);
                cross.SetActive(true);
            }
            else
            {
                isTouched = true;
                gameController.GetComponent<GameController>().crossesOrZeros = 2;
                cross.SetActive(false);
                zero.SetActive(true);
            }
        }
    }
}
