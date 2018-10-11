import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Scanner;
				//Muhammed Said BAKIRCI--16010011029
class Edge {
	int source,destination,weight;

	Edge(int source,int destination, int weight){
		this.source = source;
		this.destination = destination;
		this.weight = weight;
	}

	public int compareTo(Edge comparedEdge){
		return this.weight - comparedEdge.weight;
	}

	public String toString(){
		return "D���m | (" + this.source + ")-----(" + this.destination + ") | A��rl�k: " + this.weight;
	}
};

public class Graph {
	/*A�a��daki kare matris, a��rl�kl� bir do�rulanmam�� grafi�i temsil eder.
(i, j) konumundaki de�er, i ve j d���m� aras�ndaki maliyeti g�sterir.
S�f�rlar ba�lant� olmad���n� g�sterir*/

	
	static int[][] matrix = {
			{ 0, 3, 0, 5, 0, 0, 0, 0, 4 }, // 0
			{ 3, 0, 0, 0, 0, 0, 0, 4, 0 }, // 1
			{ 0, 0, 0, 6, 0, 1, 0, 2, 0 }, // 2
			{ 2, 0, 6, 0, 6, 0, 0, 0, 0 }, // 3
			{ 0, 0, 0, 1, 0, 0, 0, 0, 8 }, // 4
			{ 0, 0, 1, 0, 0, 0, 8, 0, 0 }, // 5
			{ 0, 0, 0, 0, 0, 8, 0, 0, 0 }, // 6
			{ 0, 4, 2, 0, 0, 0, 0, 0, 0 }, // 7
			{ 4, 0, 0, 0, 8, 0, 0, 0, 0 } // 8
	};

	/*
    static int[][] matrix = {
		            { 0, 2, 3, 0, 0 }, // 0
					{ 2, 0, 15, 2, 0 }, // 1
					{ 3, 15, 0, 0, 13}, // 2
					{ 0, 2, 0, 0, 9}, // 3
					{ 0, 0, 13, 9, 0}, // 4
                    };
	 */

	static int Node = matrix.length;
	static int[][] Edges = new int[Node][Node];
	static int NotConnected = 999999;
	static int NotReached = 999;

	public static void MakeGraph() {
		for (int i = 0; i < Node; i++) {
			for (int j = 0; j < Node; j++) {
				Edges[i][j] = matrix[i][j];
				if (Edges[i][j] == 0)// D���m i ve D���m j ba�l� de�il ise
					Edges[i][j] = NotConnected;
			}
		}
		// Grafik sunum matrisini yazd�r�n.
		for (int i = 0; i < Node; i++) {
			for (int j = 0; j < Node; j++)
				if (Edges[i][j] != NotConnected)
					System.out.print(" " + Edges[i][j] + " ");
				else // ba�lant� yokken
					System.out.print(" * ");
			System.out.println();
		}
	}

	public static void findEdges(ArrayList<Edge> edges){
		for(int src = 0 ; src < Node ;src++) {
			for (int dest = src; dest < Node; dest++) {
				if (matrix[src][dest] != 0) {
					int weight = matrix[src][dest];
					Edge newEdge = new Edge(src, dest, weight);
					edges.add(newEdge);
				}
			}
		}
	};

	public static void sortEdges(ArrayList<Edge> edges){
		edges.sort(Edge::compareTo);
	};

	public static void Prim(){
		ArrayList<Edge> edges = new ArrayList<Edge>();
		ArrayList<Edge> MST = new ArrayList<Edge>();
		findEdges(edges);
		int startingNode = 0;
		int edgeCount = 0;
		int totalCost = 0;
		int [] nodeReach = new int [Node];
		for(int i = 0; i < Node ; i++){
			nodeReach[i] = NotReached;
		}
		nodeReach[startingNode] = 1;

		while(edgeCount != Node - 1){
			Edge nextEdge = findNextEdge (edges, nodeReach);
			MST.add(nextEdge);
			updateReachability(nextEdge, nodeReach);
			edgeCount++;
			totalCost += nextEdge.weight;
		}

		System.out.println(System.lineSeparator());
		System.out.println("PR�M'�N ALGOR�TMASI �IKI�I:");
		System.out.println("SPANNING A�ACININ TOPLAM MAL�YET�: " + totalCost);
		for(Edge e : MST){
			System.out.println(e);
		}
	}

