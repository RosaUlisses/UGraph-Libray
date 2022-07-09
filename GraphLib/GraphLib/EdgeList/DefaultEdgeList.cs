﻿using System.Collections;
using System.Collections.Generic;
using GraphLib.Propertys;
using GraphLib.Edge;


namespace GraphLib.EdgeList
{
    public class EdgeList<TVertex, TEdge, TGraphType> : Graph<TVertex, TEdge, TGraphType>
        where TVertex : IComparable<TVertex>
        where TGraphType : GraphType
        where TEdge : IEdge<TVertex>
    {
        private readonly Type graphType;
        private List<TVertex> vertex_list;
        private List<Edge<TVertex>> edge_list;
        public int Count { get { return vertex_list.Count; } }

        public EdgeList()
        {
            graphType = typeof(TGraphType);
            vertex_list = new List<TVertex>();
            edge_list = new List<Edge<TVertex>>();
        }

        public override void AddVertex(TVertex vertex)
        {
            vertex_list.Add(vertex);
        }

        public override void RemoveVertex(TVertex vertex)
        {
            // TODO -> levantar execao
            vertex_list.Remove(vertex);
        }

        private void AddEdgeDirectedGraph(TEdge edge)
        {
            edge_list.Add(new Edge<TVertex>(edge.GetSource(), edge.GetDestination(), edge.GetWheight()));
        }

        private void AddEdgeUndirectedGraph(TEdge edge)
        {
            edge_list.Add(new Edge<TVertex>(edge.GetSource(), edge.GetDestination(), edge.GetWheight()));
            edge_list.Add(new Edge<TVertex>(edge.GetDestination(), edge.GetSource(), edge.GetWheight()));
        }

        public override void AddEdge(TEdge edge)
        {
            // TODO -> Levantar execao se os vertices nao existirem no grafo
            if (graphType == typeof(Directed)) AddEdgeDirectedGraph(edge);
            else AddEdgeUndirectedGraph(edge);
        }
        
        private void RemoveEdgeDirectedGraph(TEdge edge)
        {
            edge_list.Remove(new Edge<TVertex>(edge.GetSource(), edge.GetDestination(), edge.GetWheight()));
        }

        private void RemoveEdgeUndirectedGraph(TEdge edge)
        {
            edge_list.Remove(new Edge<TVertex>(edge.GetSource(), edge.GetDestination(), edge.GetWheight()));
            edge_list.Remove(new Edge<TVertex>(edge.GetDestination(), edge.GetSource(), edge.GetWheight()));
        }

        public override void RemoveEdge(TEdge edge)
        {
            // TODO -> levantar execao se a aresta nao existir no grafo
            // Considerar o peso na hora de remover a aresta ???
             if (graphType == typeof(Directed)) RemoveEdgeDirectedGraph(edge);
             else RemoveEdgeUndirectedGraph(edge);
                
        }

        public override int GetCount()
        {
            return Count;
        }
    }
}
