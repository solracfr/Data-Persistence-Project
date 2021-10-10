using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DictTest : MonoBehaviour
{
    // SortedDictonary orders by Key    
    public SortedDictionary<int, string> leaderboard = new SortedDictionary<int, string>()
    {
        {3, "three"},
        {1, "one"},
        {2, "two"},
        {4, "four"}
    };

    public SortedDictionary<string,int> test = new SortedDictionary<string, int>()
    {
        {"three", 3},
        {"one", 1},
        {"two", 2},
        {"four", 4}
    };
    // Start is called before the first frame update
    void Start()
    {
        leaderboard.Add(5, "five");

        //dict.Reverse() displays in reverse order
        foreach(KeyValuePair<int,string> player in leaderboard.Reverse()) Debug.Log($"Player: {player.Value}, Score: {player.Key}");
        foreach(KeyValuePair<string,int> player in test.OrderBy(name => name.Value).Reverse())
        {
            Debug.Log($"{player.Key}, {player.Value}");
        }

        //Debug.Log(leaderboard.Last());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
