class TreeNode:
    def __init__(self, value):
        self.value = value
        self.left = None
        self.right = None

def dfs_recursive(root):
    if not root:
        return []

    result = [root.value]
    result.extend(dfs_recursive(root.left))
    result.extend(dfs_recursive(root.right))
    return result

# Beispielbaum erstellen
root = TreeNode(1)
root.left = TreeNode(2)
root.right = TreeNode(3)
root.left.left = TreeNode(4)
root.left.right = TreeNode(5)
root.right.left = TreeNode(6)
root.right.right = TreeNode(7)

# Tiefensuche mit Rekursion durchführen
print(dfs_recursive(root))  # Ausgabe: [1, 2, 4, 5, 3, 6, 7]