	public static Edge findNextEdge(ArrayList<Edge> edges, int [] nodeReach){
		ArrayList<Edge> possibleEdges = findPossibleEdges(edges, nodeReach);
		Edge result = findSmallestEdge(possibleEdges);
		edges.remove(result);

		return result;
	}

	public static ArrayList<Edge> findPossibleEdges(ArrayList<Edge> edges,int [] nodeReach ){
		/* Ula��lan d���m� eri�ilemeyen bir d���me ba�layan kenarlar� bulur.*/
		ArrayList<Edge> possibleEdges = new ArrayList<Edge>();
		for(Edge e : edges){
			if((nodeReach[e.source] == 1 && nodeReach[e.destination] == NotReached)
					|| (nodeReach[e.destination] == 1 && nodeReach[e.source] == NotReached))
				possibleEdges.add(e);
		}

		return possibleEdges;
	}

	public static Edge findSmallestEdge(ArrayList<Edge> possibleEdges){
		/* En d���k a��rl��a sahip olan kenar� bulur */
		Edge result = new Edge(99,99,99);
		int currentDistance = NotReached;
		for(Edge p : possibleEdges){
			if(p.weight < currentDistance){
				result = p;
				currentDistance = p.weight;
			}
		}

		return result;
	}

	public static void updateReachability(Edge edge, int[] nodeReach){
		nodeReach[edge.source] = 1;
		nodeReach[edge.destination] = 1;
	}

	public static void Kruskal(){
		ArrayList<Edge> edges = new ArrayList<Edge>();
		ArrayList<Edge> MST = new ArrayList<Edge>();
		findEdges(edges);
		sortEdges(edges);
		int[] visitedNodes = new int[Node + 1];
		int numberOfPaths = 0;
		int totalCost = 0;
		initializeConnectionInfo(visitedNodes);

		for(Edge e : edges){
			if(isAddable(e,visitedNodes,numberOfPaths)) {
				MST.add(e);
				numberOfPaths++;
				totalCost += e.weight;
			}
		}

		System.out.println(System.lineSeparator());
		System.out.println("KRUSKAL ALGOR�TMA �IKI�I:");
		System.out.println("SPANNING A�ACININ M�N�MUM MAL�YET�: " + totalCost);
		for(Edge e : MST){
			System.out.println(e);
		}
	}

	public static void initializeConnectionInfo(int[] visitedNodes){
		for(int i = 0; i < Node; i++){
			visitedNodes[i] = 0;
		}
	}

	public static boolean isAddable(Edge e, int[] visitedNodes, int numberOfPaths){
		/* If the addition of the edge will not cause cycle, returns true */
		boolean result = true;
		int source = e.source;
		int destination = e.destination;
		int numberOfNodes = visitedNodes[Node];
		/* D���mler daha �nce ziyaret edilirse ve MST'deki t�m d���mleri ba�lamak i�in 
		 * yeterli yollar varsa, bu kenar�n eklenmesi d�ng�ye neden olur * D�zenlenemez olarak i�aretle */
		if(visitedNodes[source] == 1 && visitedNodes[destination] == 1){
			if(numberOfPaths == numberOfNodes - 1)
				result = false;
		}
		else{
			updateConnections(e,visitedNodes);
		}

		return result;
	}

	public static void updateConnections(Edge e, int[] visitedNodes) {
		/* A� d���m�n�n say�m�n� artt�rmadan d���mler ziyaret edilmezse */
		if(visitedNodes[e.source] != 1)
			visitedNodes[Node]++;
		if(visitedNodes[e.destination] != 1)
			visitedNodes[Node]++;
		/* D���mleri ziyaret edildi olarak i�aretle */
		visitedNodes[e.source] = 1;
		visitedNodes[e.destination] = 1;
	}

	public static void  main(String[] args) {
		MakeGraph();
		Prim();
		Kruskal();
	}
}
