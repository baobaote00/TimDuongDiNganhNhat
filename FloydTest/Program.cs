/**
 * Ten:Nguyen Le Trong Tien
 * MSSV: 19211TT4165
 */
using System;
using System.Diagnostics;
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
                //string[] str = {

                //    "0,i,i,4,i,i,i,3",
                //    "i,0,14,i,6,i,7,5",
                //    "i,14,0,i,i,i,8,i",
                //    "4,i,i,0,7,10,i,i",
                //    "i,6,i,7,0,10,6,i",
                //    "i,i,i,10,10,0,i,6",
                //    "i,7,8,i,6,i,0,i",
                //    "3,5,i,i,i,6,i,0"
                //};

                string[] str1 = {
                    "2,1,i,i,3",
                    "i,0,3,3,8",
                    "i,i,0,1,5",
                    "i,i,2,1,i",
                    "i,i,i,4,0",
                };
                string[] heuristic1 = {
                    "0,1,4,4,3",
                    "i,0,3,3,8",
                    "i,i,0,1,5",
                    "i,i,2,0,7",
                    "i,i,6,4,0",
                };

                WriteFile(str1, "ddnn.dat");
                int[,] graph1 = ReadFile("ddnn.dat");
                WriteFile(heuristic1, "ddnn.dat");
                int[,] graph2 = ReadFile("ddnn.dat");
                for (int j = 0; j < graph1.GetLength(0); j++)
                {
                    Console.WriteLine("Tu {0} -> {1}", 3, j);
                    if (!AStar(new Node(3), new Node(j), graph1,graph2))
                    {
                        Console.WriteLine("khong co duong di");
                    }
                    Console.WriteLine();
                }
                //AStar(new Node(3), new Node(1), graph2);
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
            /// <param name="O">đỉnh 1</param>
            /// <param name="G">đỉnh 2</param>
            /// <returns>đúng nếu 2 node bằng nhau</returns>
            static bool Equal(Node O, Node G)
            {
                return O.Name == G.Name;
            }
            /// <summary>
            /// kiem tra node co trong queue chưa
            /// </summary>
            /// <param name="tmp"></param>
            /// <param name="a"></param>
            /// <returns>đúng nếu node hiện tại nằm trong danh sách</returns>
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
                    Console.Write("\t");
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
            /// <param name="S">đỉnh đầu</param>
            /// <param name="G">đỉnh cuối</param>
            /// <param name="graph">ma trận trọng số</param>
            /// <param name="heuristic">ước tính chi phí tối thiểu từ bất kỳ đỉnh nào đến mục tiêu</param>
            /// <returns>đúng nếu tìm được đường đi</returns>
            static bool AStar(Node S, Node G, int[,] graph,int[,] heuristic)
            {
                // lấy dữ liệu
                List<List<Dinhs>> data = GetData(graph,S,heuristic);

                PriorityQueue<Node> Open = new PriorityQueue<Node>();
                PriorityQueue<Node> Closed = new PriorityQueue<Node>();
                S.Heuristic = data[S.Name][data[S.Name].Count - 1].TrongSo;
                Open.Enqueue(S);

                while (true)
                {
                    if (Open.Empty())
                    {
                        //Console.WriteLine("tim kiem that bai");
                        return false;
                    }

                    Node O = Open.Dequeue();
                    Console.WriteLine("duyet: "+O);
                    if (Equal(O, G))
                    {
                        GetPath(O);
                        Console.WriteLine("distance: " + (O.Heuristic));
                        return true;
                    }

                    Closed.Enqueue(O);

                    for (int i = 0; i < data[O.Name].Count - 1; i++)
                    {
                        int name = data[O.Name][i].Dinh;
                        int g = O.Weight + data[O.Name][i].TrongSo;
                        int h = data[name][data[name].Count - 1].TrongSo;

                        Node tmp = new Node(name, g, h);
                        tmp.Par = O;

                        bool ok1 = CheckInPriority(tmp, Open);
                        bool ok2 = CheckInPriority(tmp, Closed);

                        if (!ok1 && !ok2)
                        {
                            Open.Enqueue(tmp);
                        }
                    }
                }
            }
            #endregion

            #region data
            /// <summary>
            /// chuyển dữ liệu từ ma trận trọng số sang danh sách kề
            /// </summary>
            /// <param name="graph">ma trận trọng số</param>
            /// <param name="A">điểm đầu</param>
            /// <param name="heuristic">ma trận heuristic</param>
            /// <returns>danh sách kề</returns>
            static List<List<Dinhs>> GetData(int[,] graph, Node S, int[,] heuristic)
            {
                List<List<Dinhs>> data = new List<List<Dinhs>>();
                List<Dinhs> l = new List<Dinhs>();

                for (int i = 0; i < graph.GetLength(0); i++)
                {
                    l = new List<Dinhs>();
                    for (int j = 0; j < graph.GetLength(0); j++)
                    {
                        if (graph[i, j] != 0 && graph[i, j] != INF)
                        {
                            l.Add(new Dinhs(j, graph[i, j]));
                        }
                    }
                    l.Add(new Dinhs(heuristic[S.Name, i]));
                    data.Add(l);
                }

                return data;
            }
            #endregion
        }
    }
}
