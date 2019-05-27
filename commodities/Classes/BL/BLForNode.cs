using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using commodities.Models;
using commodities.Classes.Misc;
using commodities.DAL;

namespace commodities.Classes.BL
{
    /// <summary>
    /// Buisness logic class for node.
    /// </summary>
    public class BLForNode
    {
        /// <summary>
        /// Function to get child nodes of node.
        /// </summary>
        /// <param name="NodePath">Path of node.</param>
        /// <returns>If success,returns node as data else returns the error message.</returns>
        public MethodResultData<Node> GetNode(string NodePath)
        {
            var nodes = NodePath.Trim().Split('\\');
            int counter = 1;
            Node currNode = new Node();
            if (nodes.Count() < 1 || nodes.Count() > 4)
                return new MethodResultData<Node>("Nodes can't be parsed");
            try
            {
                var nodeData = Traverse(ref currNode, ref counter, ref nodes);
                return new MethodResultData<Node>(nodeData);
            }
            catch (Exception e)
            {
                return new MethodResultData<Node>(e.Message);
            }
        }
        /// <summary>
        /// Function to traverse to correct node.
        /// </summary>
        /// <param name="currNode">Reference Variable for node</param>
        /// <param name="counter">Counter for flow.</param>
        /// <param name="nodes">Array of nodes.</param>
        /// <returns>Returns Last Node with child or throw exception, if any.</returns>
        public Node Traverse(ref Node currNode, ref int counter, ref String[] nodes)
        {
            if (counter % 2 == 1)
            {
                var jsonLoader = new DALJson(((int)((counter / 2) + 1)));
                if (jsonLoader.CheckRootExists(nodes[counter - 1], counter - 2 == -1 ? String.Empty : nodes[counter - 2]))
                {
                    if (nodes.Count() == counter)
                    {
                        return jsonLoader.GetRootChildrens(nodes[counter - 1]);
                    }
                    else
                    {
                        counter++;
                        currNode = Traverse(ref currNode, ref counter, ref nodes);
                    }
                }
                else
                {
                    throw new Exception($"Node doesn't exist for {String.Join("\\", nodes)} on level {counter}");
                }
            }
            else
            {
                var jsonLoader = new DALJson(counter / 2);
                if (jsonLoader.CheckTermNodesExistsForRel(nodes[counter - 1], nodes[counter - 2]))
                {
                    if (nodes.Count() == counter)
                    {
                        return jsonLoader.GetTermNodes(nodes[counter - 1]);
                    }
                    else
                    {
                        counter++;
                        currNode = Traverse(ref currNode, ref counter, ref nodes);
                    }
                }
                else
                {
                    throw new Exception($"Node doesn't exist for {String.Join("\\", nodes)} on level {counter}");
                }
            }
            return currNode;
        }
    }
}