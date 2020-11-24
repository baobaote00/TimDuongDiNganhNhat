using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace FloydTest
{
    // floyd form https://github.com/tarasvolosheniuk/FloydWarshallAlgorithm
    // A* and https://www.youtube.com/watch?v=G7XnNtF7UEE&t=474s
    // PriorityQueue form https://gist.github.com/trevordixon/10401462
    class Program
    {
        public class FloydWarshallAlgorithm
        {
            readonly static int INF = int.MaxValue / 2;

            public static void Main(string[] args)
            {
                //int[,] graph = {
                //    {0  ,2  ,2  ,INF,11 },
                //    {1  ,0  ,INF,-1 ,INF},
                //    {12 ,5  ,0  ,8  ,8  },
                //    {7  ,INF,INF,0  ,7  },
                //    {-3 ,INF,INF,INF,0  }
                //};

                int[,] graph = {
                    {0  ,2  ,2  ,INF,11 },
                    {2  ,0  ,INF,-1 ,INF},
                    {2  ,INF,0  ,8  ,8  },
                    {INF,-1 ,8  ,0  ,7  },
                    {11 ,INF,8  ,7  ,0  }
                };

                int[,] graph2 =
                {
                    {0  ,INF,INF,4  ,INF,INF,INF,3  },
                    {INF,0  ,14 ,INF,6  ,INF,7  ,5  },
                    {INF,14 ,0  ,INF,INF,INF,8  ,INF},
                    {4  ,INF,INF,0  ,7  ,10 ,INF,INF},
                    {INF,6  ,INF,7  ,0  ,10 ,6  ,INF},
                    {INF,INF,INF,10 ,10 ,0  ,INF,6  },
                    {INF,7  ,8  ,INF,6  ,INF,0  ,INF},
                    {3  ,5  ,INF,INF,INF,6  ,INF,0  }
                };
                AStar(new Node(0), new Node(6), graph2, 8);
            }

            #region AStar
            static bool Equal(Node O, Node G)
            {
                return O.Name == G.Name;
            }

            static bool CheckInPriority(Node tmp, PriorityQueue<Node> a)
            {
                return a.InQueue(tmp);
            }

            static void GetPath(Node O)
            {
                Console.WriteLine(O.Name);
                if (O.Par == null)
                    return;
                else
                    GetPath(O.Par);
            }

            static void AStar(Node S, Node G, int[,] graph, int V)
            {
                // lấy dữ liệu
                List<List<int>> data = GetData(graph, V);
                graph = floydWarshall(graph, V);
                data = GetHeuristic(data, G, graph, V);

                PriorityQueue<Node> Open = new PriorityQueue<Node>();
                PriorityQueue<Node> Closed = new PriorityQueue<Node>();
                S.H = data[S.Name][data[S.Name].Count - 1];
                Open.Enqueue(S);

                while (true)
                {
                    if (Open.Empty())
                    {
                        Console.WriteLine("tim kiem that bai");
                        return;
                    }

                    Node O = Open.Dequeue();
                    Closed.Enqueue(O);

                    Console.WriteLine("duyet: " + O.Name + " " + O.G + " " + O.H);

                    if (Equal(O, G))
                    {
                        Console.WriteLine("tim kiem thanh cong");
                        GetPath(O);
                        Console.WriteLine("distance: " + (O.G + O.H));
                        return;
                    }

                    int i = 0;
                    while (i < data[O.Name].Count - 1)
                    {
                        int name = data[O.Name][i];
                        int g = O.G + data[O.Name][i + 1];
                        int h = data[name][data[name].Count - 1];

                        Node tmp = new Node(name, g, h);
                        tmp.Par = O;

                        bool ok1 = CheckInPriority(tmp, Open);
                        bool ok2 = CheckInPriority(tmp, Closed);

                        if (!ok1 && !ok2)
                        {
                            Open.Enqueue(tmp);
                        }

                        i += 2;
                    }
                }
            }
            #endregion

            #region Floyd
            static int[,] floydWarshall(int[,] graph, int V)
            {
                int[,] dist = new int[V, V];

                for (int i = 0; i < V; i++)
                {
                    for (int j = 0; j < V; j++)
                    {
                        dist[i, j] = graph[i, j];
                    }
                }

                for (int k = 0; k < V; k++)
                    for (int i = 0; i < V; i++)
                        for (int j = 0; j < V; j++)
                            if (dist[i, j] > dist[i, k] + dist[k, j])
                                dist[i, j] = dist[i, k] + dist[k, j];
                return dist;
            }

            static void printSolution(int[,] dist, int V)
            {
                Console.WriteLine("Matrix: ");
                for (int i = 0; i < V; ++i)
                {
                    for (int j = 0; j < V; ++j)
                    {
                        if (dist[i, j] == INF)
                        {
                            Console.Write("∞\t");
                        }
                        else
                        {
                            Console.Write(dist[i, j] + "\t");
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
            #endregion

            #region data
            static List<List<int>> GetData(int[,] graph, int V)
            {
                List<List<int>> data = new List<List<int>>();
                List<int> l = new List<int>();

                for (int i = 0; i < V; i++)
                {
                    l = new List<int>();
                    for (int j = 0; j < V; j++)
                    {
                        if (graph[i, j] != 0 && graph[i, j] != INF)
                        {
                            l.Add(j);
                            l.Add(graph[i, j]);
                        }
                    }
                    data.Add(l);
                }

                return data;
            }

            static List<List<int>> GetHeuristic(List<List<int>> data, Node G, int[,] graph, int V)
            {
                int[,] floyd = floydWarshall(graph, V);

                for (int i = 0; i < V; i++)
                {
                    data[i].Add(graph[G.Name, i]);
                }

                return data;
            }
            #endregion
        }
    }
}
