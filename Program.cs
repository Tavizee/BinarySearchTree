using System;

/// <summary>
/// Represents a single data value within a node. This class stores integer values but could be adapted to store other types.
/// </summary>
class DataEntry
{
    public int data;

    public DataEntry(int data)
    {
        this.data = data; // Assign the provided value to the data property when creating a new instance.
    }
}

/// <summary>
/// Represents a single node in the BST, which includes the data and links to two children (left and right).
/// </summary>
class Node
{
    public DataEntry data;
    public Node left;
    public Node right;

    public Node(int data)
    {
        this.data = new DataEntry(data); // Create a new DataEntry with the given value when initializing a Node.
    }
}

/// <summary>
/// Represents the overall binary search tree. It includes operations such as insert, search, delete, and traversal.
/// </summary>
class Tree
{
    public Node root; // The starting point or top node of the tree.

    /// <summary>
    /// Inserts a value into the tree while maintaining BST properties. No duplicates are allowed.
    /// </summary>
    /// <param name="data">The integer to insert into the tree.</param>
    public void Insert(int data)
    {
        root = Insert(root, data); // Start the recursive insertion from the root.
    }

    /// Recursive method to insert a new node in the correct location in the subtree.
    private Node Insert(Node node, int data)
    {
        if (node == null)
        {
            return new Node(data); // If the spot is empty, create a new node with the data here.
        }

        if (data < node.data.data)
        {
            node.left = Insert(node.left, data); // If the data is less than this node's data, go left.
        }
        else if (data > node.data.data)
        {
            node.right = Insert(node.right, data); // If the data is greater, go right.
        }
        // If data is the same, we do nothing since duplicates are not allowed.
        return node;
    }

    /// <summary>
    /// Prints all the values in the tree in ascending order using in-order traversal.
    /// </summary>
    public void PrintTree()
    {
        PrintTree(root); // Start the recursive process from the root.
        Console.WriteLine(); // Move to a new line after printing all values.
    }

    private void PrintTree(Node node)
    {
        if (node != null)
        {
            PrintTree(node.left); // First, visit the left child.
            Console.Write(node.data.data + " "); // Then print the current node's value.
            PrintTree(node.right); // Finally, visit the right child.
        }
    }

    /// <summary>
    /// Searches for a specific value in the tree.
    /// </summary>
    /// <param name="data">The integer to find.</param>
    /// <returns>True if the value exists in the tree, otherwise false.</returns>
    public bool Contains(int data)
    {
        return Contains(root, data); // Begin the search from the root.
    }

    private bool Contains(Node node, int data)
    {
        if (node == null)
        {
            return false; // If we reach a null node, the value isn't in the tree.
        }

        if (data == node.data.data)
        {
            return true; // If the value matches the current node, we've found it.
        }
        else if (data < node.data.data)
        {
            return Contains(node.left, data); // Search left if the value is less.
        }
        else
        {
            return Contains(node.right, data); // Search right if the value is greater.
        }
    }

    /// <summary>
    /// Deletes a value from the tree, if it exists.
    /// </summary>
    /// <param name="data">The integer to remove from the tree.</param>
    public void Delete(int data)
    {
        root = Delete(root, data); // Start the deletion process from the root.
    }

    private Node Delete(Node node, int data)
    {
        if (node == null)
        {
            return null; // Nothing to delete if the node is null.
        }

        if (data < node.data.data)
        {
            node.left = Delete(node.left, data); // If the value is less, go left.
        }
        else if (data > node.data.data)
        {
            node.right = Delete(node.right, data); // If the value is greater, go right.
        }
        else
        {
            if (node.left == null)
            {
                return node.right; // No left child, the right child takes its place.
            }
            else if (node.right == null)
            {
                return node.left; // No right child, the left child takes its place.
            }

            // Node with two children, replace with the smallest value in the right subtree.
            node.data.data = FindMin(node.right).data.data;
            node.right = Delete(node.right, node.data.data); // Delete the minimum node in the right subtree.
        }
        return node;
    }

    /// <summary>
    /// Helper method to find the smallest node in a subtree.
    /// </summary>
    /// <param name="node">The subtree's root node where the search starts.</param>
    /// <returns>The node with the smallest value.</returns>
    private Node FindMin(Node node)
    {
        Node current = node;
        while (current != null && current.left != null)
        {
            current = current.left; // The smallest value is always on the leftmost node.
        }
        return current;
    }
}

class Program
{
    static void Main()
    {
        Tree tree = new Tree();
        Random random = new Random();

        Console.WriteLine("Inserting unique values between 1 and 10 into the tree...");
        for (int i = 0; i < 10; i++)
        {
            int value = random.Next(1, 11);  // Values between 1 and 10
            tree.Insert(value);
        }

        Console.WriteLine("In-order traversal of the tree:");
        tree.PrintTree();

        Console.WriteLine("Checking if the tree contains the value 5:");
        bool found = tree.Contains(5);
        Console.WriteLine(found ? "Value 5 was found." : "Value 5 was not found.");

        Console.WriteLine("Deleting the value 5 from the tree...");
        tree.Delete(5);
        Console.WriteLine("In-order traversal after deletion:");
        tree.PrintTree();
    }
}
