using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class MazeGenerator : MonoBehaviour
{
    public GameObject brick;
    // Start is called before the first frame update
    void Start()
    {
        mazeGen(Constants.maze1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void mazeGen(string str)
    {   
        string[] map = str.Split(',');
        for (int i=0; i<= map.Length; i++){
            string tmp = map[i].Trim();
            Debug.Log(map[i].Length);
            for (int j=0; j< tmp.Length; j++) {
                if (tmp[j] == '1') {
                    Instantiate(brick, new Vector3(j,map.Length-i,0), Quaternion.identity);
                }
            }
        }
    }
}
