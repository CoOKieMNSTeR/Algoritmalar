using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

#region Muhammed Said BAKIRCI
namespace DrawBTree
{
    public partial class MainForm : Form
    {
        private int maxTreeHeight, totalNodes;
        private List<int> data = new List<int>();
        private Pen blackPen;
        private SolidBrush blackBrush;
        TwoThreeTree twoThreeTree = new TwoThreeTree();
        Brush colorBrush = new SolidBrush(Color.Blue);

        private void InorderTraversal(TreeNode t, int depth)
        {
            if (t != null && t.chilNode != null)
            {
                t.xpos = totalNodes++ + 1; //x coord satır no
                t.ypos = depth - 1; // y derinlik

                if (t.chilNode[0] != null)
                {
                    InorderTraversal(t.chilNode[0], depth + 1); //her y derinliği için 1 ekleme
                }

                if (t.chilNode[1] != null)
                {
                    t.xpos = totalNodes++ + 1; //x coord satır no
                    t.ypos = depth - 1; // y derinlik
                    InorderTraversal(t.chilNode[1], depth + 1);
                }

                if (t.chilNode[2] != null)
                {
                    t.xpos = totalNodes++ + 1; //x coord satır no
                    t.ypos = depth - 1; // y derinlik
                    InorderTraversal(t.chilNode[2], depth + 1);
                }

            }
        }

        private void ComputeNodePositions()
        {
            int depth = 1;

            InorderTraversal(twoThreeTree.root, depth);
        }


        private int TreeHeight(TreeNode t)
        {
            if (t == null || t.chilNode == null || t.chilNode.Length == 0 || t.chilNode[0] == null) return -1;
            else return 1 + TreeHeight(t.chilNode[0]);
        }

        public MainForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            panel1.Paint += new PaintEventHandler(panel1_Paint);
            panel1.SizeChanged += new EventHandler(panel1_SizeChanged);
            panel1.Font = new Font("SansSerif", 20.0f, FontStyle.Bold);
            blackPen = new Pen(Color.Black);
            blackBrush = new SolidBrush(Color.Black);
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

       

        public void DrawTree(TreeNode root, Graphics g)
        {
            panel1.Width = ClientSize.Width - 8;
            panel1.Height = ClientSize.Height - 8;

            int Width = panel1.Width;
            int Height = panel1.Height;
            int dx = 0, dy = 0, dx2 = 0, dy2 = 0, ys = 20;
            int XSCALE, YSCALE;

            XSCALE = (int)(Width / totalNodes); //x Ölçekleme
            YSCALE = (int)((Height - ys) / (maxTreeHeight + 2)); //y ölçekleme

            XSCALE = XSCALE - 20;
            YSCALE = YSCALE + 20;

            if (root != null)
            {
                // her düğüm çizer
                if (root.chilNode != null && root.chilNode.Length > 0 && root.chilNode[0] != null)
                    DrawTree(root.chilNode[0], g);
                dx = root.xpos * XSCALE; // x ve y kordinatlarını ölçekler
                dy = root.ypos * YSCALE;
                string s = "";
                
                int addElipse = 0;

                if (root.keys != null && root.keys.Length > 0 && root.keys[0] != 0)
                {
                    s = root.keys[0].ToString(); //sayiyi yazdırmak için

                    addElipse = (s.Length - 1) * 5;
                    g.DrawString(s, panel1.Font, blackBrush, new PointF(dx - ys - 5, dy + 5));

                    g.FillEllipse(colorBrush, dx - ys - 5 + addElipse, dy - 5, 10, 10);
                    g.DrawEllipse(blackPen, dx - ys - 5, dy + 5, CalculateEclipse(s), CalculateEclipse(s));
                }

                int add = s.Length * 20;
                if (root.keys != null && root.keys.Length > 1 && root.keys[1] != 0)
                {
                    s = root.keys[1].ToString(); 
                    addElipse = (s.Length - 1) * 5;
                    add = s.Length * 20;
                    g.DrawString(s, panel1.Font, blackBrush, new PointF(dx - ys - 5 + add, dy + 5));//sayiyi yazdırmak için
                    g.FillEllipse(colorBrush, dx - ys + add - 10 + addElipse, dy - 5, 10, 10);

                    g.DrawEllipse(blackPen, dx - ys - 5 + add, dy + 5, CalculateEclipse(s), CalculateEclipse(s));
                }

                // node dan yaprak node 1 çizgi çizer
                if (root.chilNode != null && root.chilNode.Length > 0 && root.chilNode[0] != null && root.keys[0] != 0)
                {
                    dx2 = root.chilNode[0].xpos * XSCALE;
                    dy2 = root.chilNode[0].ypos * YSCALE;
                    g.DrawLine(blackPen, dx, dy, dx2, dy2);
                }
                // node dan yaprak node 1 çizgi çizer
                if (root.chilNode != null && root.chilNode.Length > 1 && root.chilNode[0] != null && root.keys[0] != 0)
                {
                    dx2 = root.chilNode[1].xpos * XSCALE;
                    dy2 = root.chilNode[1].ypos * YSCALE;
                    g.DrawLine(blackPen, dx, dy, dx2, dy2);
                }
                // node dan yaprak node 2 çizgi çizer
                if (root.chilNode != null && root.chilNode.Length > 2 && root.chilNode[1] != null && root.keys[1] != 0)
                {
                    dx2 = root.chilNode[2].xpos * XSCALE;
                    dy2 = root.chilNode[2].ypos * YSCALE;
                    g.DrawLine(blackPen, dx, dy, dx2, dy2);
                }

                if (root.chilNode != null && root.chilNode.Length > 1 && root.chilNode[1] != null && root.keys[0] != 0)
                    DrawTree(root.chilNode[1], g);

                if (root.chilNode != null && root.chilNode.Length > 2 && root.chilNode[2] != null && root.keys[1] != 0)
                    DrawTree(root.chilNode[2], g);
            }
        }

        public int CalculateEclipse(string s)
        {
            return 30 + (s.Length - 1) * 10;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (twoThreeTree.root != null && twoThreeTree.root.NumberOfElements() > 0)
                DrawTree(twoThreeTree.root, e.Graphics);
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            int sayi = 0;

            if (int.TryParse(textBox1.Text, out sayi))
            {
                data.Add(sayi);
                twoThreeTree.Insert(sayi);
                totalNodes = 1;
                ComputeNodePositions();
                maxTreeHeight = TreeHeight(twoThreeTree.root);
                panel1.Invalidate();
            }
            textBox1.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int number = 0;

            if (int.TryParse(textBox1.Text, out number))
            {
                if (data.Contains(number))
                {

                    data.Remove(number);

                    twoThreeTree = new TwoThreeTree();

                    for (int index = 0; index < data.Count;index++ )
                    {
                        twoThreeTree.Insert(data[index]);
                    }

                        totalNodes = 1;
                    ComputeNodePositions();
                    maxTreeHeight = TreeHeight(twoThreeTree.root);
                    panel1.Invalidate();
                    
                }
                else
                {
                    MessageBox.Show(number + " sayısı ağaçta  bulunamdı!!!");
                }
            }
        }

       
    }


}
#endregion