/**
 * Ten:Nguyen Le Trong Tien
 * MSSV: 19211TT4165
 */
using System;
using System.Collections.Generic;
using System.IO;

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
                int[,] graph = {
                    {0  ,2  ,2  ,INF,11 },
                    {2  ,0  ,INF,-1 ,INF},
                    {2  ,INF,0  ,8  ,8  },
                    {INF,-1 ,8  ,0  ,7  },
                    {11 ,INF,8  ,7  ,0  }
                };
                //int[,] graph2 =
                //{
                //    {0  ,INF,INF,4  ,INF,INF,INF,3  },
                //    {INF,0  ,14 ,INF,6  ,INF,7  ,5  },
                //    {INF,14 ,0  ,INF,INF,INF,8  ,INF},
                //    {4  ,INF,INF,0  ,7  ,10 ,INF,INF},
                //    {INF,6  ,INF,7  ,0  ,10 ,6  ,INF},
                //    {INF,INF,INF,10 ,10 ,0  ,INF,6  },
                //    {INF,7  ,8  ,INF,6  ,INF,0  ,INF},
                //    {3  ,5  ,INF,INF,INF,6  ,INF,0  }
                //};
                string[] str = {

                    "0,i,i,4,i,i,i,3",
                    "i,0,14,i,6,i,7,5",
                    "i,14,0,i,i,i,8,i",
                    "4,i,i,0,7,10,i,i",
                    "i,6,i,7,0,10,6,i",
                    "i,i,i,10,10,0,i,6",
                    "i,7,8,i,6,i,0,i",
                    "3,5,i,i,i,6,i,0"
                };
                string[] str1 = {
                    "0,1,i,i,3",
                    "i,0,3,3,8",
                    "i,i,0,1,5",
                    "i,i,2,0,i",
                    "i,i,i,4,0",
                };

                WriteFile(str1, "test.dat");
                int[,] graph2 = ReadFile("test.dat");
                //for (int i = 0; i < graph2.GetLength(0); i++)
                //{
                //    AStar(new Node(0), new Node(i),graph2, 5);
                //}
                AStar(new Node(0), new Node(2), graph2, 5);

            }

            #region ReadWriteFile
            /// <summary>
            /// ghi gile
            /// </summary>
            /// <param name="grap"></param>
            /// <param name="path"></param>
            static void WriteFile(string[] grap, string path)
            {
                try
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(path, FileMode.Create)))
                    {
                        bw.Write(grap.Length);
                        string[] str1;
                        for (int i = 0; i < grap.Length; i++)
                        {
                            str1 = grap[i].Split(',');
                            foreach (var k in str1)
                            {
                                bw.Write(k);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            /// <summary>
            /// doc file
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            static int[,] ReadFile(string path)
            {
                try
                {
                    using (BinaryReader br = new BinaryReader(new FileStream(path, FileMode.Open)))
                    {
                        int length = br.ReadInt32();
                        int[,] result = new int[length, length];
                        for (int i = 0; i < length; i++)
                        {
                            for (int j = 0; j < length; j++)
                            {
                                string str = br.ReadString();
                                result[i, j] = str == "i" ? INF : int.Parse(str);
                            }
                        }
                        return result;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
            #endregion

            #region AStar
            /// <summary>
            /// kiêm tra co bang hay khong
            /// </summary>
            /// <param name="O"></param>
            /// <param name="G"></param>
            /// <returns></returns>
            static bool Equal(Node O, Node G)
            {
                return O.Name == G.Name;
            }
            /// <summary>
            /// kiem tra node co trong queue chưa
            /// </summary>
            /// <param name="tmp"></param>
            /// <param name="a"></param>
            /// <returns></returns>
            static bool CheckInPriority(Node tmp, PriorityQueue<Node> a)
            {
                return a.InQueue(tmp);
            }
            /// <summary>
            /// truy vết đường đi
            /// </summary>
            /// <param name="O"></param>
            static void GetPath(Node O)
            {
                Console.Write(O.Name);
                if (O.Par == null)
                {
                    Console.WriteLine();
                    return;
                }
                else
                {
                    Console.Write("->");
                    GetPath(O.Par);
                }
            }
            /// <summary>
            /// thuật toán A*
            /// </summary>
            /// <param name="S"></param>
            /// <param name="G"></param>
            /// <param name="graph"></param>
            /// <param name="V"></param>
            static void AStar(Node S, Node G, int[,] graph, int V)
            {
                // lấy dữ liệu
                List<List<int>> data = GetData(graph, V);
                graph = floydWarshall(graph, V);
                data = GetHeuristic(data, S, graph, V);
                
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

                    Console.WriteLine("duyet: " + O);

                    if (Equal(O, G))
                    {
                        Console.WriteLine("tim kiem thanh cong");
                        GetPath(O);
                        Console.WriteLine("distance: " + (O.H));
                        return;
                    }

                    for (int i = 0; i < data[O.Name].Count - 1; i += 2)
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
                    }
                    Console.WriteLine("Open: "+Open);
                    Console.WriteLine("Closed: " + Closed);

                }
            }
            #endregion

            #region Floyd
            static int[,] floydWarshall(int[,] graph, int V)
            {
                int[,] dist = new int[V, V];

                for (int i = 0; i < V; i++)
                    for (int j = 0; j < V; j++)
                        dist[i, j] = graph[i, j];

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
