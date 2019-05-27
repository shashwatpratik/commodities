using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using commodities.Models;
using Newtonsoft.Json.Linq;


namespace commodities.DAL
{
    public class DALSecondJson
    {
        private readonly string _jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data/Level2.json");
        /// <summary>
        /// Get L2 node with child nodes.
        /// </summary>
        /// <param name="nodeName">Node name.</param>
        /// <returns>Returns L2 node with child nodes.</returns>
        public Node GetRoot(string nodeName)
        {
            Node node = new Node() { Name = nodeName};

            var json = JObject.Parse(File.ReadAllText(_jsonPath));
            
            if (!((string)json["name"]).Equals(nodeName))
                return node;
            
            IList<Node> childNodes = new List<Node>();
            foreach (var term in (JArray)json["termsrelation"])
            {
                string name = (string)term["rel"];
                childNodes.Add(new Node { Name = name });
            }
            node.ChildNodes = childNodes;
            return node;
        }
        /// <summary>
        /// Get L3 node with child nodes.
        /// </summary>
        /// <param name="relName">Node name.</param>
        /// <returns>Returns L3 node with child nodes.</returns>
        public Node GetTermNodes(string relName)
        {
            Node node = new Node() { Name = relName};

            var json = JObject.Parse(File.ReadAllText(_jsonPath))["termsrelation"];
            var termRelation = json.FirstOrDefault(fd => ((string)fd["rel"]).Equals(relName));
            if (termRelation == null)
                return node;
            node.Name = (string)termRelation["rel"];
            IList<Node> childNodes = new List<Node>();
            foreach (var term in (JArray)termRelation["terms"])
            {
                childNodes.Add(new Node { Name = (string)term["name"] });
            }
            node.ChildNodes = childNodes;
            return node;
        }
    }
}