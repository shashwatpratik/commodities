using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using commodities.Models;
using Newtonsoft.Json.Linq;

namespace commodities.DAL
{
    /// <summary>
    /// Class for getting data from json1.
    /// </summary>
    public class DALFisrtJson
    {
        private readonly string _jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data/Level1.json");
        /// <summary>
        /// Function to get root node.
        /// </summary>
        /// <returns>Returns Root node with its child nodes.</returns>
        public Node GetRoot()
        {
            Node node = new Node();

            var json = JObject.Parse(File.ReadAllText(_jsonPath));
            node.Name = (string)json["name"];
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
        /// Function to get L1 node.
        /// </summary>
        /// <param name="relName">Node Name</param>
        /// <returns>Returns L1 node with child nodes.</returns>
        public Node GetTermNodes(string relName)
        {
            Node node = new Node() { Name = relName };
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