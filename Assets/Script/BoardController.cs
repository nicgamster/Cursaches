using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    private int _routeLength;

    private ReactiveButton _firstButton; //Первая кнопка
    private ReactiveButton _secondButton; //Вторая кнопка

    private Dictionary<int, int> routes = new Dictionary<int, int>();

    private Transform clips;
    private List<ReactiveClip> clipsToTurn = new List<ReactiveClip>(); //Лист с клипами, которые нужно вырубить

    private ControllCarriage CarriageController;


    private void Start()
    {
        

        for (int i = 0; i < 11; i++)
        {
            routes.Add(i, 0);
        }
        _routeLength = 0;
    }


    /// <summary>
    /// Считает путь, при двух выбранных кнопках
    /// </summary>
    public void CalculateRoute()
    {
        int routeLength = 0;
        InnerStationsRoute(_firstButton.ID, _secondButton.ID, routeLength);
      
        text.text = _routeLength.ToString();
        TurnClips();

        //Debug.Log(routeLength);
    }



    private void InnerStationsRoute(int f_ID, int s_ID, int routeLength)
    {
        //int[,] graph = { {0, 5, 0, 0, 0, 3, 0, 0, 0, 0, 0}, //1
        //                 {5, 0, 0, 0, 2, 4, 0, 0, 0, 0, 0}, //2
        //                 {0, 0, 0, 3, 0, 0, 0, 0, 0, 7, 0}, //3
        //                 {0, 0, 3, 0, 1, 0, 0, 0, 0, 0, 0}, //4
        //                 {0, 2, 0, 1, 0, 9, 0, 0, 0, 4, 0}, //5
        //                 {3, 4, 0, 0, 9, 0, 2, 0, 0, 0, 3}, //6
        //                 {0, 0, 0, 0, 0, 2, 0, 1, 0, 0, 0}, //7
        //                 {0, 0, 0, 0, 0, 0, 1, 0, 5, 0, 0}, //8
        //                 {0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0}, //9
        //                 {0, 0, 7, 0, 4, 0, 0, 0, 0, 0, 8}, //10
        //                 {0, 0, 0, 0, 0, 3, 0, 0, 0, 8, 0}  //11  
        //};

        int[,] graph = { {0, 7, 0, 0 }, //1
                         {7, 0, 5, 4 }, //2
                         {0, 5, 0, 3 }, //3
                         {0, 4, 3, 0 }  //4
        };

        clips = transform.Find("_ InnerStations").transform.Find("Clips"); //Берем нужные клипы
        int start;
        int end;
        if (f_ID > s_ID)
        {
            start = s_ID;
            end = f_ID;
        }
        else
        {
            start = f_ID;
            end = s_ID;
        }

        DijkstraInit(start, graph); 
        string clipname;
        while (start != end)
        {
            int startClip = System.Math.Min(end, routes[end]); //
            int endClip = System.Math.Max(end, routes[end]);

            clipname = $"Clip ({startClip}-{endClip})";

            _routeLength += graph[startClip, endClip];

            //Debug.Log(clips.transform.Find(clipname));
            ReactiveClip clipToTurnOn = clips.transform.Find(clipname).GetComponent<ReactiveClip>();

            clipsToTurn.Add(clipToTurnOn);
            end = routes[end];
        }
    }

    public void StationChoosed(ReactiveButton button) 
    {
        if (_firstButton == null)
        {
            _firstButton = button;
            _firstButton.PushButton();
            
        }
        else if (_secondButton == null)
        {
            _secondButton = button;
            _secondButton.PushButton();
            _secondButton.GetComponent<ControllCarriage>().UseCarriage();
            Messenger<int>.Broadcast(GameEvent.TIMETOMOVE, _secondButton.ID);
            CalculateRoute();
        }
        else
        {
            _firstButton.PushButton();
            _firstButton.GetComponent<ControllCarriage>().UseCarriage();
            _firstButton = null;
            _secondButton.PushButton();
            _secondButton = null;

            TurnClips();
            clipsToTurn.Clear();

            _routeLength = 0;
            text.text = _routeLength.ToString();

        }
    }

    private void TurnClips() //Вырубаем клипы
    {

        foreach (var clip in clipsToTurn)
        {
            clip.TurnClip();
        }


    }

    void DijkstraInit(int start,  int[,] graph)
    {
        

        DijkstraAlgo(graph, start, 4);
    }

    /// <summary>
    /// Штука нужная для алгоритма Дийкстры
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="shortestPathTreeSet"></param>
    /// <param name="verticesCount"></param>
    /// <returns></returns>
    private static int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
    {
        int min = int.MaxValue;
        int minIndex = 0;

        for (int v = 0; v < verticesCount; ++v)
        {
            if (shortestPathTreeSet[v] == false && distance[v] <= min)
            {
                min = distance[v];
                minIndex = v;
            }
        }

        return minIndex;
    }

    public void DijkstraAlgo(int[,] graph, int source, int verticesCount)
    {
        int[] distance = new int[verticesCount];
        bool[] shortestPathTreeSet = new bool[verticesCount];

        for (int i = 0; i < verticesCount; ++i)
        {
            distance[i] = int.MaxValue;
            shortestPathTreeSet[i] = false;
        }

        distance[source] = 0; //Расстояние до начальной точке равно нулю

        for (int count = 0; count < verticesCount - 1; ++count)
        {
            int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount); //Находим минимальное расстояние
            shortestPathTreeSet[u] = true;

            for (int v = 0; v < verticesCount; ++v)
                if (!shortestPathTreeSet[v] &&                //Мы не нашли мин расстояние до этой точки
                    System.Convert.ToBoolean(graph[u, v]) &&  //Мы можем дойти до неё из исследуемой точки
                    distance[u] != int.MaxValue &&            //Предохранитель
                    distance[u] + graph[u, v] < distance[v])  //Расстояние действительно является меньшим
                {
                    distance[v] = distance[u] + graph[u, v];
                    routes[v] = u;

                }



        }

    }




}
