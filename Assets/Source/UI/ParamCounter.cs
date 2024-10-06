using UnityEngine;
using UnityEngine.UI;
public class ParamCounter : MonoBehaviour
{
    [SerializeField] private string enemyTag;
    [SerializeField] private Text enemyCount;

    private float elapsedTime = 0f;

    [SerializeField] private Text timer;

    [SerializeField] VictoryWindowShower wonPanel;

    public int EnemiesCount
    {
          get { return GameObject.FindGameObjectsWithTag(enemyTag).Length; }
    }


    void Update()
    {
        if(wonPanel.isWon == false){
            elapsedTime += Time.deltaTime;
        }
        CountEnemyies();
        CountTime();

    }

    private void CountEnemyies()
    {
        enemyCount.text = ": " + EnemiesCount.ToString();
    }
    private void CountTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timer.text =  string.Format("{0:D2}:{1:D2}", minutes, seconds); 
    }
}
