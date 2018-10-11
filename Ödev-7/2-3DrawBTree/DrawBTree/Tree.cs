using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrawBTree
{
    public abstract class TreeNode
    {
        public bool isLeaf;
        public int[] keys;
        public TreeNode[] chilNode;
        public TreeNode parent;
        public int numberOfElements;
        public int xpos, ypos;

        public TreeNode()
        {
            this.parent = null;
        }

        public bool IsLeaf()//yaprak düğüm mü
        {
            bool result = true;
            for (int i = 0; i < this.chilNode.Length; i++)
            {
                if (this.chilNode[i] != null)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        //key sayısı
        public int NumberOfElements()
        {
            int count = 0;
            for (int i = 0; i < this.keys.Length; i++)
            {
                if (this.keys[i] != 0)
                    count++;
            }
            return count;
        }
    }

    public class TwoThreeTreeNode : TreeNode
    {
        public TwoThreeTreeNode()
        {
            this.keys = new int[2];
            this.chilNode = new TwoThreeTreeNode[4];
        }
    }

    public abstract class Tree
    {
        public TreeNode root;
        public int limit;

        public Tree()
        {
            this.limit = 0;
        }

        public void initRoot()
        {
            this.root.isLeaf = true;
            this.root.parent = null;
        }
        //Ekleme Fonk.
        public void Insert(int element)
        {
            TreeNode treeNode = this.FindSubtreeLeaf(this.root, element);
            if (!this.TryInsert(treeNode, element))
                this.Split(treeNode, element);
        }

        //Sayı ağaca eklenebilecek mi, yoksa false döner ve ağaç revize edilir(değiştirilir).
        public bool TryInsert(TreeNode treeNode, int element)
        {
            bool result = true;

            //ana düğümse sayıyı düğüme ekler
            if (treeNode.NumberOfElements() == 0 && treeNode.parent == null)
            {
                treeNode.keys[0] = element;
            }
            //düğümde boş yer varsa boş yere eklenecek.
            else if (treeNode.NumberOfElements() < this.limit)
            {
                treeNode.keys[treeNode.NumberOfElements()] = element;
                this.SortElements(treeNode.keys, treeNode.NumberOfElements());
            }
            else
            {
                result = false;
            }

            return result;
        }

        //miras alınan sınıftan override edilecek.
        public virtual void Split(TreeNode treeNode, int element)
        {

        }

        //sayının ekleneceği alt yapraktaki düğüm bulunur.
        public TreeNode FindSubtreeLeaf(TreeNode node, int element)
        {
            if (node.IsLeaf())
                return node;

            else
            {
                if (element < node.keys[0])
                    return this.FindSubtreeLeaf(node.chilNode[0], element);

                else if (node.NumberOfElements() == 1 || (element > node.keys[0] && element < node.keys[1]))
                    return this.FindSubtreeLeaf(node.chilNode[1], element);

                else if (node.NumberOfElements() == 2 || (element > node.keys[1] && element < node.keys[2]))
                    return this.FindSubtreeLeaf(node.chilNode[2], element);

                else
                    return this.FindSubtreeLeaf(node.chilNode[3], element);
            }
        }

        public TreeNode FindNode(TreeNode node, int element)
        {
            bool isFound = false;
            if (node != null)
            {
                for (int i = 0; i < node.NumberOfElements(); i++)
                {
                    if (node.keys[i] == element)
                        isFound = true;
                }

                if (isFound == true)
                    return node;

                else if (node.NumberOfElements() == 1)
                {
                    if (element < node.keys[0])
                        return FindNode(node.chilNode[0], element);
                    else
                        return FindNode(node.chilNode[1], element);
                }
                else if (node.NumberOfElements() == 2)
                {
                    if (element < node.keys[0])
                    {
                        return FindNode(node.chilNode[0], element);
                    }
                    else if (element > node.keys[1])
                    {
                        return FindNode(node.chilNode[2], element);
                    }
                    else
                    {
                        return FindNode(node.chilNode[1], element);
                    }
                }
                else if (node.NumberOfElements() == 3)
                {
                    if (element < node.keys[0])
                    {
                        return FindNode(node.chilNode[0], element);
                    }
                    else if (element > node.keys[0] && element < node.keys[1])
                    {
                        return FindNode(node.chilNode[1], element);
                    }
                    else if (element > node.keys[1] && element < node.keys[2])
                    {
                        return FindNode(node.chilNode[2], element);
                    }
                    else
                    {
                        return FindNode(node.chilNode[3], element);
                    }
                }
            }

            return null;
        }

        //keyleri küçükten büyüğe doğru sıralar.
        public void SortElements(int[] elements, int numberOfElement)
        {
            int temp = 0;

            for (int write = 0; write < numberOfElement; write++)
            {
                for (int sort = 0; sort < numberOfElement - 1; sort++)
                {
                    if (elements[sort] > elements[sort + 1])
                    {
                        temp = elements[sort + 1];
                        elements[sort + 1] = elements[sort];
                        elements[sort] = temp;
                    }
                }
            }
        }
    }

    public class TwoThreeTree : Tree
    {
        public TwoThreeTree()
        {
            this.root = new TwoThreeTreeNode();
            this.limit = 2;
            this.initRoot();
        }
        //Bölme ayırma işlemi  yapılıyor.
        public override void Split(TreeNode twoThreeNode, int element)
        {
            TwoThreeTreeNode p;

            if (twoThreeNode.parent == null)
            {
                p = new TwoThreeTreeNode();
            }
            else
            {
                p = (TwoThreeTreeNode)twoThreeNode.parent;
            }

            TwoThreeTreeNode n1 = new TwoThreeTreeNode();
            TwoThreeTreeNode n2 = new TwoThreeTreeNode();

            //büyük küçük ve  ortanca sayı bulunuyor.
            int small, middle, large;

            if (element < twoThreeNode.keys[0])
            {
                small = element;
                middle = twoThreeNode.keys[0];
                large = twoThreeNode.keys[1];
            }
            else if (element > twoThreeNode.keys[1])
            {
                small = twoThreeNode.keys[0];
                middle = twoThreeNode.keys[1];
                large = element;
            }
            else
            {
                small = twoThreeNode.keys[0];
                middle = element;
                large = twoThreeNode.keys[1];
            }

            //en sağ ve en sol düğümlere büyük ve küçük sayı setlemesi (ayarlaması) yapılıyor.
            n1.keys[0] = small;
            n2.keys[0] = large;

            //büyük ve küçük sayı nodları p nin child nodu yapılıyor.
            n1.parent = p;
            n2.parent = p;

            //parentın elemanı yoksa değerler büyüklüğe göre uygun sırada eklenir.
            if (p.NumberOfElements() == 0)
            {
                p.chilNode[0] = n1;
                p.chilNode[1] = n2;
                this.root = p;

                n1.isLeaf = true;
                n2.isLeaf = true;
            }
            else if (p.NumberOfElements() == 1)//bir key varsa
            {
                if (n2.keys[0] < p.keys[0])
                {
                    p.chilNode[2] = p.chilNode[1];
                    p.chilNode[0] = n1;
                    p.chilNode[1] = n2;
                }
                else
                {
                    p.chilNode[1] = n1;
                    p.chilNode[2] = n2;
                }
                n1.isLeaf = true;
                n2.isLeaf = true;
            }
            else
            {
                if (n2.keys[0] < p.keys[0] && n2.keys[0] < p.keys[1])
                {
                    p.chilNode[3] = p.chilNode[2];
                    p.chilNode[2] = p.chilNode[1];
                    p.chilNode[0] = n1;
                    p.chilNode[1] = n2;
                }
                else if (n1.keys[0] > p.keys[0] && n1.keys[0] > p.keys[1])
                {
                    p.chilNode[2] = n1;
                    p.chilNode[3] = n2;
                }

                else
                {
                    p.chilNode[3] = p.chilNode[2];
                    p.chilNode[1] = n1;
                    p.chilNode[2] = n2;
                }
            }

            //Yaprak düğüm değilse(a-Aşağı kaydırma işlemi)
            if (twoThreeNode.IsLeaf() == false)
            {
                twoThreeNode.chilNode[0].parent = n1;
                twoThreeNode.chilNode[1].parent = n1;
                twoThreeNode.chilNode[2].parent = n2;
                twoThreeNode.chilNode[3].parent = n2;
                n1.chilNode[0] = twoThreeNode.chilNode[0];
                n1.chilNode[1] = twoThreeNode.chilNode[1];
                n2.chilNode[0] = twoThreeNode.chilNode[2];
                n2.chilNode[1] = twoThreeNode.chilNode[3];
                n1.isLeaf = false;
                n2.isLeaf = false;
            }
            if (p.NumberOfElements() == 2)
            {
                this.Split(p, middle);
                if (n1.chilNode[0] != null || n2.chilNode[0] != null)
                {
                    if (n1.chilNode[0].IsLeaf() || n2.chilNode[0].IsLeaf())
                    {
                        n1.isLeaf = false;
                        n2.isLeaf = false;
                    }
                }
                else
                {
                    n1.isLeaf = true;
                    n2.isLeaf = true;
                }
                n1.parent.isLeaf = false;
                n2.parent.isLeaf = false;
            }
            else if (p.NumberOfElements() == 1)
            {
                if (p.keys[0] < middle)
                {
                    p.keys[1] = middle;
                }
                else
                {
                    p.keys[1] = p.keys[0];
                    p.keys[0] = middle;
                }
            }
            else
            {
                p.keys[0] = middle;
            }
        }
    }
}
