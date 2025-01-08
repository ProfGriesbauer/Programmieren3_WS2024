from collections import deque

class Node:
    def __init__(self, value):
        self.value = value
        self.children = []

    def add_child(self, child_node):
        self.children.append(child_node)

def heuristic(node):
    # Beispiel-Heuristik: Hier können Sie Ihre eigene Heuristik implementieren
    return node.value

def bfs_with_heuristic(root, target_value):
    queue = deque([root])
    visited = set()

    while queue:
        current_node = queue.popleft()
        if current_node.value == target_value:
            return current_node

        visited.add(current_node)

        # Sortiere die Kinder nach der Heuristik
        sorted_children = sorted(current_node.children, key=heuristic)

        for child in sorted_children:
            if child not in visited:
                queue.append(child)

    return None

# Beispielverwendung
if __name__ == "__main__":
    root = Node(1)
    child1 = Node(2)
    child2 = Node(3)
    child3 = Node(4)
    root.add_child(child1)
    root.add_child(child2)
    child1.add_child(child3)

    target = 4
    result = bfs_with_heuristic(root, target)
    if result:
        print(f"Node with value {target} found: {result.value}")
    else:
        print(f"Node with value {target} not found")