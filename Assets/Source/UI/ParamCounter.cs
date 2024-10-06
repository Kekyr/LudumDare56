using UnityEngine;
using UnityEngine.UI;
public class Counter : MonoBehaviour
{
    [SerializeField] private string enemyTag;
    [SerializeField] private Text enemyCount;

    private float elapsedTime = 0f;

    [SerializeField] private Text timer;


    void Update()
    {
        elapsedTime += Time.deltaTime;
        CountEnemyies();
        CountTime();

    }

    private void CountEnemyies()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(enemyTag);
        enemyCount.text = ": " + objectsWithTag.Length.ToString();
    }
    private void CountTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timer.text =  string.Format("{0:D2}:{1:D2}", minutes, seconds); 
    }
}
