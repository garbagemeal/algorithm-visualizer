[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)

# Algorithm visualizer

A C# winform app containing a collection of algorithms visualized.

### Sorting:
Insertion, Selection, Bubble, Merge, Quick, Heap, Counting, Radix, Introspective.
### Searching:
Binary, Ternary, Exponential.
### Trees (visuals not yet supported):
Binary tree and BST, Depth/Breadth first traversals, Construction using given traversals(pre/post/level + in).
### Graph theory:
BFS, DFS, Connected components using DFS or a disjoint set, Prim's and Kruskal's MST, DFS based maze generator, Top sort DFS, Kahn's top sort, SSSP for DAG, Dijkstra's SSSP, Kosaraju's and Tarjan's strongly connected components, Bellman Ford's SSSP.

# Setup

- Clone the repository
- Make sure to install Microsoft Visual Studio and include .NET Framework in the installer:
![vs-installer-dot-net-framework-option](images/setup/vs-installer-dot-net-framework-option.PNG)
- Navigate to the root dir, run AlgorithmVisualizer.sln using Microsoft Visual Studio.
- Run

In order to make use of the 'Presets' window you will need to have the MySQL DB with the preset details, I used PHPMyAdmin.
An export of the DB is available in 'AlgorithmVisualizer\SQL Files'.

# Screens

![Graph algos](images/mockups/graph-algos.PNG)
![Preset dialog](images/mockups/preset-dialog.PNG)
![Vertex rightclick menu](images/mockups/vertex-rightclick.PNG)
![Empty location rightclick menu](images/mockups/empty-location-rightclick.PNG)
![Maze generator](images/mockups/maze-gen.PNG)
![Array algos](images/mockups/array-algos.PNG)

# License

This content is released under the [MIT license](https://opensource.org/licenses/MIT).
