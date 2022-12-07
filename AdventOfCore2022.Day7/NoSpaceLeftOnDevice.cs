namespace AdventOfCore2022.Day7;

public class NoSpaceLeftOnDevice
{
    public Task<string[]> GetInput() => System.IO.File.ReadAllLinesAsync("input.txt");

    public Tree BuildTree(string[] input)
    { 
        Tree tree = new Tree();

        foreach (string line in input)
        {
            switch (line[0])
            {
                case '$':
                    if (line.Substring(2, 2) == "cd")
                    {
                        tree.Move(line.Substring(5));
                    }
                    break;
                case 'd':
                    tree.Add(new Node(new Folder(line.Substring(4))));
                    break;
                default:
                    var parts = line.Split(' ');
                    tree.Add(new Node(new File(int.Parse(parts[0]), parts[1])));
                    break;
            }
        }

        return tree;
    }
}

public class Tree
{
    private Node _current;
    private Node _root;

    public Tree()
    {
        _current = _root = new Node(new Folder("/"));
    }

    public void Add(Node node)
    {
        _current.AddChild(node);
    }

    public void Move(string argument)
    {
        switch (argument)
        {
            case "/":
                _current = _root;
                break;
            case "..":
                if (_current.Parent != null)
                {
                    _current = _current.Parent;
                }
                break;
            default:
                _current = _current.Children.Single(i => i.Item.Name == argument);
                break;
        }
    }

    public long GetSizeOfFoldersAtMost(int limit)
    {
        return GetSizeOfFoldersAtMost(limit, _root);
    }

    private long GetSizeOfFoldersAtMost(int limit, Node node)
    {
        if (node.Item.Type != ItemType.Folder)
        {
            return 0;
        }

        long sum = 0;

        if (node.Size <= limit)
        {
            sum += node.Size;
        }

        foreach (Node child in node.Children)
        {
            sum += GetSizeOfFoldersAtMost(limit, child);
        }

        return sum;
    }

    public long GetSizeOfFolderToDelete()
    { 
        long systemSize = 70000000;
        long requiredSpace = 30000000;
        long currentlyUsed = _root.Size;
        long currentlyFree = systemSize - currentlyUsed;

        long toBeDeleted = requiredSpace - currentlyFree;

        return GetSizeOfFolderToDelete(toBeDeleted, _root.Size, _root);
    }

    private long GetSizeOfFolderToDelete(long toBeDeleted, long currentCandidate, Node node)
    {
        if (node.Item.Type != ItemType.Folder)
        {
            return currentCandidate;
        }

        if (node.Size >= toBeDeleted && node.Size < currentCandidate)
        {
            currentCandidate = node.Size;
        }

        foreach (Node child in node.Children)
        {
            long childSize = GetSizeOfFolderToDelete(toBeDeleted, currentCandidate, child);
            if (childSize < currentCandidate)
            {
                currentCandidate = childSize;
            }
        }

        return currentCandidate;
    }
}

public class Node
{
    public Item Item { get; set; }

    public long Size { get; set; }

    public Node Parent { get; set; }

    public List<Node> Children { get; set; } = new List<Node>();


    public Node(Item item)
    {
        Item = item;
        Size = item.Size;
    }

    public void AddChild(Node node)
    {
        node.Parent = this;

        Children.Add(node);

        Node parent = this;
        while (parent != null)
        {
            parent.Size += node.Size;
            parent = parent.Parent;
        }
    }
}

public abstract record class Item(long Size, string Name, ItemType Type);

public record class File(long Size, string Name) : Item(Size, Name, ItemType.File);

public record class Folder(string Name) : Item(0, Name, ItemType.Folder);

public enum ItemType
{
    File,
    Folder
}