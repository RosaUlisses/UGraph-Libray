﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using GraphLib.Edge;
using GraphLib.Propertys;

namespace GraphLib.AdjacencyList
{
    public class AdjacencyList<TVertex, TEdge, TGraphType> : Graph<TVertex, TEdge, TGraphType>
        where TVertex : IComparable<TVertex>
        where TGraphType : GraphType
        where TEdge : IEdge<TVertex>
    {
        private Type graphType;
        private Dictionary<TVertex, List<OutEdge<TVertex>>> adjacency_lists;
        
        public int Count { get { return adjacency_lists.Count; } }

        public AdjacencyList()
        {
            adjacency_lists = new Dictionary<TVertex, List<OutEdge<TVertex>>>();
            graphType = typeof(TGraphType);
        }

        public override void AddVertex(TVertex vertex)
        {
            // TODO -> levantar execao caso vertex seja null 
            adjacency_lists.Add(vertex, new List<OutEdge<TVertex>>());
        }

        public override void RemoveVertex(TVertex vertex)
        {
            // TODO -> levantar execao caso vertex seja null 
            // TODO -> levantar execao caso o vertice nao exista
            // TODO -> levantar execao caso o o grafo esteja vazio
            adjacency_lists.Remove(vertex);
        }

        private void AddEdgeUndirectedGraph(TEdge edge)
        {
            adjacency_lists[edge.GetSource()].Add(new(edge.GetDestination(), edge.GetWheight())); 
            adjacency_lists[edge.GetDestination()].Add(new(edge.GetSource(), edge.GetWheight()));
        }
        
        private void AddEdgeEdgeDirectedGraph(TEdge edge)
        {
            adjacency_lists[edge.GetSource()].Add(new(edge.GetDestination(), edge.GetWheight())); 
        }
        
        public override void AddEdge(TEdge edge)
        {
            // TODO -> levantar execao caso edge seja null 
            if (graphType == typeof(Directed)) AddEdgeEdgeDirectedGraph(edge);
            else AddEdgeUndirectedGraph(edge);
        }

        private void RemoveEdgeUndirectedGraph(TEdge edge)
        {
            adjacency_lists[edge.GetSource()].Remove(new(edge.GetDestination(), edge.GetWheight())); 
            adjacency_lists[edge.GetDestination()].Remove(new(edge.GetSource(), edge.GetWheight()));
        }

        private void RemoveEdgeDirectedGraph(TEdge edge)
        {
            adjacency_lists[edge.GetSource()].Remove(new(edge.GetDestination(), edge.GetWheight())); 
        }
        
        public override void RemoveEdge(TEdge edge)
        {
            // TODO -> levantar execao caso edge seja null 
            if (graphType == typeof(Directed)) RemoveEdgeDirectedGraph(edge);
            else RemoveEdgeUndirectedGraph(edge);
        }
    }
}