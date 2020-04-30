using System;

namespace Trie
{

    public class Node
    {
        bool IsWord { get; set; }

        Node[] Child { get; set; }

        int amount = 0;

        char Value { get; set; }

        public Node()
        {
            IsWord = false;
            Child = new Node[33];
        }

        public Node(char value)
        {
            Value = value;
            IsWord = false;
            Child = new Node[33];
        }

        public Node GetChild(char value)
        {
            foreach (Node child in Child)
            {
                if (child?.Value == value)
                    return child;
            }
            return null;
        }

        public void SetChild(char value)
        {
            Child[amount++] = new Node(value);
        }

        public bool Add(string wrd)
        {
            if (wrd == "")
            {
                bool ok = !IsWord;
                IsWord = true;
                return ok;
            }

            char f = wrd[0];
            if (this.GetChild(f) == null)
                this.SetChild(f);

            return this.GetChild(f).Add(wrd.Substring(1));
        }

        public bool Delete(string wrd)
        {
            if (wrd == "")
            {
                bool ok = IsWord;
                IsWord = false;
                return ok;
            }
            
            char f = wrd[0];
            return ((this.GetChild(f) != null) && (this.GetChild(f).Delete(wrd.Substring(1))));
        }

        public bool Find(string wrd)
        {
            if (wrd == "")
                return IsWord;
            return ((this.GetChild(wrd[0]) != null) &&
                    (this.GetChild(wrd[0]).Find(wrd.Substring(1))));

        }

        public bool IsEmpty()
        {
            return amount == 0;
        }

        public void View( ref string str, string wrd = "")
        {
            if (this.IsWord)
                str += wrd + Environment.NewLine;
            foreach(Node child in this.Child)
            {
                if (child != null)
                {
                    string word = wrd + child.Value;
                    child.View(ref str, word);
                }
                    
            }
        }

        public void Reverse( Node node, string wrd = "")
        {
            if (this.IsWord)
                node.Add(wrd);

            foreach (Node child in this.Child)
            {
                if (child != null)
                {
                    string word = child.Value + wrd;
                    child.Reverse(node, word);
                }
            }
        }
        
    }

    public class TreeRoot
    {
        Node Root { get; set; }

        public TreeRoot()
        {
            Root = null;
        }

        public static bool CheckWord(ref string word)
        {
            word = word.Trim();
            if (word == "" || word.IndexOf(' ') > 0)
                return false;
            return true;
        }

        public bool Add(string wrd)
        {
            if (Root == null)
                Root = new Node();
            return (CheckWord(ref wrd) && Root.Add(wrd));
        }

        public bool Find(string wrd)
        {
            return (CheckWord(ref wrd) && !(this.IsEmpty()) && (Root.Find(wrd)));
        }

        public bool Delete(string wrd)
        {
            return (CheckWord(ref wrd) && !(this.IsEmpty()) && Root.Delete(wrd));
        }

        public bool IsEmpty()
        {
            return (Root == null || Root.IsEmpty());
        }
        
        public void Clear()
        {
            Root = null;
        }

        public string View()
        {
            string str = "";
            Root?.View(ref str);
            str.Trim();
            return str;
        }

        public TreeRoot Reverse()
        {
            TreeRoot root = new TreeRoot();
            root.Root = new Node();

            this?.Root?.Reverse(root.Root);

            return root;
        }

    }
}