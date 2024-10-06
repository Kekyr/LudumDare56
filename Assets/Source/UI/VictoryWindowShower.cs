using UnityEngine;

public class VictoryWindowShower : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;

    [SerializeField] private ParamCounter param;

    public bool isWon = false;
     private void Update()
     {
        if(isWon)
        {
           if(Input.anyKeyDown)
           {
                //Restart
           }
            return;
        }

        if(param.EnemiesCount <= 0)
        {
            victoryPanel.SetActive(true);
            isWon = true;
        }
     }
}
