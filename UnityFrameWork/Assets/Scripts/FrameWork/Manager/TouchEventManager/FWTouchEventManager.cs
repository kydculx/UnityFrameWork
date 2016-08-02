using UnityEngine;
using System.Collections;

public class FWTouchEventManager : FWSingleton<FWTouchEventManager>
{
    delegate void listener(string type, int id, float x, float y, float dx, float dy);
    event listener begin0, begin1, begin2, begin3, begin4;
    event listener move0, move1, move2, move3, move4;
    event listener end0, end1, end2, end3, end4;

    Vector2[] delta = new Vector2[5];

    void Start()
    {
        begin0 += onTouch;
        move0 += onTouch;
        end0 += onTouch;
    }

    void Update()
    {
        int count = Input.touchCount;
        if (count == 0) return;

        for (int i = 0; i < count; i++)
        {
            Touch touch = Input.GetTouch(i);
            int id = touch.fingerId;
            Vector2 pos = touch.position;
            if (touch.phase == TouchPhase.Began) delta[id] = touch.position;
            float x, y, dx, dy;
            x = pos.x;
            y = pos.y;
            if (touch.phase == TouchPhase.Began)
            {
                dx = dy = 0;
            }
            else
            {
                dx = pos.x - delta[id].x;
                dy = pos.y - delta[id].y;
            }

            //상태에 따라 이벤트를 호출하자
            if (touch.phase == TouchPhase.Began)
            {
                switch (id)
                {
                case 0: if (begin0 != null) begin0("begin", id, x, y, dx, dy); break;
                case 1: if (begin1 != null) begin1("begin", id, x, y, dx, dy); break;
                case 2: if (begin2 != null) begin2("begin", id, x, y, dx, dy); break;
                case 3: if (begin3 != null) begin3("begin", id, x, y, dx, dy); break;
                case 4: if (begin4 != null) begin4("begin", id, x, y, dx, dy); break;
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                switch (id)
                {
                case 0: if (move0 != null) move0("move", id, x, y, dx, dy); break;
                case 1: if (move1 != null) move1("move", id, x, y, dx, dy); break;
                case 2: if (move2 != null) move2("move", id, x, y, dx, dy); break;
                case 3: if (move3 != null) move3("move", id, x, y, dx, dy); break;
                case 4: if (move4 != null) move4("move", id, x, y, dx, dy); break;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                switch (id)
                {
                case 0: if (end0 != null) end0("end", id, x, y, dx, dy); break;
                case 1: if (end1 != null) end1("end", id, x, y, dx, dy); break;
                case 2: if (end2 != null) end2("end", id, x, y, dx, dy); break;
                case 3: if (end3 != null) end3("end", id, x, y, dx, dy); break;
                case 4: if (end4 != null) end4("end", id, x, y, dx, dy); break;
                }
            }
        }
    }

    void onTouch(string type, int id, float x, float y, float dx, float dy)
    {
        switch (type)
        {
        case "begin": Debug.Log("down:" + x + "," + y); break;
        case "end": Debug.Log("end:" + x + "," + y + ", d:" + dx + "," + dy); break;
        case "move": Debug.Log("move:" + x + "," + y + ", d:" + dx + "," + dy); break;
        }
    }
}