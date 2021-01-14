/**
 * Ten:Nguyen Le Trong Tien
 * MSSV: 19211TT4165
 */
using System;
using System.Linq;
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
                    "0,1,i,i,3",
                    "i,0,3,3,8",
                    "i,i,0,1,5",
                    "i,i,2,0,i",
                    "i,i,i,4,0",
                };
                string[] heuristic1 = {
                    "0,1,4,4,3",
                    "i,0,3,3,8",
                    "i,i,0,1,5",
                    "i,i,2,0,7",
                    "i,i,6,4,0",
                };//luu duong di ngan nhat
                
                WriteFile(str1, "ddnn.dat");
                int[,] graph1 = ReadFile("ddnn.dat");
                WriteFile(heuristic1, "ddnn.dat");
                int[,] graph2 = ReadFile("ddnn.dat");

                //in danh sach ke 
                //for (int dinhDau = 0; dinhDau < graph1.GetLength(0); dinhDau++)
                //{
                List<List<Canhs>> danhSachKe = GetData(xoaKhuyen(graph1), new Node(0), xoaKhuyen(graph2));
                inDanhSach(danhSachKe);
                //    inDanhSach(danhSachKe);
                //    //in duong di
                //    for (int j = 0; j < graph1.GetLength(0); j++)
                //    {
                //        if (j == dinhDau)
                //        {
                //            continue;
                //        }
                //        Console.WriteLine("Tu {0} -> {1}", dinhDau, j);
                //        if (!AStar(new Node(dinhDau), new Node(j), danhSachKe))
                //        {
                //            Console.WriteLine("khong co duong di");
                //        }
                //        Console.WriteLine();
                //    }
                //}
                AStar(new Node(0), new Node(2), danhSachKe);
            }
            /// <summary>
            /// hàm xóa khuyên
            /// </summary>
            /// <param name="graph">ma trận trọng số</param>
            /// <returns>ma trận trọng số đã xóa khuyên</returns>
            static int[,] xoaKhuyen(int[,] graph)
            {
                int[,] result = new int[graph.GetLength(0), graph.GetLength(0)];
                for (int i = 0; i < graph.GetLength(0); i++)
                {
                    for (int j = 0; j < graph.GetLength(0); j++)
                    {
                        result[i, j] = graph[i, j];
                    }
                }
                for (int i = 0; i < result.GetLength(0); i++)
                {
                    if (result[i,i]!=0)
                    {
                        result[i, i] = 0;
                    }
                }
                return result;
            }
            /// <summary>
            /// in danh sach kề
            /// </summary>
            /// <param name="danhSachKe"></param>
            public static void inDanhSach(List<List<Canhs>> danhSachKe)
            {
                foreach (var k in danhSachKe)
                {
                    foreach (var l in k)
                    {
                        if (l.DinhDau == -1)
                        {
                            Console.Write(l.TrongSo + "\t");
                        }
                        else
                        {
                            Console.Write(l + "\t");
                        }
                    }
                    Console.WriteLine();
                }
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
                Stack<int> path = new Stack<int>();
                int dinhDau = O.Name;
                path.Push(dinhDau);
                while (O.Par != null)
                {
                    O = O.Par;
                    path.Push(O.Name);
                }
                foreach (var k in path)
                {
                    if (k != path.Last())
                    {
                        Console.Write(k + "->");
                    }
                    else
                    {
                        Console.Write(k+"\t");
                    }
                }  
            }
            /// <summary>
            /// thuật toán A*
            /// </summary>
            /// <param name="start">đỉnh đầu</param>
            /// <param name="last">đỉnh cuối</param>
            /// <param name="graph">ma trận trọng số</param>
            /// <param name="heuristic">ước tính chi phí tối thiểu từ bất kỳ đỉnh nào đến mục tiêu</param>
            /// <returns>đúng nếu tìm được đường đi</returns>
            static bool AStar(Node start, Node last, List<List<Canhs>> danhSachKe)
            {
                // lấy dữ liệu
                PriorityQueue<Node> open = new PriorityQueue<Node>();//luu các đỉnh chuẩn bị đi tới để tìm ra đỉnh có đường đi ngắn nhất
                PriorityQueue<Node> dinhDaXet = new PriorityQueue<Node>();
                start.Heuristic = danhSachKe[start.Name][danhSachKe[start.Name].Count - 1].TrongSo;//lay gia tri cuoi cung
                open.Enqueue(start);

                while (true)
                {
                    if (open.Empty())
                    {
                        //Console.WriteLine("tim kiem that bai");
                        return false;
                    }

                    Node O = open.Dequeue();
                    if (Equal(O, last))
                    {
                        GetPath(O);
                        Console.WriteLine("tong trong so: " + (O.Heuristic));
                        return true;
                    }

                    dinhDaXet.Enqueue(O);

                    for (int i = 0; i < danhSachKe[O.Name].Count - 1; i++)
                    {
                        int name = danhSachKe[O.Name][i].DinhCuoi;
                        int g = O.Weight + danhSachKe[O.Name][i].TrongSo;
                        int h = danhSachKe[name][danhSachKe[name].Count - 1].TrongSo;//gia tri cuoi cung moi dong

                        Node tmp = new Node(name, g, h);
                        tmp.Par = O;

                        bool ok1 = CheckInPriority(tmp, open);//cos true
                        bool ok2 = CheckInPriority(tmp, dinhDaXet);//k cos false 
                        //false && true

                        if (!ok1 && !ok2)//kiem tra dinh trung
                        {
                            open.Enqueue(tmp);
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
            static List<List<Canhs>> GetData(int[,] graph, Node S, int[,] heuristic)
            {
                List<List<Canhs>> data = new List<List<Canhs>>();
                List<Canhs> l = new List<Canhs>();

                for (int i = 0; i < graph.GetLength(0); i++)
                {
                    l = new List<Canhs>();
                    for (int j = 0; j < graph.GetLength(0); j++)
                    {
                        if (graph[i, j] != 0 && graph[i, j] != INF)
                        {
                            l.Add(new Canhs(i,j, graph[i, j]));
                        }
                    }
                    l.Add(new Canhs(-1,-1,heuristic[S.Name, i]));
                    data.Add(l);
                }

                return data;
            }
            #endregion
        }
    }
}
