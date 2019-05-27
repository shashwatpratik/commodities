using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using commodities.Models;
using commodities.Classes.Misc;

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
            if (String.IsNullOrEmpty(NodePath))
            {
                try
                {
                    return new MethodResultData<Node>(new DAL.DALFisrtJson().GetRoot());
                }
                catch (Exception ex)
                {
                    return new MethodResultData<Node>(ex.Message);
                }
            }

            var nodes = NodePath.Trim().Split('\\');
            switch (nodes.Count())
            {
                case 2 :
                    try
                    {
                        return new MethodResultData<Node>(new DAL.DALFisrtJson().GetTermNodes(nodes[1]));
                    }
                    catch (Exception ex)
                    {
                        return new MethodResultData<Node>(ex.Message);
                    }
                case 3 :
                    try
                    {
                        return new MethodResultData<Node>(new DAL.DALSecondJson().GetRoot(nodes[2]));
                    }
                    catch (Exception ex)
                    {
                        return new MethodResultData<Node>(ex.Message);
                    }

                case 4:
                    try
                    {
                        return new MethodResultData<Node>(new DAL.DALSecondJson().GetTermNodes(nodes[3]));
                    }
                    catch (Exception ex)
                    {
                        return new MethodResultData<Node>(ex.Message);
                    }

                default :
                    return new MethodResultData<Node>("Not able to get node details.");
            }
        }
    }
}