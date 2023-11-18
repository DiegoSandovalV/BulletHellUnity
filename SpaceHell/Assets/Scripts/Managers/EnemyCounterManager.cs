using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounterManager : MonoBehaviour
{

    public TextMeshProUGUI textMesh;

    public int enemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = "Enemies: " + enemyCount;
    }
}
