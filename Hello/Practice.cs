using System.Collections.Generic;
using Script.Common;
using UnityEngine;

namespace Script.Hello
{
    public class Practice : MonoBehaviour
    {
        readonly List<(int Number, int State)> _list = new()
        {
            (1, 1),
            (2, 1),
            (3, 1),
            (4, -1),
            (5, 1),
            (6, 1),
            (7, 1),
            (8, -1),
            (9, 1),
        };
        
        // State가 1인 넘버를 제거를 한 리스트를 출력하세여 
        
        void Start()
        {
            One();
            Show.PrintList(_list);
        }

        void One()
        {
            var copyList = new List<(int Number, int State)>(_list);
            _list.Clear();
            _list.Add(copyList[4 - 1]);
            _list.Add(copyList[8 - 1]);
        }
    }
}
